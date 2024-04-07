using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_PythagoreanData : MonoBehaviour
{
  //����
    public static s_PythagoreanData instance;

  //���������
    //���е������滻��Ԥ����
    public MeshFilter[] prefabs;//˳�� b-a,ab,c,��
    //���������
    bool playerChild_isActive;


  //���洢������
    //������ݴ洢
    public int playerChild_data = -1;//-1��ʧ�˳�� b-a,ab,c,�ߣ�0��1,2,3��
    public int puzzle_1 = 3;
    public int puzzle_2 = 3;
    public int puzzle_3 = 3;



    GameObject player;
    Transform playerChildTransform;//��ҵ�һ��������

    private void Awake()
    {
        instance = this;
        InitData();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��ʼ�����ݣ���Ҫ�������ݴ洢
    void InitData()
    {
        //��ȡ����Ҽ�������
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerChildTransform = player.transform.GetChild(0);
        }



        //������ʼ����Ϊ-1����Ϊfalse
        if (playerChild_data == -1)
        {
            playerChild_isActive = false;
        }
        else
        {
            playerChild_isActive = true;    
            playerChildTransform.GetComponent<MeshFilter>().mesh = prefabs[playerChild_data].mesh;
        }
        playerChildTransform.gameObject.SetActive(playerChild_isActive);
    }


    //�ж�����������Ƿ�ʧ��
    public bool JudgePlayerChilde_Active()
    {
        if (playerChildTransform.gameObject.activeSelf == true)
        {
            playerChild_isActive = true;
        }
        else
        {
            playerChild_isActive = false;
        }
        return playerChild_isActive;
    }


    //�����洢������
    public void Exchange_PuzzleAndPlayerChildData(int type)
    {
        int temp = playerChild_data;
        switch (type)
        {
            case 0:
                playerChild_data = puzzle_1;
                puzzle_1 = temp;
                break;
            case 1:
                playerChild_data = puzzle_2;
                puzzle_2 = temp;
                break;
            case 2:
                playerChild_data = puzzle_3;
                puzzle_3 = temp;
                break;
        }
    }

    public int GetPuzzleData(int i)
    {
        switch (i)
        {
            case 0: return puzzle_1;
                case 1: return puzzle_2;
                case 2: return puzzle_3;
                default: return -1;
                
        }


    }

    //�õ��洢����ʽ�յ�mesh
    public MeshFilter GetPuzzleData_Mesh(int i)
    {
        switch (i)
        {
            case 1:
                return prefabs[puzzle_1];
            case 2:
                return prefabs[puzzle_2];
            case 3:
                return prefabs[puzzle_3];
             default: return prefabs[3];
        }
        
    }

}
