using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_PythagoreanData : MonoBehaviour
{
  //单例
    public static s_PythagoreanData instance;

  //其他类调用
    //所有的用于替换的预制体
    public MeshFilter[] prefabs;//顺序： b-a,ab,c,线
    //玩家子物体
    bool playerChild_isActive;


  //待存储的数据
    //玩家数据存储
    public int playerChild_data = -1;//-1，失活，顺序： b-a,ab,c,线（0，1,2,3）
    public int puzzle_1 = 3;
    public int puzzle_2 = 3;
    public int puzzle_3 = 3;



    GameObject player;
    Transform playerChildTransform;//玩家第一个子物体

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

    //初始化数据，主要用于数据存储
    void InitData()
    {
        //获取到玩家及子物体
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerChildTransform = player.transform.GetChild(0);
        }



        //场景初始化，为-1设置为false
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


    //判断玩家子物体是否失活
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


    //交换存储的数据
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

    //得到存储的算式空的mesh
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
