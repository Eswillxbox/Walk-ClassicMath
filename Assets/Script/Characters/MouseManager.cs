using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// [System.Serializable]
// public class EventVector3 : UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    RaycastHit hitInfo;
    public event Action<Vector3> OnMouseClicked;
    public bool setUp;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitInfo);
        MouseControl();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground") && !setUp)
                OnMouseClicked!.Invoke(hitInfo.point);
            if (hitInfo.collider.gameObject.CompareTag("Npc") && !setUp)
            {
                GameManager.instance.diaLogDisplay.SetActive(true);
                this.setUp = true;
            }

        }
    }

    public void SwitchSetUp(bool setup)
    {
        setUp = setup;
    }
}
