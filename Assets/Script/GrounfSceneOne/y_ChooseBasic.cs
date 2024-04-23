using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class y_ChooseBasic : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Slider backSlider;
    private bool isBack = true;


    void SliderZero()
    {
        backSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ShowButtonText();
    }

    public void ShowButtonText()
    {
        GameObject playerBasic = GameManager.instance.GetPlayerBasic();
        if (playerBasic.GetComponent<y_Basic>().leftBasic != null)
        {
            leftButton.GetComponentInChildren<Text>().text = playerBasic.GetComponent<y_Basic>().leftBasic.GetComponent<y_Basic>().basic_kind;
            ShowDetail(leftButton.GetComponentInChildren<Text>());
            leftButton.gameObject.SetActive(true);
        }
        else leftButton.gameObject.SetActive(false);
        if (playerBasic.GetComponent<y_Basic>().rightBasic != null)
        {
            rightButton.GetComponentInChildren<Text>().text = playerBasic.GetComponent<y_Basic>().rightBasic.GetComponent<y_Basic>().basic_kind;
            ShowDetail(rightButton.GetComponentInChildren<Text>());
            rightButton.gameObject.SetActive(true);
        }
        else rightButton.gameObject.SetActive(false);
    }

    public void BackNum()
    {
        if (backSlider.value >= 0.85 && isBack)
        {
            isBack = false;
            GameManager.instance.YangHuiBack();
            Invoke("SliderZero", 0.5f);
        }
        else if (backSlider.value <= 0.2f) isBack = true;
    }

    public void ShowDetail(Text basicText)
    {
        switch (basicText.text)
        {
            case "得与失": basicText.text += "\n失去10个点数,知晓前方左右节点的数值"; break;
            case "陷阱": basicText.text += "\n失去该节点数值一半的点数"; break;
            case "帮助": basicText.text += "\n恢复一些点数或知晓同列随机节点的数值"; break;
            case "无用": basicText.text += "\n你来这里好像没有什么作用"; break;
            default: break;
        }
        return;
    }
}
