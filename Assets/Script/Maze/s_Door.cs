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
        cube = GameManager.instance.UI.GetComponent<s_UIControl_03>().cube;
    }

    private void Update()
    {
        if (GameManager.instance.UI.GetComponent<s_UIControl_03>().IsRightTriangle && GameManager.instance.UI.GetComponent<s_UIControl_03>().IsRightCube)
        {
            if (!isRightCollider)
            {
                gameObject.SetActive(false);
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isRightCollider)
        {
            return;
        }
        MouseManager.instance.closedMouseControl = true;
        isRightCollider = false;

        if (collision.collider.CompareTag("Player"))
        {
            //停止移动
            agent.gameObject.SetActive(false);
            //切换镜头
            //virtualCamer[0].gameObject.SetActive(false);
            GameManager.instance.UI.GetComponent<s_UIControl_03>().virtualCamer.SetActive(true);
            //显示UI
            GameManager.instance.UI.GetComponent<s_UIControl_03>().Item_UI.SetActive(true);

            //设置答案
            cube.GetComponent<s_Item_03>().outsideLength = outsideLength;
            cube.GetComponent<s_Item_03>().insideLength = insideLength;
        }
    }

    



    private void OnCollisionExit(Collision collision)
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
