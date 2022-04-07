using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuStatus
{
    MAIN = 0, SHOP, PLAY, FIND_ENTER, ADVENTURE_ENTER, COMBO , Collection
}

public class MainMenuUI : MonoBehaviour {

    public GameObject[] menuUIObjects;
    public TileSet tileSet;
    public CollectionUI collection;
    public Text mainMoneyText;
    public Text shopMoneyText;
    private Dictionary<MenuStatus, GameObject> menuController = new Dictionary<MenuStatus, GameObject>();

    public CharacterDialogManager mainDialogManager;
    public CharacterDialogManager shopDialogManager;
    public MenuStatus curMenu;
    public MenuStatus backMenu;
    //private SettingManager settingManager;

    private void initMenu()
    {
        menuController[MenuStatus.MAIN] = menuUIObjects[(int)MenuStatus.MAIN];
        menuController[MenuStatus.SHOP] = menuUIObjects[(int)MenuStatus.SHOP];
        menuController[MenuStatus.PLAY] = menuUIObjects[(int)MenuStatus.PLAY];
        menuController[MenuStatus.FIND_ENTER] = menuUIObjects[(int)MenuStatus.FIND_ENTER];
        menuController[MenuStatus.ADVENTURE_ENTER] = menuUIObjects[(int)MenuStatus.ADVENTURE_ENTER];
        menuController[MenuStatus.COMBO] = menuUIObjects[(int)MenuStatus.COMBO];
        menuController[MenuStatus.Collection] = menuUIObjects[(int)MenuStatus.Collection];
    }

    private void Awake()
    {
        initMenu();
        mainMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        //settingManager = GameObject.FindGameObjectWithTag("Setting").GetComponent<SettingManager>();
    }

    private void Start()
    {
        SetMenu(MenuStatus.MAIN);
        mainDialogManager.ClickDialog(Random.Range(0, 15));
    }

    public void OnMain()
    {
        mainMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        SetMenu(MenuStatus.MAIN);
        mainDialogManager.ClickDialog(Random.Range(0, 15)); // min 이상, max 미만        
    }
    public void OnMainOther() // relic
    {
        mainMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        tileSet.ResetRelic();
        tileSet.DestroyRelicButton();
        SetMenu(MenuStatus.MAIN);
        mainDialogManager.ClickDialog(Random.Range(0, 15));
    }
    public void OnMainOther2() // shop
    {
        mainMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        ShopPopup popup = GameObject.FindGameObjectWithTag("Popup").GetComponent<ShopPopup>();
        popup.popup1.SetActive(false);
        popup.popup2.SetActive(false);
        popup.popup3.SetActive(false);
        popup.popup4.SetActive(false);
        popup.popup5.SetActive(false);
        SetMenu(MenuStatus.MAIN);
        mainDialogManager.ClickDialog(Random.Range(0, 15));
    }
    public void OnMainOther3() // collection
    {
        mainMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        collection.DelPiece();
        collection.DelRelic();
        collection.DelPopup();
        SetMenu(MenuStatus.MAIN);
        mainDialogManager.ClickDialog(Random.Range(0, 15));
    }


    public void OnPlay()
    {
        SetMenu(MenuStatus.PLAY);
    }

    public void OnEnterAdventure()
    {
        SetMenu(MenuStatus.ADVENTURE_ENTER);
    }

    public void OnEnterFind()
    {
        SetMenu(MenuStatus.FIND_ENTER);
    }

    public void OnShop()
    {
        shopMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        SetMenu(MenuStatus.SHOP);
        shopDialogManager.ClickDialog(Random.Range(20, shopDialogManager.dialogSheet.dataArray.Length)); // min 이상, max 미만
    }

    public void SetMenu(MenuStatus changeState)
    {
        foreach (var item in menuController)
        {
            item.Value.SetActive(false);
        }

        menuController[changeState].SetActive(true);
        backMenu = curMenu;
        curMenu = changeState;
    }

    public void OnCombo()
    {
        SetMenu(MenuStatus.COMBO);
        tileSet.InitRelic();
        tileSet.ResetRelicCount();
    }

    public void OnCollection()
    {
        collection.ResetPiece(collection.itemDB.db.dataArray);
        collection.ResetRelic(collection.relicDB.db.dataArray);
        SetMenu(MenuStatus.Collection);
    }
}
