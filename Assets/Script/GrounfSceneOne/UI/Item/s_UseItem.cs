using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class s_UseItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RaycastHit[] hits;

    GameObject item;
    GameObject formula;
    bool isPermittedUseItem = false;

    public bool IsPermittedUseItem { get => isPermittedUseItem; set => isPermittedUseItem = value; }

    private void Start()
    {
        
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isPermittedUseItem)
        {
            return;
        }
        item = Instantiate(this.GetComponent<s_Item>().itemObj);
        item.gameObject.SetActive(true);
        Destroy(item.GetComponent<BoxCollider>());
        //item.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

        
        //item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane + 10;
        item.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        hits = MouseManager.instance.HitInfos;
        bool isContains = false;

        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                //hit = MouseManager.instance.HitInfo

                if (hits[i].collider.gameObject.CompareTag("Formula"))
                {
                    formula = hits[i].collider.gameObject;
                    isContains = true;
                }
            }

            if (isContains)
            {
                
                if (item.gameObject.GetComponent<s_Item>().number > 9)
                {
                    item.transform.SetParent(formula.transform);
                    item.transform.localPosition = formula.transform.Find("answerʮλ").localPosition;
                    item.transform.localRotation = formula.transform.Find("answerʮλ").localRotation;

                    Destroy(formula.transform.Find("answerʮλ").gameObject);
                    item.name = "answerʮλ";
                    formula.transform.Find("��ʮλ").gameObject.SetActive(false);
                    
                }
                else
                {
                    item.transform.SetParent(formula.transform);
                    item.transform.localPosition = formula.transform.Find("answer��λ").localPosition;
                    item.transform.localRotation = formula.transform.Find("answer��λ").localRotation;

                    Destroy(formula.transform.Find("answer��λ").gameObject);
                    item.name = "answer��λ";
                    formula.transform.Find("�ո�λ").gameObject.SetActive(false);
                    
                }
                
            }
            else
            {
                Destroy(item.gameObject);
            }
        }
        
    }

}
