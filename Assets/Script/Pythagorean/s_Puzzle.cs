using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.判断玩家身上是否为激活，失活则打印请去获取道具吧
/// 2.判断是否是线
///     a.当是线的时候，不为空，将线换成玩家头上的物体，即putdown
///     b.有字符的时候(不是线的时候)，与玩家身上的物体进行更换,即change
/// 3.正确字符闪光特效
/// 
/// </summary>


public class s_Puzzle : MonoBehaviour
{

    public int type;


    [SerializeField ()]
    bool isActive;//true 放下，false 则更换
    bool isLineOfChild = true;//子物体是否是线


    GameObject player;
    GameObject childGameObject;

    //记录子物体线的位置
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
        //判断当答案满足要求的时候，此时该算式不进行任何操作
        if (JudgeAnswer())
        {
            return;
        }

        //玩家没到指定位置，不执行任何操作
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

        //初始化算式内容
        childGameObject.GetComponent<MeshFilter>().mesh = PythagoreanData.instance.GetPuzzleData_Mesh(type).sharedMesh;
    }


    //判断当更换的数据一致的时候，此时该算式不进行任何操作
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
                //没激活，打印请获取道具吧
                print("请获取道具吧!");
                return;
            }

            Mesh_Exchange();
            if (isLineOfChild)
            {
                //激活但是算式是线，则放下玩家子物体，玩家物体失活
                player.transform.GetChild(0).gameObject.SetActive(false);
                isActive = false;//更改判断条件
            }


            
        }
    }


    //交换mesh
    void Mesh_Exchange()
    {
        //交换用于存储的数据
        PythagoreanData.instance.Exchange_PuzzleAndPlayerChildData(type);

        //交换玩家和道具mesh
        Mesh tempMesh;
        tempMesh = player.transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        player.transform.GetChild(0).GetComponent<MeshFilter>().mesh =
            childGameObject.transform.GetComponent<MeshFilter>().mesh;
        childGameObject.transform.GetComponent<MeshFilter>().mesh = tempMesh;

        SetChildTransform();
    }

    void SetChildTransform()
    {

        //判断当前展示的是否是线
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
            //判断玩家身上是否有子物体字母
            isActive = PythagoreanData.instance.JudgePlayerChilde_Active();
        }

        //判断物体是否是线
        if (transform.GetChild(0).name.Contains("线"))
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


   
