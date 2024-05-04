using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;

public class s_Door : MonoBehaviour
{
    
    public int outsideLength;
    public int insideLength;

    GameObject cube;//正方形
    NavMeshAgent agent;
    


    bool isRightCollider = true;//判断是否是正确的碰撞
    bool coroutineRunning = false;//判断协程是否正在运行

    private void Start()
    {
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        
        cube = GameManager.instance.UI.GetComponent<s_TaskControl_03>().cube;

        
    }

    private void Update()
    {
        if (GameManager.instance.UI.GetComponent<s_TaskControl_03>().IsRightTriangle && GameManager.instance.UI.GetComponent<s_TaskControl_03>().IsRightCube)
        {
            if (!isRightCollider)
            {
                AudioManage.instance.SetClips(ClipSelect.获取道具);
                gameObject.SetActive(false);
                GameManager.instance.UI.GetComponent<s_TaskControl_03>().BackEvent();
                GameManager.instance.UI.GetComponent <s_TaskControl_03>().NavMeshObstacle.enabled = false;
            }

        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (!isRightCollider)
        {
            return;
        }
        MouseManager.instance.closedMouseControl = true;
        isRightCollider = false;

        if (other.CompareTag("Player"))
        {
            AudioManage.instance.SetClips(ClipSelect.跳转);
            //停止移动
            agent.gameObject.SetActive(false);
            //切换镜头
            //virtualCamer[0].gameObject.SetActive(false);
            GameManager.instance.UI.GetComponent<s_TaskControl_03>().virtualCamer.SetActive(true);
            StartCoroutine(WaitShowUI());
        }
    }

    IEnumerator WaitShowUI()
    {
        yield return new WaitForSeconds(1.5f);
        //显示UI
        GameManager.instance.UI.GetComponent<s_TaskControl_03>().Item_UI.SetActive(true);

        //设置答案
        cube.GetComponent<s_Item_03>().outsideLength = outsideLength;
        cube.GetComponent<s_Item_03>().insideLength = insideLength;

        //展示对话
        switch (outsideLength)
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
        GameManager.instance.diaLogDisplay.SetActive(true);
    }





    private void OnTriggerExit(Collider other)
    {
        if (!isRightCollider && !coroutineRunning)
        {
            StartCoroutine(WaitChange());
            coroutineRunning = true;
        }
    }

    IEnumerator WaitChange()
    {
        yield return new WaitForSeconds(1.5f);
        coroutineRunning = false;
        isRightCollider = true;
    }
}
