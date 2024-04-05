using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Null : MonoBehaviour
{
    public int type;//��Ϊ���ߵı�ʶ



    bool PickUp_Change = false;//true �����false �����
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
            //����
            Mesh_Exchange();
            if (PickUp_Change)
            {
                //����Ļ����ٵ����������
                Destroy(gameObject);
                PickUp_Change = false;
            }
        }
    }

    //����mesh
    void Mesh_Exchange()
    {
        player.transform.GetChild(0).gameObject.SetActive(true);
        Mesh tempMesh;
        //������Һ͵���mesh
        tempMesh = player.transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        player.transform.GetChild(0).GetComponent<MeshFilter>().mesh =
            transform.GetComponent<MeshFilter>().mesh;
        transform.GetComponent<MeshFilter>().mesh = tempMesh;


        //����������洢����
        PythagoreanData.instance.playerChild_data = type;
        
    }






    //��������ʾ���Լ�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            //�ж���������Ƿ�����������ĸ
            PickUp_Change = !PythagoreanData.instance.JudgePlayerChilde_Active();
        }
    }

    //�뿪�����ܽ��в���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
