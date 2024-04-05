using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Null : MonoBehaviour
{
    public int type;//作为道具的标识



    bool PickUp_Change = false;//true 则捡起，false 则更换
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        PickUp();
    }

    


    void PickUp()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            //交换
            Mesh_Exchange();
            if (PickUp_Change)
            {
                //捡起的话销毁掉地面的物体
                Destroy(gameObject);
                PickUp_Change = false;
            }
        }
    }

    //交换mesh
    void Mesh_Exchange()
    {
        player.transform.GetChild(0).gameObject.SetActive(true);
        Mesh tempMesh;
        //交换玩家和道具mesh
        tempMesh = player.transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        player.transform.GetChild(0).GetComponent<MeshFilter>().mesh =
            transform.GetComponent<MeshFilter>().mesh;
        transform.GetComponent<MeshFilter>().mesh = tempMesh;


        //更换子物体存储数据
        PythagoreanData.instance.playerChild_data = type;
        
    }






    //碰触到显示可以捡起来
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            //判断玩家身上是否有子物体字母
            PickUp_Change = !PythagoreanData.instance.JudgePlayerChilde_Active();
        }
    }

    //离开，不能进行操作
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
