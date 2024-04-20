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
            leftButton.gameObject.SetActive(true);
        }
        else leftButton.gameObject.SetActive(false);
        if (playerBasic.GetComponent<y_Basic>().rightBasic != null)
        {
            rightButton.GetComponentInChildren<Text>().text = playerBasic.GetComponent<y_Basic>().rightBasic.GetComponent<y_Basic>().basic_kind;
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
}
