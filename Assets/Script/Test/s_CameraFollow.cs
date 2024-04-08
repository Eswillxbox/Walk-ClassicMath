using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_CameraFollow : MonoBehaviour
{
    //摄像机跟随的对象
    public Transform target;

    //两者初始的向量差
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
        //设置目标地点为目标位置加上初始的差
        Vector3 targetPos = target.position + vector3_Distance;

        //实现平滑移动
        transform.position = Vector3.Lerp(transform.position,targetPos,0.1f);
    }
}
