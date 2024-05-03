using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class s_UIControl_03 : MonoBehaviour
{
    public Button back_button;

    public GameObject virtualCamer;
    public GameObject Item_UI;
    public GameObject cube;//正方形
    NavMeshAgent agent;
    bool isRightTriangle = false;
    bool isRightCube = false;

    public bool IsRightTriangle { get => isRightTriangle; set => isRightTriangle = value; }
    public bool IsRightCube { get => isRightCube; set => isRightCube = value; }

    private void Start()
    {
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        virtualCamer.SetActive(false);
        Item_UI.SetActive(false);

        back_button.onClick.AddListener(delegate ()
        {
            BackEvent();
        });
    }

    void BackEvent()
    {

        agent.gameObject.SetActive(true);
        MouseManager.instance.closedMouseControl = false;
        virtualCamer.SetActive(false);
        Item_UI.SetActive(false);
        for (int i = 0; i < cube.transform.childCount; i++)
        {
            cube.transform.GetChild(i).gameObject.SetActive(false);

        }

        //返回后重新进行判断
        isRightCube = false;
        isRightTriangle = false;
    }
}
