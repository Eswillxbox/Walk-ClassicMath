using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


// [System.Serializable]
// public class EventVector3 : UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    RaycastHit hitInfo;
    public event Action<Vector3> OnMouseClicked;
    public bool closedMouseControl;
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
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground") && !closedMouseControl)
                OnMouseClicked!.Invoke(hitInfo.point);
            if (hitInfo.collider.gameObject.CompareTag("Npc") && !closedMouseControl)
            {
                if (SceneManager.GetActiveScene().name.CompareTo("GrounfSceneOne") == 0)
                {
                    GameManager.instance.diaLogDisplay.GetComponent<JudgeOnClickNPC>().JudgeNPC(hitInfo.collider.gameObject.name);
                }
                else
                {
                    GameManager.instance.diaLogDisplay.SetActive(true);
                }

                this.closedMouseControl = true;
            }
            if (hitInfo.collider.gameObject.CompareTag("Basic") && closedMouseControl)
            {
                if (hitInfo.collider.gameObject.GetComponent<y_Basic>().isWaitChoose == -1)
                {
                    GameManager.instance.waitChooseBasic = -1;
                    GameManager.instance.DisplayUI();
                }
                else if (hitInfo.collider.gameObject.GetComponent<y_Basic>().isWaitChoose == +1)
                {
                    GameManager.instance.waitChooseBasic = +1;
                    GameManager.instance.DisplayUI();
                }
                else if (hitInfo.collider.gameObject.GetComponent<y_Basic>().isBackBasic)
                {
                    GameManager.instance.DisplayUI();
                }

            }
        }
    }

    public void SwitchSetUp(bool setup)
    {
        closedMouseControl = setup;
    }
}
