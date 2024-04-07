using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class s_MouseMangerTest : MonoBehaviour
{
    public EventVector3 OnMouseButton;
    RaycastHit hit;


    private void Update()
    {
        GetRaycastHit();
        MouseControl();
    }


    void GetRaycastHit()
    {
        //设置摄像机到鼠标点的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, out hit);

    }

    void MouseControl()
    {
        //避免点击到外部，和UI上
        if (Input.GetMouseButtonDown(0) && hit.collider != null  && !EventSystem.current.IsPointerOverGameObject())
        {
            if (hit.collider.gameObject.CompareTag("Ground") || hit.collider.gameObject.CompareTag("Formula"))
            {
                OnMouseButton?.Invoke(hit.point);
            }
        }
    }

}
