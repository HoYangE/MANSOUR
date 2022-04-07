using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public DB db;
    DBData[] data;

    public ArrayList list = new ArrayList();
    public GameObject[] relicArr;

    public GameObject tile;

    private void Awake()
    {
        data = db.dataArray;
    }

    void Start ()
    {
        
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
