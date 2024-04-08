using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_CameraFollow : MonoBehaviour
{
    //���������Ķ���
    public Transform target;

    //���߳�ʼ��������
    Vector3 vector3_Distance;


    // Start is called before the first frame update
    void Start()
    {
        vector3_Distance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }


    void CameraFollow()
    {
        //����Ŀ��ص�ΪĿ��λ�ü��ϳ�ʼ�Ĳ�
        Vector3 targetPos = target.position + vector3_Distance;

        //ʵ��ƽ���ƶ�
        transform.position = Vector3.Lerp(transform.position,targetPos,0.1f);
    }
}
