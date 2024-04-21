using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_NPC_Interaction : MonoBehaviour
{
    
    public Image interaction_Image;//交互文本框


    Transform player;
    Transform childTransform;//提示
    //提示距离
    float distance = 5f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        childTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

        if (JudgeDistance())
        {
            ChildFollowPlayer();
            KeyDownToText();
        }
        
    }


    //提示跟随玩家
    void ChildFollowPlayer()
    {
        Vector3 playerTrans = new Vector3(player.position.x, childTransform.position.y, player.position.z);
        childTransform.LookAt(playerTrans);
        childTransform.Rotate(Vector3.up, 180);
    }

    void KeyDownToText()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            interaction_Image.gameObject.SetActive(true);
        }
    }

    //判断玩家和NPc的距离
    bool JudgeDistance()
    {
        if (Vector3.Distance(player.position,transform.position) <= distance)
        {
            childTransform.gameObject.SetActive(true);

            return true;
        }
        else
        {
            childTransform.gameObject.SetActive(false);
            return false;
        }

        
    }
}
