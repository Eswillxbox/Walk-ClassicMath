using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class s_TaskControl_03 : MonoBehaviour
{
    public Button back_button;
    public Button help_Button, help_back_Button;
    public GameObject help_panel;

    public GameObject virtualCamer;
    public GameObject Item_UI;
    public GameObject cube;//������
    public GameObject finish;//�������
    public GameObject[] Npcs;//npc
    public Task task;

    NavMeshAgent agent;
    // ����һ��˽�е� NavMeshObstacle ����
    private NavMeshObstacle navMeshObstacle;
    bool isRightTriangle = false;
    bool isRightCube = false;

    public bool IsRightTriangle { get => isRightTriangle; set => isRightTriangle = value; }
    public bool IsRightCube { get => isRightCube; set => isRightCube = value; }
    public NavMeshObstacle NavMeshObstacle { get => navMeshObstacle; set => navMeshObstacle = value; }

    private void Start()
    {
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        NavMeshObstacle = gameObject.AddComponent<NavMeshObstacle>();
        virtualCamer.SetActive(false);
        Item_UI.SetActive(false);
        NavMeshObstacle.carving = true;

        back_button.onClick.AddListener(delegate ()
        {
            AudioManage.instance.SetClips(ClipSelect.ѡ��);
            BackEvent();
        });

        help_Button.onClick.AddListener(delegate ()
        {
            AudioManage.instance.SetClips(ClipSelect.ѡ��);
            help_panel.gameObject.SetActive(true);
        });

        help_back_Button.onClick.AddListener(delegate ()
        {
            AudioManage.instance.SetClips(ClipSelect.ѡ��);
            help_panel.gameObject.SetActive(false);
        });
    }

    public void BackEvent()
    {

        agent.gameObject.SetActive(true);
        navMeshObstacle.enabled = false;

        MouseManager.instance.closedMouseControl = false;
        virtualCamer.SetActive(false);
        Item_UI.SetActive(false);
        StartCoroutine(WaitBackEvent());
        
    }

    IEnumerator WaitBackEvent()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < cube.transform.childCount; i++)
        {
            cube.transform.GetChild(i).gameObject.SetActive(false);

        }

        //���غ����½����ж�
        isRightCube = false;
        isRightTriangle = false;
    }

    public void SetTask()
    {
        switch (task)
        {
            case Task.��ʼ:
                GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(0);
                task++;
                break;
            case Task.��ʼ02:
                GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(1);
                task++;
                break;
            case Task.����:
                switch (cube.GetComponent<s_Item_03>().outsideLength)
                {
                    case 5:
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(2);
                        break;
                    case 13:
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(3);
                        break;
                    case 17:
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(4);
                        break;
                    case 25:
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(5);
                        break;
                }

                if (finish.GetComponent<s_Finish>().finish)
                {
                    GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(6);
                    task++;
                    
                }
                break;
            case Task.����:
                GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(6);
                break;
        }

        GameManager.instance.diaLogDisplay.SetActive(true);
    }
}

public enum Task
{
    ��ʼ,��ʼ02,����,����
}
