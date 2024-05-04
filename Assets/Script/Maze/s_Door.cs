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

    GameObject cube;//������
    NavMeshAgent agent;
    


    bool isRightCollider = true;//�ж��Ƿ�����ȷ����ײ
    bool coroutineRunning = false;//�ж�Э���Ƿ���������

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
                AudioManage.instance.SetClips(ClipSelect.��ȡ����);
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
            AudioManage.instance.SetClips(ClipSelect.��ת);
            //ֹͣ�ƶ�
            agent.gameObject.SetActive(false);
            //�л���ͷ
            //virtualCamer[0].gameObject.SetActive(false);
            GameManager.instance.UI.GetComponent<s_TaskControl_03>().virtualCamer.SetActive(true);
            StartCoroutine(WaitShowUI());
        }
    }

    IEnumerator WaitShowUI()
    {
        yield return new WaitForSeconds(1.5f);
        //��ʾUI
        GameManager.instance.UI.GetComponent<s_TaskControl_03>().Item_UI.SetActive(true);

        //���ô�
        cube.GetComponent<s_Item_03>().outsideLength = outsideLength;
        cube.GetComponent<s_Item_03>().insideLength = insideLength;

        //չʾ�Ի�
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
