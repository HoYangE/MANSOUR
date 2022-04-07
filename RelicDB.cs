using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDB : MonoBehaviour
{
    public Relic db;
    RelicData[] data;

    public ArrayList list = new ArrayList();
    public GameObject[] relicArr;

    private void Awake()
    {
        data = db.dataArray;
    }

    public string GetDataCode(int num)
    {
        return data[num].Code;
    }
    public int GetCount(int num)
    {
        return data[num].Num;
    }
    public void SetCount(int num, int count)
    {
        data[num].Num = count;
    }
}
