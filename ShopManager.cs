using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public GameObject buyContentObject;
    public GameObject sellContentObject;
    public GameObject itemPrefab;
    public ShopDB shopDB;
    public ItemDB itemDB;
    public ShopPopup shopPopup;
    public GameObject buyCheck;
    public GameObject sellCheck;

    public GameObject buySee;
    public GameObject sellSee;
    public Sprite moneyIcon;
    public CharacterDialogManager dialogManager;
    public MainMenuUI UIManager;

    WeaponDBData[] weaponData;
    DBData[] relicData;
    Button button;

    private void Awake()
    {
        weaponData = shopDB.db.dataArray;
        relicData = itemDB.db.dataArray;

        InitBuy();
        InitSell();
    }

    public void InitBuy()
    {
        for (int i = 0; i < weaponData.Length; i++)
        {
            GameObject itemObject = Instantiate<GameObject>(itemPrefab, buyContentObject.transform);
            for (int j = 0; j < itemObject.transform.childCount; j++)
            {
                if (itemObject.transform.GetChild(j).name == "ItemImage")
                {
                    foreach (Sprite weapon in shopDB.weaponSpriteArr)
                    {
                        if (weapon.name == weaponData[i].Code)
                        {
                            itemObject.transform.GetChild(j).GetComponent<Image>().sprite = weapon;
                        }
                    }
                }
                else if (itemObject.transform.GetChild(j).name == "NameBack")
                {
                    itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = weaponData[i].Name;
                }
                else if (itemObject.transform.GetChild(j).name == "EffectBack")
                {
                    itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = weaponData[i].Effectmore;
                }
                else if (itemObject.transform.GetChild(j).name == "BuyBack")
                {
                    itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = weaponData[i].Buy.ToString();
                    button = itemObject.transform.GetChild(j).GetComponent<Button>();
                    button.onClick.AddListener(OnBuyButton);
                }
                else if (itemObject.transform.GetChild(j).name == "Buy")
                {
                    itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = "구매";
                }
            }
        }
    }
    public void DelBuy()
    {
        for(int i = 0;i< buyContentObject.transform.childCount; i++)
        {
            Destroy(buyContentObject.transform.GetChild(i).gameObject);
        }
    }
    public void InitSell()
    {
        for (int i = 0; i < weaponData.Length; i++)
        {
            if (weaponData[i].Num > 0)
            {
                GameObject itemObject = Instantiate<GameObject>(itemPrefab, sellContentObject.transform);
                for (int j = 0; j < itemObject.transform.childCount; j++)
                {
                    if (itemObject.transform.GetChild(j).name == "ItemImage")
                    {
                        foreach (Sprite weapon in shopDB.weaponSpriteArr)
                        {
                            if (weapon.name == weaponData[i].Code)
                            {
                                itemObject.transform.GetChild(j).GetComponent<Image>().sprite = weapon;
                            }
                        }
                    }
                    else if (itemObject.transform.GetChild(j).name == "NameBack")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = weaponData[i].Name;
                    }
                    else if (itemObject.transform.GetChild(j).name == "EffectBack")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = weaponData[i].Effectmore;
                    }
                    else if (itemObject.transform.GetChild(j).name == "BuyBack")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = weaponData[i].Sell.ToString();
                        button = itemObject.transform.GetChild(j).GetComponent<Button>();
                        button.onClick.AddListener(OnSellButton);
                    }
                    else if (itemObject.transform.GetChild(j).name == "Buy")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = "판매";
                    }
                }
            }
        }

        for (int i = 0; i < relicData.Length; i++)
        {
            if (relicData[i].Num > 0)
            {
                GameObject itemObject = Instantiate<GameObject>(itemPrefab, sellContentObject.transform);
                for (int j = 0; j < itemObject.transform.childCount; j++)
                {
                    if (itemObject.transform.GetChild(j).name == "ItemImage")
                    {
                        foreach (GameObject relic in itemDB.relicArr)
                        {
                            if (relic.name == relicData[i].Code)
                            {
                                itemObject.transform.GetChild(j).GetComponent<Image>().sprite = relic.GetComponent<Image>().sprite;
                            }
                        }
                    }
                    else if (itemObject.transform.GetChild(j).name == "NameBack")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = relicData[i].Name;
                    }
                    else if (itemObject.transform.GetChild(j).name == "EffectBack")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = "";
                    }
                    else if (itemObject.transform.GetChild(j).name == "BuyBack")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = relicData[i].Sell.ToString();
                        button = itemObject.transform.GetChild(j).GetComponent<Button>();
                        button.onClick.AddListener(OnSellButton);
                    }
                    else if (itemObject.transform.GetChild(j).name == "Buy")
                    {
                        itemObject.transform.GetChild(j).GetComponentInChildren<Text>().text = "판매";
                    }
                }
            }
        }
    }
    public void DelSell()
    {
        for (int i = 0; i < sellContentObject.transform.childCount; i++)
        {
            Destroy(sellContentObject.transform.GetChild(i).gameObject);
        }
    }

    public void OnBuyButton()
    {
        buyCheck.SetActive(true);
        shopPopup.parObject = EventSystem.current.currentSelectedGameObject;
        for(int i = 0;i< EventSystem.current.currentSelectedGameObject.transform.parent.transform.childCount; i++)
        {
            if(EventSystem.current.currentSelectedGameObject.transform.parent.transform.GetChild(i).name == "ItemImage")
            {
                shopPopup.Popup1Image(EventSystem.current.currentSelectedGameObject.transform.parent.transform.GetChild(i).GetComponent<Image>().sprite.name);
            }
        }
    }
    public void OnSellButton()
    {
        sellCheck.SetActive(true);
        shopPopup.parObject = EventSystem.current.currentSelectedGameObject;
        shopPopup.OnPopup4();
    }

    public void OnBuySeeButton()
    {
        buySee.SetActive(true);
        DelBuy();
        InitBuy();
        sellSee.SetActive(false);
    }
    public void OnSellSeeButton()
    {
        sellSee.SetActive(true);
        DelSell();
        InitSell();
        buySee.SetActive(false);
    }
}
