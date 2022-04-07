using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    public Image[] colorChangeButtons;

    bool isColorChange = false;

    private void Update()
    {
        if (isColorChange)
        {
            for (int i = 0; i < colorChangeButtons.Length; i++)
            {
                colorChangeButtons[i].color = Color.Lerp(colorChangeButtons[i].color, Color.gray, 0.1f);
            }
        }
        else
        {
            for (int i = 0; i < colorChangeButtons.Length; i++)
            {
                colorChangeButtons[i].color = Color.Lerp(colorChangeButtons[i].color, Color.white, 0.1f);
            }
        }
    }

    public void OnButtonDown()
    {
        isColorChange = true;
    }
    public void OnButtonUp()
    {
        isColorChange = false;
    }
}
