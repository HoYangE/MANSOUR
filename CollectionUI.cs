using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollectionUI : MonoBehaviour
{
    public ItemDB itemDB;
    public RelicDB relicDB;
    public GameObject piece;
    public GameObject relic;
    public GameObject pieceCollectionButtonPar;
    public GameObject relicCollectionButtonPar;
    public GameObject CollectionButton;
    public GameObject popup;

    public void ResetPiece(DBData[] itemData)
    {
        for (int i = 0; i < itemDB.relicArr.Length; i++)
        {
            GameObject instantButton = Instantiate<GameObject>(CollectionButton, pieceCollectionButtonPar.transform);
            instantButton.transform.GetChild(0).GetComponent<Image>().sprite = itemDB.relicArr[i].GetComponent<Image>().sprite;
            Debug.Log(instantButton.GetComponentInChildren<Image>().sprite.name);
            if (itemData[i].Find == true)
            {
                instantButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                instantButton.transform.GetChild(0).GetComponent<Image>().color = Color.black;
            }
            instantButton.GetComponent<Button>().onClick.AddListener(MakePopup);
        }
    }
    public void ResetRelic(RelicData[] relicData)
    {
        for (int i = 0; i < relicDB.relicArr.Length; i++)
        {
            GameObject instantButton = Instantiate<GameObject>(CollectionButton, relicCollectionButtonPar.transform);
            instantButton.transform.GetChild(0).GetComponent<Image>().sprite = relicDB.relicArr[i].GetComponent<Image>().sprite;
            if (relicData[i].Find == true)
            {
                instantButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                instantButton.transform.GetChild(0).GetComponent<Image>().color = Color.black;
            }
            instantButton.GetComponent<Button>().onClick.AddListener(MakePopup);
        }
    }

    public void OnPieceButton()
    {
        piece.SetActive(true);
        relic.SetActive(false);
    }
    public void OnRelicButton()
    {
        relic.SetActive(true);
        piece.SetActive(false);
    }

    public void DelPiece()
    {
        for(int i = 0;i< pieceCollectionButtonPar.transform.childCount; i++)
        {
            Destroy(pieceCollectionButtonPar.transform.GetChild(i).gameObject);
        }
    }
    public void DelRelic()
    {
        for (int i = 0; i < relicCollectionButtonPar.transform.childCount; i++)
        {
            Destroy(relicCollectionButtonPar.transform.GetChild(i).gameObject);
        }
    }

    public void MakePopup()
    {
        Sprite tempSprite = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        string tempName = tempSprite.name;
        Debug.Log(tempSprite.name);
        DBData[] pieceData = itemDB.db.dataArray;
        RelicData[] relicData = relicDB.db.dataArray;
        for (int i = 0; i < pieceData.Length; i++)
        {
            if(pieceData[i].Code == tempName)
            {
                for (int j = 0; j < popup.transform.childCount; j++)
                {
                    if(popup.transform.GetChild(j).name == "Item")
                    {
                        popup.transform.GetChild(j).GetComponent<Image>().sprite = tempSprite;

                        if (pieceData[i].Find == true)
                            popup.transform.GetChild(j).GetComponent<Image>().color = Color.white;
                        else
                            popup.transform.GetChild(j).GetComponent<Image>().color = Color.black;

                        popup.transform.GetChild(j).GetComponent<Image>().sprite = null;
                        popup.transform.GetChild(j).GetComponent<Image>().sprite = tempSprite;
                    }
                    if (popup.transform.GetChild(j).name == "Explanation")
                    {
                        for (int k = 0; k < popup.transform.GetChild(j).childCount; k++)
                        {
                            if (popup.transform.GetChild(j).GetChild(k).name == "BackImage")
                            {
                                for(int ii = 0; ii < popup.transform.GetChild(j).GetChild(k).childCount; ii++)
                                {
                                    if (popup.transform.GetChild(j).GetChild(k).GetChild(ii).name == "Name")
                                    {
                                        if (pieceData[i].Find == true)
                                            popup.transform.GetChild(j).GetChild(k).GetChild(ii).GetComponentInChildren<Text>().text = pieceData[i].Name;
                                        else
                                            popup.transform.GetChild(j).GetChild(k).GetChild(ii).GetComponentInChildren<Text>().text = "???";
                                    }
                                }
                            }
                            if (popup.transform.GetChild(j).GetChild(k).name == "ExplanationText")
                            {
                                if (pieceData[i].Find == true)
                                    popup.transform.GetChild(j).GetChild(k).GetComponent<Text>().text = pieceData[i].Explanation;
                                else
                                    popup.transform.GetChild(j).GetChild(k).GetComponent<Text>().text = "???";
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < relicData.Length; i++)
        {
            if (relicData[i].Code == tempName)
            {
                for (int j = 0; j < popup.transform.childCount; j++)
                {
                    if (popup.transform.GetChild(j).name == "Item")
                    {
                        popup.transform.GetChild(j).GetComponent<Image>().sprite = tempSprite;
                        if (relicData[i].Find == true)
                            popup.transform.GetChild(j).GetComponent<Image>().color = Color.white;
                        else
                            popup.transform.GetChild(j).GetComponent<Image>().color = Color.black;

                        popup.transform.GetChild(j).GetComponent<Image>().sprite = null;
                        popup.transform.GetChild(j).GetComponent<Image>().sprite = tempSprite;
                    }
                    if (popup.transform.GetChild(j).name == "Explanation")
                    {
                        for (int k = 0; k < popup.transform.GetChild(j).childCount; k++)
                        {
                            if (popup.transform.GetChild(j).GetChild(k).name == "BackImage")
                            {
                                for (int ii = 0; ii < popup.transform.GetChild(j).GetChild(k).childCount; ii++)
                                {
                                    if (popup.transform.GetChild(j).GetChild(k).GetChild(ii).name == "Name")
                                    {
                                        if(relicData[i].Find == true)
                                            popup.transform.GetChild(j).GetChild(k).GetChild(ii).GetComponentInChildren<Text>().text = relicData[i].Name;
                                        else
                                            popup.transform.GetChild(j).GetChild(k).GetChild(ii).GetComponentInChildren<Text>().text = "???";
                                    }
                                }
                            }
                            if (popup.transform.GetChild(j).GetChild(k).name == "ExplanationText")
                            {
                                if (relicData[i].Find == true)
                                    popup.transform.GetChild(j).GetChild(k).GetComponent<Text>().text = relicData[i].Explanation;
                                else
                                    popup.transform.GetChild(j).GetChild(k).GetComponent<Text>().text = "???";
                            }
                        }
                    }
                }
            }
        }
        popup.SetActive(true);
    }
    public void DelPopup()
    {
        popup.SetActive(false);
    }
}
