using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CombinationPiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector3 defaultPosition;
    public Camera mainCamera;
    public Canvas canvas;
    public GameObject tile;
    public RectTransform tileParent;
    public Combination combination;
    CombinationData[] data;
    TileSet tileSet;

    float tileSize;
    public float minX, maxX, minY, maxY;
    public int posx, posy;

    Sprite sprite;
    GameObject image;
    public GameObject origin;
    string numstr;
    bool isZero;

    private void Awake()
    {
        tileSet = GameObject.Find("Combo_Screen").GetComponent<TileSet>();
        mainCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        canvas = GameObject.Find("SetRelic_Canvas").GetComponent<Canvas>();
        tile = tileSet.itemDB.tile;
        tileParent = GameObject.Find("ComboSeat").GetComponent<RectTransform>();
        combination = Resources.Load("Dialog/Combination") as Combination;
        data = combination.dataArray;

        tileSize = tile.GetComponent<RectTransform>().sizeDelta.x;

        SetPiecePosisionSet();
        isZero = false;
    }

    private void Update()
    {
        SetPiecePosisionSet();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>())
        {
            numstr = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        }
        if (numstr == "0")
        {
            isZero = true;
        }
        // 드래그 시작
        defaultPosition = transform.localPosition;

        sprite = EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image>().sprite;

        if (!EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>())
        {
            // 자식없을때(이미 생긴 오브젝트)
            image = EventSystem.current.currentSelectedGameObject;
            ResetBoolTile(posx, posy);
        }
        else
        {
            // 새로 만들어야하는 오브젝트
            int num = int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
            if (num > 0)
            {
                num--;
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = num.ToString();

                image = Instantiate<GameObject>(Resources.Load("Prefabs/Item/" + sprite.name) as GameObject, canvas.transform, false);
                image.transform.localScale = Vector3.one * 3;
                image.GetComponent<Image>().sprite = sprite;
                image.GetComponent<CombinationPiece>().origin = gameObject;
                image.transform.name = image.GetComponent<Image>().sprite.name;
                Destroy(image.transform.GetChild(0).gameObject);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isZero != true)
        {
            // 드래그중
            Vector3 test = Input.mousePosition;
            Vector3 currentPos = new Vector3(test.x - (Screen.width / 2), test.y - (Screen.height / 2), 0);
            if (image != null)
                image.transform.localPosition = currentPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isZero != true)
        {
            if (image != null)
            {
                if (image.transform.localPosition.x > minX && image.transform.localPosition.x < maxX && image.transform.localPosition.y > minY && image.transform.localPosition.y < maxY)
                {
                    // 범위 안
                    for (int x = 0; x < tileSet.tileX; x++)
                    {
                        for (int y = 0; y < tileSet.tileY; y++)
                        {
                            if (image.transform.localPosition.x > minX + x * tileSize && image.transform.localPosition.x < minX + (x + 1) * tileSize && image.transform.localPosition.y > minY + y * tileSize && image.transform.localPosition.y < minY + (y + 1) * tileSize)
                            {
                                image.transform.localPosition = new Vector3(minX + (x * tileSize + tileSize / 2), minY + (y * tileSize + tileSize / 2), 0);
                                image.GetComponent<CombinationPiece>().posx = x;
                                image.GetComponent<CombinationPiece>().posy = y;
                                foreach (CombinationData i in data)
                                {
                                    if (i.Name == image.GetComponent<Image>().sprite.name)
                                    {
                                        Vector2 first = new Vector2(i.Firstx, i.Firsty);
                                        Vector2 second = new Vector2(i.Secondx, i.Secondy);
                                        Vector2 third = new Vector2(i.Thirdx, i.Thirdy);
                                        Vector2 fourth = new Vector2(i.Fourthx, i.Fourthy);
                                        Vector2 fifth = new Vector2(i.Fifthx, i.Fifthy);
                                        Vector2[] pieceVectors = { first, second, third, fourth, fifth };

                                        int count = 0;

                                        for (int j = 0; j < 5; j++)
                                        {
                                            if ((int)pieceVectors[j].x + x >= 0 && (int)pieceVectors[j].x + x < tileSet.tileX && (int)pieceVectors[j].y + y >= 0 && (int)pieceVectors[j].y + y < tileSet.tileY)
                                            {
                                                count++;
                                            }
                                            else
                                            {
                                                DestroyImage();
                                                break;
                                            }
                                        }
                                        if (count == 5)
                                        {
                                            count = 0;

                                            for (int k = 0; k < 5; k++)
                                            {
                                                // 5가지 전부 내부
                                                if (tileSet.tileXY[(int)pieceVectors[k].x + x, (int)pieceVectors[k].y + y] == false)
                                                {
                                                    count++;
                                                }
                                                if (count == 5)
                                                {
                                                    for (int ik = 0; ik < 5; ik++)
                                                    {
                                                        tileSet.tileXY[(int)pieceVectors[ik].x + x, (int)pieceVectors[ik].y + y] = true;
                                                    }
                                                }
                                            }
                                            if (count != 5)
                                            {
                                                DestroyImage();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // 범위 밖
                    Debug.Log("범위 밖");
                    DestroyImage();
                }
            }
            tileSet.ChackTile();            
        }
    }

    public void DestroyImage()
    {
        if (!origin)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>())
            {
                int num = int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
                num++;
                EventSystem.current.currentSelectedGameObject.GetComponent<CombinationPiece>().isZero = false;
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = num.ToString();
            }
        }
        else
        {
            int num = int.Parse(origin.GetComponentInChildren<Text>().text);
            num++;
            origin.GetComponent<CombinationPiece>().isZero = false;
            origin.GetComponentInChildren<Text>().text = num.ToString();
        }
        Destroy(image);
    }

    public void ResetBoolTile(int x, int y)
    {
        foreach (CombinationData i in data)
        {
            if (i.Name == image.GetComponent<Image>().sprite.name)
            {
                Vector2 first = new Vector2(i.Firstx, i.Firsty);
                Vector2 second = new Vector2(i.Secondx, i.Secondy);
                Vector2 third = new Vector2(i.Thirdx, i.Thirdy);
                Vector2 fourth = new Vector2(i.Fourthx, i.Fourthy);
                Vector2 fifth = new Vector2(i.Fifthx, i.Fifthy);
                Vector2[] pieceVectors = { first, second, third, fourth, fifth };

                for (int ik = 0; ik < 5; ik++)
                {
                    tileSet.tileXY[(int)pieceVectors[ik].x + x, (int)pieceVectors[ik].y + y] = false;
                }
            }
        }
    }

    private void SetPiecePosisionSet()
    {
        if (tileSet.tileX == 3)
        {
            minX = tileParent.localPosition.x - (tileSize / 2) + 70;
            maxX = tileParent.localPosition.x + ((tileSet.tileX - 1) * tileSize + tileSize / 2) + 70;
        }
        else if (tileSet.tileX == 4)
        {
            minX = tileParent.localPosition.x - (tileSize / 2) + 20;
            maxX = tileParent.localPosition.x + ((tileSet.tileX - 1) * tileSize + tileSize / 2) + 20;
        }
        else if (tileSet.tileX == 5)
        {
            minX = tileParent.localPosition.x - (tileSize / 2) - 30;
            maxX = tileParent.localPosition.x + ((tileSet.tileX - 1) * tileSize + tileSize / 2) - 30;
        }
        else if (tileSet.tileX == 6)
        {
            minX = tileParent.localPosition.x - (tileSize / 2) - 80;
            maxX = tileParent.localPosition.x + ((tileSet.tileX - 1) * tileSize + tileSize / 2) - 80;
        }
        if (tileSet.tileY == 3)
        {
            minY = tileParent.localPosition.y - ((tileSet.tileY - 1) * tileSize + tileSize / 2) - 150;
            maxY = tileParent.localPosition.y + (tileSize / 2) - 150;
        }
        else if (tileSet.tileY == 4)
        {
            minY = tileParent.localPosition.y - ((tileSet.tileY - 1) * tileSize + tileSize / 2) - 100;
            maxY = tileParent.localPosition.y + (tileSize / 2) - 100;
        }
        else if (tileSet.tileY == 5)
        {
            minY = tileParent.localPosition.y - ((tileSet.tileY - 1) * tileSize + tileSize / 2) - 50;
            maxY = tileParent.localPosition.y + (tileSize / 2) - 50;
        }
        else if (tileSet.tileY == 6)
        {
            minY = tileParent.localPosition.y - ((tileSet.tileY - 1) * tileSize + tileSize / 2);
            maxY = tileParent.localPosition.y + (tileSize / 2);
        }
    }
}
