using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Yanghui : MonoBehaviour
{
    [Header("棋盘基座")]
    public GameObject player_Basic;
    public Stack<GameObject> back_Basic;
    public int basicNum;
    private Vector3[,] basic_position;
    void Start()
    {
        CreateBasic();
        GameManager.instance.GetYangHui(this.gameObject);
        back_Basic = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateBasic()
    {
        //一个二元数组用来记录基座的坐标
        basic_position = new Vector3[basicNum, basicNum];
        basic_position[0, 0] = player_Basic.transform.position;
        for (int i = 1; i < basicNum; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (basic_position[i - 1, j] != Vector3.zero)
                    basic_position[i, j] = basic_position[i - 1, j] + new Vector3(1.8f, 0f, -1.2f);
                else
                    basic_position[i, j] = basic_position[i - 1, j - 1] + new Vector3(1.8f, 0f, 1.2f);
            }
        }
        //用队列处理设置左右基座
        // 1 4 6 4 1
        //  1 3 3 1
        //   1 2 1  
        //    1 1         ^
        //     1       <--|  方向从右往左，从下往上
        Queue<GameObject> basicQueue = new Queue<GameObject>();
        GameObject basic_Temp = player_Basic;
        GameObject temp;
        int[,] numArray = new int[basicNum, basicNum];
        numArray[0, 0] = 1;
        for (int i = 1; i < basicNum; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                //生成后拿到指针
                temp = Instantiate(player_Basic, basic_position[i, j], player_Basic.transform.rotation, this.transform);
                //最后一排不录入队列，也不能因复制首个基座得到左右节点
                if (i != basicNum - 1) basicQueue.Enqueue(temp);
                else
                {
                    temp.GetComponent<y_Basic>().leftBasic = null;
                    temp.GetComponent<y_Basic>().rightBasic = null;
                }
                //队列空说明没有基座需要设置左右的节点了，即生成完毕
                if (basicQueue.Count == 0) return;
                //防止越界
                if (j != 0)
                    //每行中间节点一定是两节点数字加和，且既是其一的左节点也是其二的右节点
                    if (numArray[i - 1, j - 1] != 0 && numArray[i - 1, j] != 0)
                    {
                        numArray[i, j] = numArray[i - 1, j - 1] + numArray[i - 1, j];
                        basic_Temp.GetComponent<y_Basic>().leftBasic = temp;
                        //当左右节点都设置完后，便需要录入队列下个节点的右节点
                        basic_Temp = basicQueue.Dequeue();
                        basic_Temp.GetComponent<y_Basic>().rightBasic = temp;
                    }
                    else
                    {
                        //每行末位数字一定是一，且一定没有左节点
                        numArray[i, j] = 1;
                        basic_Temp.GetComponent<y_Basic>().leftBasic = temp;
                        basic_Temp = basicQueue.Dequeue();
                    }
                else
                {
                    //每行首节点数字一定是1，且一定没有右节点
                    numArray[i, j] = 1;
                    basic_Temp.GetComponent<y_Basic>().rightBasic = temp;
                }
                //设置基座的数字
                temp.GetComponent<y_Basic>().numText.text = numArray[i, j].ToString();
            }
        }
    }
}
