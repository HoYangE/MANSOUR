using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSet : MonoBehaviour
{
    public bool[,] tileXY;
    public int tileX = 3, tileY = 6;
    public GameObject endButton;
    public GameObject relicParent;
    public GameObject makeCanvas;
    public Effect effect;
    public GameObject makeRelic;
    public GameObject nameText;
    public GameObject findText;
    public ItemDB itemDB;
    public GameObject relicButtonParent;
    public GameObject sizeText;
    public GameObject backGroundImage;
    public Relic relicExle;
    public GameObject popup1;

    EffectData[] data;
    RelicData[] relicData;
    Queue<string> relicNameQueue = new Queue<string>();
    Queue<string> notRelicDBZeroCountName = new Queue<string>();

    private void Awake()
    {
        tileXY = new bool[tileX, tileY];
        endButton.SetActive(false);
        makeCanvas.SetActive(false);
        data = effect.dataArray;
        relicData = relicExle.dataArray;
        sizeText.GetComponent<Text>().text = tileX + " / " + tileY;
        InitTileSet();
    }

    public void ChackTile()
    {
        int count = 0;
        for(int x = 0; x < tileX; x++)
        {
            for(int y = 0; y < tileY; y++)
            {
                if(tileXY[x,y] == true)
                {
                    count++;
                }
            }
        }
        if(count == tileX * tileY)
        {
            endButton.SetActive(true);
        }
        else
        {
            endButton.SetActive(false);
        }
    }
    public void OnOKButton()
    {
        popup1.SetActive(true);
    }
    public void OnOKYesButton()
    {
        popup1.SetActive(false);
        OnEndButton();
    }
    public void OnOKNoButton()
    {
        popup1.SetActive(false);
    }

    private void OnEndButton()
    {
        int childCount = relicParent.transform.childCount;
        int relicCount = 0;
        int myRelicNum = 0;
        ArrayList relicName = new ArrayList();
        for (int i = 0; i < childCount; i++)
        {
            relicNameQueue.Enqueue(relicParent.transform.GetChild(i).GetComponent<Image>().sprite.name);
            Destroy(relicParent.transform.GetChild(i).gameObject);
        }
        int temp = relicNameQueue.Count;
        for (int i = 0;i< temp; i++)
        {
            relicName.Add(relicNameQueue.Dequeue());
        }

        for (int j = 0; j < data.Length; j++)
        {
            relicCount = 0;
            int k = relicName.Count;
            for (int i = 0; i < k; i++)
            {
                if (relicName[i].ToString() == data[j].Firstrelicname || "" == data[j].Firstrelicname)
                {
                    relicCount++;
                }
                else if (relicName[i].ToString() == data[j].Secondrelicname || "" == data[j].Firstrelicname)
                {
                    relicCount++;
                }
                else if (relicName[i].ToString() == data[j].Thirdrelicname || "" == data[j].Firstrelicname)
                {
                    relicCount++;
                }
                else if (relicName[i].ToString() == data[j].Fourthrelicname || "" == data[j].Firstrelicname)
                {
                    relicCount++;
                }
            }
            if (relicCount == k)
            {
                myRelicNum = j;
                break;
            }
        }

        if (childCount == relicCount)
        {
            //Sprite mySprite = Resources.Load<Sprite>("Images/Relic/" + data[myRelicNum].Name);
            Sprite mySprite = Resources.Load<GameObject>("Prefabs/Item/" + data[myRelicNum].Name).GetComponent<Image>().sprite;
            makeRelic.GetComponent<Image>().sprite = mySprite;
            findText.GetComponent<Text>().text =  data[myRelicNum].Findeffecttext;
            for(int i = 0; i < relicData.Length; i++)
            {
                if(data[myRelicNum].Name == relicData[i].Code)
                {
                    relicData[i].Find = true;
                    nameText.GetComponent<Text>().text = relicData[i].Name;
                    relicData[i].Num++;
                }
            }
        }

        for (int x = 0; x < tileX; x++)
        {
            for (int y = 0; y < tileY; y++)
            {
                tileXY[x, y] = false;
            }
        }
        endButton.SetActive(false);
        makeCanvas.SetActive(true);

        childCount = relicParent.transform.childCount;
        DBData[] datas;
        datas = itemDB.db.dataArray;
        for (int i = 0; i < childCount; i++)
        {
            for (int j = 0; j < datas.Length; j++)
            {
                if (relicParent.transform.GetChild(i).GetComponent<Image>().sprite.name == itemDB.GetDataCode(j))
                {
                    itemDB.SetCount(j, itemDB.GetCount(j) - 1);
                }
            }
        }
        ResetRelicCount();
    }

    public void OffEndButton()
    {
        makeCanvas.SetActive(false);
    }

    public void ResetRelic()
    {
        int childCount = relicParent.transform.childCount;
        int num = 0;
        for (int i = 0; i < childCount; i++)
        {
            num = int.Parse(relicParent.transform.GetChild(i).GetComponent<CombinationPiece>().origin.GetComponentInChildren<Text>().text);
            num++;
            relicParent.transform.GetChild(i).GetComponent<CombinationPiece>().origin.GetComponentInChildren<Text>().text = num.ToString();
            Destroy(relicParent.transform.GetChild(i).gameObject);
        }
    }

    public void InitRelic()
    {
        DBData[] datas;
        datas = itemDB.db.dataArray;

        for(int i = 0; i < datas.Length; i++)
        {
            if (datas[i].Num > 0)
            {
                for (int j = 0; j < datas.Length; j++)
                {
                    if(datas[j].Code == itemDB.relicArr[i].GetComponent<Image>().sprite.name)
                    {
                        GameObject instanRelic = Instantiate<GameObject>(itemDB.relicArr[i], relicButtonParent.transform);
                        instanRelic.GetComponentInChildren<Text>().text = datas[j].Num.ToString();
                        instanRelic.SetActive(true);
                    }
                }
            }
        }
    }
    public void DelRelic()
    {
        for (int i = 0; i < relicButtonParent.transform.childCount; i++)
        {
            Destroy(relicButtonParent.transform.GetChild(i).gameObject);
        }
    }

    public void ResetRelicCount()
    {
        int childCount = relicButtonParent.transform.childCount;
        DBData[] datas;
        datas = itemDB.db.dataArray;

        float parX = relicButtonParent.transform.position.x;
        float parY = relicButtonParent.transform.position.y;
        float parWid = relicButtonParent.GetComponent<RectTransform>().rect.width;
        float parHei = relicButtonParent.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < childCount; i++)
        {
            for (int j = 0; j < datas.Length; j++)
            {
                if (relicButtonParent.transform.GetChild(i).GetComponent<Image>().sprite.name == itemDB.GetDataCode(j))
                {
                    if (itemDB.GetCount(j) != 0)
                    {
                        if (itemDB.list.Contains(relicButtonParent.transform.GetChild(i).gameObject) == false)
                        {
                            itemDB.list.Add(relicButtonParent.transform.GetChild(i).gameObject);
                        }
                        relicButtonParent.transform.GetChild(i).GetComponentInChildren<Text>().text = itemDB.GetCount(j).ToString();
                        notRelicDBZeroCountName.Enqueue(itemDB.GetDataCode(j));
                    }
                    else
                    {
                        // 0개일때
                        itemDB.list.Remove(relicButtonParent.transform.GetChild(i).gameObject);
                        Destroy(relicButtonParent.transform.GetChild(i).gameObject);
                    }                   
                }
            }
        }
    }   

    public void DestroyRelicButton()
    {
        for (int i = 0; i < relicButtonParent.transform.childCount; i++)
        {
            Destroy(relicButtonParent.transform.GetChild(i).gameObject);
        }
        itemDB.list.Clear();
        for(int x = 0; x < tileX; x++)
        {
            for(int y = 0; y < tileY; y++)
            {
                tileXY[x, y] = false;
            }
        }
    }

    public void OnXPlusButton()
    {
        if (tileX != 6)
        {
            tileX++;
            ResetSizeButton();
        }
    }

    public void OnXMinusButton()
    {
        if (tileX != 3)
        {
            tileX--;
            ResetSizeButton();
        }
    }

    public void OnYPlusButton()
    {
        if (tileY != 6)
        {
            tileY++;
            ResetSizeButton();
        }
    }

    public void OnYMinusButton()
    {
        if (tileY != 3)
        {
            tileY--;
            ResetSizeButton();
        }
    }

    private void ResetSizeButton()
    {
        tileXY = new bool[tileX, tileY];
        for (int i = 0; i < relicParent.transform.childCount; i++)
        {
            Destroy(relicParent.transform.GetChild(i).gameObject);
        }
        ResetRelicCount();
        sizeText.GetComponent<Text>().text = tileX + " / " + tileY;
        DelTileSet();
        InitTileSet();
        MoveTileSet();
        endButton.SetActive(false);
    }

    private void InitTileSet()
    {
        for(int x = 0; x < tileX; x++)
        {
            for(int y = 0; y < tileY; y++)
            {
                GameObject tile = Instantiate<GameObject>(itemDB.tile, backGroundImage.transform);
                tile.transform.localPosition = new Vector3(x * 100, y * 100,0);
            }
        }
    }

    private void DelTileSet()
    {
        for(int i = 0; i < backGroundImage.transform.childCount; i++)
        {
            Destroy(backGroundImage.transform.GetChild(i).gameObject);
        }
    }

    private void MoveTileSet()
    {
        backGroundImage.transform.localPosition = new Vector3(220 + tileX * -50, -200 + tileY * -50, 0);
    }
}
