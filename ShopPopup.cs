using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public GameObject popup4;
    public GameObject popup5;

    public GameObject parObject;
    public ShopManager manager;

    private void Start()
    {
        //manager.dialogManager.ClickDialog(Random.Range(21, manager.dialogManager.dialogSheet.dataArray.Length)); // min 이상, max 미만
    }

    public void OnBuyYesButton()
    {
        string targetStr = parObject.GetComponentInChildren<Text>().text;
        string tempStr = Regex.Replace(targetStr, @"\D", "");
        if (int.Parse(tempStr) <= PlayerPrefs.GetInt("money"))
        {
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - int.Parse(tempStr));
            for (int i = 0; i < parObject.transform.parent.gameObject.transform.childCount; i++)
            {
                if (parObject.transform.parent.gameObject.transform.GetChild(i).name == "ItemImage")
                {
                    string spriteName = parObject.transform.parent.gameObject.transform.GetChild(i).GetComponent<Image>().sprite.name;
                    WeaponDBData[] WeaponData = manager.shopDB.db.dataArray;
                    for (int j = 0; j < WeaponData.Length; j++)
                    {
                        if (spriteName == WeaponData[j].Code)
                        {
                            WeaponData[j].Num++;
                            Popup2Text(WeaponData[j].Name);
                            Popup2Image(WeaponData[j].Code);
                        }
                    }
                }
            }
            popup2.SetActive(true);
            popup1.SetActive(false);
        }
        else
        {
            popup3.SetActive(true);
            popup1.SetActive(false);
        }
        manager.UIManager.shopMoneyText.text = PlayerPrefs.GetInt("money").ToString();
    }
    public void OnBuyNoButton()
    {
        popup1.SetActive(false);
        manager.dialogManager.ClickDialog(Random.Range(16, 18));
    }

    public void OnSellYesButton()
    {
        string targetStr = parObject.GetComponentInChildren<Text>().text;
        string tempStr = Regex.Replace(targetStr, @"\D", "");
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + int.Parse(tempStr));
        DelMenuNum();
        DelMenu();
        manager.InitSell();
        for (int i = 0; i < parObject.transform.parent.gameObject.transform.childCount; i++)
        {
            if (parObject.transform.parent.gameObject.transform.GetChild(i).name == "ItemImage")
            {
                string spriteName = parObject.transform.parent.gameObject.transform.GetChild(i).GetComponent<Image>().sprite.name;

                WeaponDBData[] WeaponData = manager.shopDB.db.dataArray;
                DBData[] relicData = manager.itemDB.db.dataArray;
                for (int j = 0; j < WeaponData.Length; j++)
                {
                    if (spriteName == WeaponData[j].Code)
                    {
                        Popup5Text(WeaponData[j].Sell);
                        Popup5Image(WeaponData[j].Code);
                    }
                }
                for (int j = 0; j < relicData.Length; j++)
                {
                    if (spriteName == relicData[j].Code)
                    {
                        Popup5Text(relicData[j].Sell);
                        Popup5Image(relicData[j].Code);
                    }
                }
            }
        }
        popup5.SetActive(true);
        popup4.SetActive(false);
        manager.UIManager.shopMoneyText.text = PlayerPrefs.GetInt("money").ToString();
    }
    public void OnSellNoButton()
    {
        popup4.SetActive(false);
    }


    public void OnPopup2OkButton()
    {
        popup2.SetActive(false);
        manager.dialogManager.ClickDialog(Random.Range(14, 16));
    }
    public void OnPopup3OkButton()
    {
        popup3.SetActive(false);
    }
    public void OnPopup5OkButton()
    {
        popup5.SetActive(false);
        manager.dialogManager.ClickDialog(Random.Range(18, 20));
    }


    void DelMenuNum()
    {
        for (int i = 0; i < parObject.transform.parent.gameObject.transform.childCount; i++)
        {
            if (parObject.transform.parent.gameObject.transform.GetChild(i).name == "ItemImage")
            {
                string spriteName = parObject.transform.parent.gameObject.transform.GetChild(i).GetComponent<Image>().sprite.name;
                WeaponDBData[] WeaponData = manager.shopDB.db.dataArray;
                DBData[] relicData = manager.itemDB.db.dataArray;
                for (int j = 0; j < WeaponData.Length; j++)
                {
                    if (spriteName == WeaponData[j].Code)
                    {
                        WeaponData[j].Num--;
                    }
                }
                for (int j = 0; j < relicData.Length; j++)
                {
                    if (spriteName == relicData[j].Code)
                    {
                        relicData[j].Num--;
                    }
                }
            }
        }
    }

    void DelMenu()
    {
        for(int i = 0; i< parObject.transform.parent.gameObject.transform.parent.gameObject.transform.childCount; i++)
        {
            Destroy(parObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void Popup1Image(string tempImage)
    {
        Sprite[] sprites = manager.shopDB.weaponSpriteArr;
        for (int i = 0; i < popup1.transform.childCount; i++)
        {
            if (popup1.transform.GetChild(i).name == "Item")
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    if (tempImage == sprites[j].name)
                    {
                        popup1.transform.GetChild(i).GetComponent<Image>().sprite = sprites[j];
                    }
                }
            }
        }
    }

    void Popup2Text(string tempText)
    {
        for (int i = 0; i < popup2.transform.childCount; i++)
        {
            if (popup2.transform.GetChild(i).name == "NameText")
            {
                popup2.transform.GetChild(i).GetComponent<Text>().text = tempText + "를 획득했습니다.";
            }
        }
    }
    void Popup2Image(string tempImage)
    {
        Sprite[] sprites = manager.shopDB.weaponSpriteArr;
        for (int i = 0; i < popup2.transform.childCount; i++)
        {
            if (popup2.transform.GetChild(i).name == "Item")
            {
                for(int j = 0; j < sprites.Length; j++)
                {
                    if(tempImage == sprites[j].name)
                    {
                        popup2.transform.GetChild(i).GetComponent<Image>().sprite = sprites[j];
                    }
                }
            }
        }
    }

    void Popup4Image(string tempImage)
    {
        Sprite[] sprites = manager.shopDB.weaponSpriteArr;
        GameObject[] spritesOther = manager.itemDB.relicArr;
        for (int i = 0; i < popup4.transform.childCount; i++)
        {
            if (popup4.transform.GetChild(i).name == "Item")
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    if (tempImage == sprites[j].name)
                    {
                        popup4.transform.GetChild(i).GetComponent<Image>().sprite = sprites[j];
                    }
                }
            }
        }
        for (int i = 0; i < popup4.transform.childCount; i++)
        {
            if (popup4.transform.GetChild(i).name == "Item")
            {
                for (int j = 0; j < spritesOther.Length; j++)
                {
                    if (tempImage == spritesOther[j].GetComponent<Image>().sprite.name)
                    {
                        popup4.transform.GetChild(i).GetComponent<Image>().sprite = spritesOther[j].GetComponent<Image>().sprite;
                    }
                }
            }
        }
    }

    public void OnPopup4()
    {
        for (int i = 0; i < parObject.transform.parent.gameObject.transform.childCount; i++)
        {
            if (parObject.transform.parent.gameObject.transform.GetChild(i).name == "ItemImage")
            {
                string spriteName = parObject.transform.parent.gameObject.transform.GetChild(i).GetComponent<Image>().sprite.name;

                WeaponDBData[] WeaponData = manager.shopDB.db.dataArray;
                DBData[] relicData = manager.itemDB.db.dataArray;
                for (int j = 0; j < WeaponData.Length; j++)
                {
                    if (spriteName == WeaponData[j].Code)
                    {
                        Popup4Image(WeaponData[j].Code);
                    }
                }
                for (int j = 0; j < relicData.Length; j++)
                {
                    if (spriteName == relicData[j].Code)
                    {
                        Popup4Image(relicData[j].Code);
                    }
                }
            }
        }
    }

    void Popup5Text(int tempText)
    {
        for (int i = 0; i < popup5.transform.childCount; i++)
        {
            if (popup5.transform.GetChild(i).name == "NameText")
            {
                popup5.transform.GetChild(i).GetComponent<Text>().text = tempText + "장을 받았습니다.";
            }
        }
    }
    void Popup5Image(string tempImage)
    {
        /*
        Sprite[] sprites = manager.shopDB.weaponSpriteArr;
        GameObject[] spritesOther = manager.itemDB.relicArr;
        for (int i = 0; i < popup5.transform.childCount; i++)
        {
            if (popup5.transform.GetChild(i).name == "Item")
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    if (tempImage == sprites[j].name)
                    {
                        popup5.transform.GetChild(i).GetComponent<Image>().sprite = sprites[j];
                    }
                }
            }
        }
        for (int i = 0; i < popup5.transform.childCount; i++)
        {
            if (popup5.transform.GetChild(i).name == "Item")
            {
                for (int j = 0; j < spritesOther.Length; j++)
                {
                    if (tempImage == spritesOther[j].GetComponent<Image>().sprite.name)
                    {
                        popup5.transform.GetChild(i).GetComponent<Image>().sprite = spritesOther[j].GetComponent<Image>().sprite;
                    }
                }
            }
        }
        */

        for (int i = 0; i < popup5.transform.childCount; i++)
        {
            if (popup5.transform.GetChild(i).name == "Item")
            {           
                popup5.transform.GetChild(i).GetComponent<Image>().sprite = manager.moneyIcon;              
            }
        }
    }
}