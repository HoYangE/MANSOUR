using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicManager : MonoBehaviour
{
    public GameObject relicButtonParent;
    public GameObject relicDBResources;
    public RelicDB relicDB;

    Queue<string> notRelicDBZeroCountName = new Queue<string>();

    private void Awake()
    {
        ResetRelicCount();
        InitRelic();
    }

    public void ResetRelicCount()
    {
        int childCount = relicButtonParent.transform.childCount;
        RelicData[] datas;
        datas = relicDB.db.dataArray;

        float parX = relicButtonParent.transform.position.x;
        float parY = relicButtonParent.transform.position.y;
        float parWid = relicButtonParent.GetComponent<RectTransform>().rect.width;
        float parHei = relicButtonParent.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < childCount; i++)
        {
            for (int j = 0; j < datas.Length; j++)
            {
                if (relicButtonParent.transform.GetChild(i).GetComponent<Image>().sprite.name == relicDB.GetDataCode(j))
                {
                    if (relicDB.GetCount(j) != 0)
                    {
                        if (relicDB.list.Contains(relicButtonParent.transform.GetChild(i).gameObject) == false)
                        {
                            relicDB.list.Add(relicButtonParent.transform.GetChild(i).gameObject);
                        }
                        relicButtonParent.transform.GetChild(i).GetComponentInChildren<Text>().text = relicDB.GetCount(j).ToString();
                        notRelicDBZeroCountName.Enqueue(relicDB.GetDataCode(j));
                    }
                    else
                    {
                        // 0개일때
                        relicDB.list.Remove(relicButtonParent.transform.GetChild(i).gameObject);
                        Destroy(relicButtonParent.transform.GetChild(i).gameObject);
                    }
                }
            }
        }
    }        

    public void InitRelic()
    {
        RelicData[] datas;
        datas = relicDB.db.dataArray;
        int next = 0;
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].Num > 0)
            {
                for (int j = 0; j < datas.Length; j++)
                {
                    if (datas[i].Code == relicDB.relicArr[j].GetComponent<Image>().sprite.name)
                    {
                        GameObject instanRelic = Instantiate<GameObject>(relicDB.relicArr[j], relicButtonParent.transform);
                        instanRelic.SetActive(true);
                    }
                }
            }
            else
            {
                next++;
            }
        }
    }

    public void DelRelic()
    {
        for(int i = 0;i< relicButtonParent.transform.childCount; i++)
        {
            Destroy(relicButtonParent.transform.GetChild(i).gameObject);
        }
    }
}
