using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_TextDisplay : y_TextDisplay
{
    [Header("�ı����")]
    public Text dialogText;
    public Image faceImage;
    public Sprite[] faceImages;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;
    private bool textFinished = true;//�ı��Ƿ�������
    private bool cancelTyping;//ȡ����ʱ�������
    private List<string> textList = new List<string>();
    private void Awake()
    {
        GetTextFormFile(textFile);
    }

    // Update is called once per frame
    void Update()
    {
        DisPlayText();
    }

    private void OnEnable()
    {
        dialogText.text = textList[index];
        StartCoroutine("SetDialogText");
    }

    IEnumerator SetDialogText()
    {
        textFinished = false;
        dialogText.text = "";
        switch (textList[index].Trim().ToString())
        {
            case "A":
                faceImage.sprite = faceImages[0];
                index++;
                break;
            case "B":
                faceImage.sprite = faceImages[1];
                index++;
                break;
            default: break;
        }
        // for (int i = 0; i < textList[index].Length; i++)
        // {
        //     dialogText.text += textList[index][i];
        //     yield return new WaitForSeconds(textSpeed);
        // }
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            dialogText.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        dialogText.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }

    private void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    private void DisPlayText()
    {
        if (Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            gameObject.SetActive(false);
            MouseManager.instance.setUp = false;
            index = 0;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (textFinished)
                StartCoroutine("SetDialogText");
            else if (!textFinished)
                cancelTyping = !cancelTyping;
        }


    }
}
