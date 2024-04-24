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
    private bool textFinished = true;//文本是否输出完毕
    private bool cancelTyping;//取消延时输出文字
    private List<string> textList = new List<string>();
    private void Awake()
    {
        GetTextFormFile(textFile[textFileIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        DisPlayText();
        if (this.gameObject.activeSelf == true) MouseManager.instance.setUp = true;
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
            case "Y":
                //杨辉三角跳转
                if (GameManager.instance.IsInYangHui())
                    GameManager.instance.YangHuiScene(true);
                gameObject.SetActive(false);
                break;
            case "EY":
                //退出杨辉三角
                if (GameManager.instance.IsInYangHui())
                    GameManager.instance.ExitYangHuiScene();
                gameObject.SetActive(false);
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


    public void GetTextFormFile1(TextAsset file, int n1 = 0, int n2 = 0)
    {
        textList.Clear();
        index = 0;
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            string str = line;
            //判断当前是否是算式
            if (line.Contains("="))
            {
                str = string.Format(str, n1, n2);
            }
            textList.Add(str);
        }
    }

    private void DisPlayText()
    {
        if (Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            gameObject.SetActive(false);
            if (textFileIndex < textFile.Length - 1)
                GetTextFormFile(textFile[++textFileIndex]);
            MouseManager.instance.setUp = false;
            //不为这个场景
            if (SceneManager.GetActiveScene().name.CompareTo("GrounfSceneOne") == -1)
            {
                MouseManager.instance.setUp = false;
            }

            if (SceneManager.GetActiveScene().name.CompareTo("GrounfSceneOne") > -1)
            {
                if (s_Item_UI.instance != null)
                    s_Item_UI.instance.isShowNewFormula = true;
            }
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

    public void SetTextFile(int index)
    {
        textFileIndex = index;
        GetTextFormFile(textFile[textFileIndex]);
    }
}
