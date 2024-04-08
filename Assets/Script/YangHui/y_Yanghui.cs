using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Yanghui : MonoBehaviour
{
    [Header("棋盘基座")]
    public GameObject player_Basic;
    public int basicNum;
    private Vector3[,] basic_position;
    void Start()
    {
        CreateBasic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateBasic()
    {
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
        Queue<GameObject> basicQueue = new Queue<GameObject>();
        GameObject basic_Temp = player_Basic;
        GameObject temp;
        int[,] numArray = new int[basicNum, basicNum];
        numArray[0, 0] = 1;
        for (int i = 1; i < basicNum; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (j != 0)
                    if (numArray[i - 1, j - 1] != 0 && numArray[i - 1, j] != 0)
                        numArray[i, j] = numArray[i - 1, j - 1] + numArray[i - 1, j];
                    else
                        numArray[i, j] = 1;
                else numArray[i, j] = 1;
                // temp = Instantiate(player_Basic, basic_position[i, j], player_Basic.transform.rotation, this.transform);
                // temp.GetComponent<y_Basic>().numText.text = numArray[i, j].ToString();
                // if (i != basicNum - 1)
                // {
                //     basicQueue.Enqueue(temp);
                //     if (basic_Temp.GetComponent<y_Basic>().rightBasic == null)
                //         basic_Temp.GetComponent<y_Basic>().rightBasic = temp;
                //     else
                //     {
                //         basic_Temp.GetComponent<y_Basic>().leftBasic = temp;
                //         basic_Temp = basicQueue.Dequeue();
                //         if (j != i)
                //             basic_Temp.GetComponent<y_Basic>().rightBasic = temp;
                //     }
                // }
            }
        }
    }
}
