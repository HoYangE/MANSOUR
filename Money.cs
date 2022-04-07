using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int startMoney = 100;

    private void Awake()
    {
        PlayerPrefs.SetInt("money", startMoney);    
    }
}
