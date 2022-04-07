using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicPowerUp : MonoBehaviour
{
    public Image itemImage;
    public Text curText;
    public Text nowText;
    public Text costText;

    string curStringName;

    void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.tag == "Treasure")
            {
                itemImage.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
                if(itemImage.sprite.name == "Treasure_05")
                {
                    curText.text = "HP가 다 떨어질 시 1회 부활";
                    nowText.text = "HP가 다 떨어질 시 2회 부활";
                    costText.text = "500";
                }
                else if(itemImage.sprite.name == "Treasure_01")
                {
                    curText.text = "전방 10M 앞을 일직선으로 1초동안 데미지 70의 레이저 빔을 쏜다";
                    nowText.text = "전방 10M 앞을 일직선으로 1초동안 데미지 120의 레이저 빔을 쏜다";
                    costText.text = "500";
                }
            }
        }
	}
}
