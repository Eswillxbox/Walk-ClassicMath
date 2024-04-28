using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class y_YangHuiDisplay : MonoBehaviour
{
    public Button startButton;
    public Button backButton;
    public GameObject chooseButtons;
    public Button[] chooseButtonArray;
    public int textFileIndex;
    void Start()
    {
        ShowButton(false);
    }

    public void ShowButton(bool isShow)
    {
        startButton.gameObject.SetActive(isShow);
        backButton.gameObject.SetActive(isShow);
    }

    public void StartLevel()
    {
        chooseButtons.SetActive(true);
    }

    public void BackLevel()
    {
        this.gameObject.SetActive(false);
        MouseManager.instance.closedMouseControl = false;
        GetComponent<y_TextDisplay>().SetTextFile(1);
        GetComponent<y_TextDisplay>().index = 0;
    }

    public void chooseLevel(int level)
    {
        switch (level)
        {
            case 0:
                ShowButton(false);
                if (GameManager.instance.IsInYangHui())
                    GameManager.instance.YangHuiScene(true, 5, 6);
                GetComponent<y_TextDisplay>().index = 0;
                GetComponent<y_TextDisplay>().SetTextFile(2);
                GetComponent<y_TextDisplay>().onceDis++;
                break;
            case 1:
                if (GameManager.instance.IsInYangHui())
                    GameManager.instance.YangHuiScene(true, 7, 20);
                GetComponent<y_TextDisplay>().SetTextFile(1); break;
            case 2:
                if (GameManager.instance.IsInYangHui())
                    GameManager.instance.YangHuiScene(true, 10, 126);
                GetComponent<y_TextDisplay>().SetTextFile(1); break;
            case 3:
                if (GameManager.instance.IsInYangHui())
                    GameManager.instance.YangHuiScene(true, 15, 126);
                GetComponent<y_TextDisplay>().SetTextFile(1); break;
            default: break;
        }
        chooseButtons.SetActive(false);
        if (GetComponent<y_TextDisplay>().onceDis != 1) BackLevel();
        MouseManager.instance.closedMouseControl = true;
    }

    public void OnEnableChooseButton(int i)
    {
        chooseButtonArray[i].gameObject.SetActive(true);
    }
}
