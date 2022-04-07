using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDB : MonoBehaviour
{
    public WeaponDB db;
    WeaponDBData[] data;

    public Sprite[] weaponSpriteArr;

    private void Awake()
    {
        data = db.dataArray;
    }

    public string GetDataName(int num)
    {
        return data[num].Name;
    }
}
