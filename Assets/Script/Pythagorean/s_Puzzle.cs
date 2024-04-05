using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.�ж���������Ƿ�Ϊ���ʧ�����ӡ��ȥ��ȡ���߰�
/// 2.�ж��Ƿ�����
///     a.�����ߵ�ʱ�򣬲�Ϊ�գ����߻������ͷ�ϵ����壬��putdown
///     b.���ַ���ʱ��(�����ߵ�ʱ��)����������ϵ�������и���,��change
/// 3.��ȷ�ַ�������Ч
/// 
/// </summary>


public class s_Puzzle : MonoBehaviour
{

    public int type;


    [SerializeField ()]
    bool isActive;//true ���£�false �����
    bool isLineOfChild = true;//�������Ƿ�����


    GameObject player;
    GameObject childGameObject;

    //��¼�������ߵ�λ��
    Vector3 childPosition;

    // Start is called before the first frame update
    private void Start()
    {
        childGameObject = transform.GetChild(0).gameObject;
        childPosition = childGameObject.transform.localPosition;
        InitData();
    }


    private void Update()
    {
        //�жϵ�������Ҫ���ʱ�򣬴�ʱ����ʽ�������κβ���
        if (JudgeAnswer())
        {
            return;
        }

        //���û��ָ��λ�ã���ִ���κβ���
        if (player == null)
        {
            return;
        }
        //print(111);
        PutDown();
    }

    void InitData()
    {
        isActive = PythagoreanData.instance.JudgePlayerChilde_Active();

        //��ʼ����ʽ����
        childGameObject.GetComponent<MeshFilter>().mesh = PythagoreanData.instance.GetPuzzleData_Mesh(type).sharedMesh;
    }


    //�жϵ�����������һ�µ�ʱ�򣬴�ʱ����ʽ�������κβ���
    bool JudgeAnswer()
    {
        int puzzleData = PythagoreanData.instance.GetPuzzleData(type);
        if (puzzleData != -1 && puzzleData == type)
        {
            return true;
        }
        return false;
    }



    void PutDown()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isActive)
            {
                //û�����ӡ���ȡ���߰�
                print("���ȡ���߰�!");
                return;
            }

            Mesh_Exchange();
            if (isLineOfChild)
            {
                //�������ʽ���ߣ��������������壬�������ʧ��
                player.transform.GetChild(0).gameObject.SetActive(false);
                isActive = false;//�����ж�����
            }


            
        }
    }


    //����mesh
    void Mesh_Exchange()
    {
        //�������ڴ洢������
        PythagoreanData.instance.Exchange_PuzzleAndPlayerChildData(type);

        //������Һ͵���mesh
        Mesh tempMesh;
        tempMesh = player.transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        player.transform.GetChild(0).GetComponent<MeshFilter>().mesh =
            childGameObject.transform.GetComponent<MeshFilter>().mesh;
        childGameObject.transform.GetComponent<MeshFilter>().mesh = tempMesh;

        SetChildTransform();
    }

    void SetChildTransform()
    {

        //�жϵ�ǰչʾ���Ƿ�����
        if (PythagoreanData.instance.GetPuzzleData(type) == 3)
        {
            childGameObject.transform.localPosition = childPosition;
            childGameObject.transform.localScale = new Vector3(3,1,1);
        }
        else
        {
            childGameObject.transform.localPosition = new Vector3(childPosition.x,0, childPosition.z);
            childGameObject.transform.localScale = Vector3.one;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            //�ж���������Ƿ�����������ĸ
            isActive = PythagoreanData.instance.JudgePlayerChilde_Active();
        }

        //�ж������Ƿ�����
        if (transform.GetChild(0).name.Contains("��"))
            {
                isLineOfChild = true;
            }
            else
            {
                isLineOfChild = false;
            }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}


   
