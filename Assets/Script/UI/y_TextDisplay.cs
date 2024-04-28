using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class y_TextDisplay : MonoBehaviour
{

    [Header("文本组件")]
    public Text dialogText;
    public Image faceImage;
    public Sprite[] faceImages;

    [Header("文本文件")]
    public TextAsset[] textFile;
    public int textFileIndex = 0;
    public int index;
    public float textSpeed;
    public bool overDisText = false;//文本输出完毕是否消失
    private bool textFinished = true;//文本是否输出完毕
    private bool cancelTyping;//取消延时输出文字
    public int onceDis = 0;//单次文本输出

    private List<string> textList = new List<string>();
    private void Awake()
    {
        GetTextFormFile(textFile[textFileIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        DisPlayText();
        if (this.gameObject.activeSelf == true) MouseManager.instance.closedMouseControl = true;
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
            // case "Y":
            //     //杨辉三角跳转
            //     if (GameManager.instance.IsInYangHui())
            //         GameManager.instance.YangHuiScene(true, 5, 6);
            //     GetTextFormFile(textFile[textFileIndex]);
            //     break;
            // case "EY":
            //     GetTextFormFile(textFile[textFileIndex]);
            //     MouseManager.instance.closedMouseControl = false;
            //     break;
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


    public void GetTextFormFile1(TextAsset file, int n1 = 0, int n2 = 0)
    {
        textList.Clear();
        index = 0;
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            string str = line;
            textList.Add(str);
        }
    }

    private void DisPlayText()
    {
        if (Input.GetMouseButtonDown(0) && index == textList.Count && !overDisText)
        {
            gameObject.SetActive(false);
            if (textFileIndex < textFile.Length - 1)
                GetTextFormFile(textFile[++textFileIndex]);
            MouseManager.instance.closedMouseControl = false;

            if (GameManager.instance.UI.GetComponent<s_UIControl>() != null)
            {
                if (GameManager.instance.UI.GetComponent<s_UIControl>().TaskProgress == TaskProgress.捡拾算筹 && !GameManager.instance.UI.GetComponent<s_UIControl>().IsFirstDialog)
                {
                    GameObject[] Npcs =  GameManager.instance.UI.GetComponent<s_UIControl>().Npcs;
                    Npcs[1].SetActive(true);
                    Npcs[0].SetActive(false);
                }
            }


            MouseManager.instance.closedMouseControl = false;
            index = 0;
            return;
        }
        else if (Input.GetMouseButtonDown(0) && index == textList.Count && overDisText)
        {
            if (onceDis == 1)
            {
                onceDis++;
                gameObject.SetActive(false);
            }
            if (GetComponent<y_YangHuiDisplay>() != null)
            {
                GetComponent<y_YangHuiDisplay>().ShowButton(true);
            }
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

    public void SetTextFile(int index)
    {
        textFileIndex = index;
        GetTextFormFile(textFile[textFileIndex]);
    }
}
