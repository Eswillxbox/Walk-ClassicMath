using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_NPC_Interaction : MonoBehaviour
{
    public Transform player;
    public Image interaction_Image;//�����ı���

    Transform childTransform;//��ʾ
    //��ʾ����
    float distance = 1f;


    // Start is called before the first frame update
    void Start()
    {
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


    //��ʾ�������
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

    //�ж���Һ�NPc�ľ���
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
