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
    RaycastHit[] hitInfos;
    public event Action<Vector3> OnMouseClicked;
    public bool closedMouseControl;

    public RaycastHit[] HitInfos { get => hitInfos; set => hitInfos = value; }

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
        hitInfos = Physics.RaycastAll(ray);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
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
                if (GameManager.instance.UI.GetComponent<s_UIControl>() != null)
                {
                    GameManager.instance.UI.GetComponent<s_UIControl>().SetTask();
                }
                else
                {
                    GameManager.instance.diaLogDisplay.SetActive(true);
                }
               

                
                this.closedMouseControl = true;
            }

            if (hitInfo.collider.gameObject.CompareTag("Item") && !closedMouseControl)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<s_Player_01>().GetItem(hitInfo);
            }

            if (GameManager.instance.UI.GetComponent<s_UIControl>() != null)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<s_Player_01>().AbandonAnswer(hitInfos);
            }

            if (hitInfo.collider.gameObject.CompareTag("Basic") && closedMouseControl)
            {
                if (hitInfo.collider.gameObject.GetComponent<y_Basic>().isWaitChoose == -1)
                {
                    GameManager.instance.waitChooseBasic = -1;
                    GameManager.instance.YangHuiSetMessage(hitInfo.collider.gameObject.GetComponent<y_Basic>().basic_kind + "\n");
                    GameManager.instance.YangHuiMessage(hitInfo.collider.gameObject.GetComponent<y_Basic>().basic_kind);
                    GameManager.instance.DisplayUI();
                }
                else if (hitInfo.collider.gameObject.GetComponent<y_Basic>().isWaitChoose == +1)
                {
                    GameManager.instance.waitChooseBasic = +1;
                    GameManager.instance.YangHuiSetMessage(hitInfo.collider.gameObject.GetComponent<y_Basic>().basic_kind + "\n");
                    GameManager.instance.YangHuiMessage(hitInfo.collider.gameObject.GetComponent<y_Basic>().basic_kind);
                    GameManager.instance.DisplayUI();
                }
                else if (hitInfo.collider.gameObject.GetComponent<y_Basic>().isBackBasic)
                {
                    GameManager.instance.YangHuiSetMessage("确认要返回上一节点吗，需要扣除10点数");
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
