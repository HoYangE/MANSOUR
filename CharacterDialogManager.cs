using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogManager : MonoBehaviour
{
    public GameObject root;

    public CharacterDialog dialogSheet;

    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private Image faceImage;

    private CharacterDialogData[] dialogData;
    public Sprite[] faceBundle;

    [SerializeField]
    private int key;

    private bool isTyping;

    private Coroutine typingTask;

    private void Awake()
    {
        //key = -1;
        isTyping = false;
        dialogData = dialogSheet.dataArray;
    }

    private void Start()
    {
       // ClickDialog();
    }

    private IEnumerator TypingText(int key)
    {
        isTyping = true;

        string sentence = dialogData[key].Text;

        foreach (var item in sentence)
        {
            if (isTyping) dialogText.text += item;
            yield return new WaitForSeconds(0.08f);
        }

        isTyping = false;
    }

    public void ClickDialog(int key)
    {
        // When Users click the screen, while each letter is displayed one by one,
        // you can see all the letters immediately
        if (isTyping)
        {
            if (key != 0) StopCoroutine(typingTask);

            // Erase remaining characters
            dialogText.text = "";
            // Show dialog text 'immediately'
            dialogText.text = dialogData[key].Text;
            // Do not allow typing in coroutine - "TypingText"
            isTyping = false;

            return;
        }
        else
        {
            // Erase remaining characters
            dialogText.text = "";
            // Show face sprite
            faceImage.sprite = faceBundle[dialogData[key].Face];
            // Show dialog text
            typingTask = StartCoroutine(TypingText(key));
        }
    }
}
