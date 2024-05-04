using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class s_Player_01 : MonoBehaviour
{
    GameObject Item_0;
    GameObject Item_0_Prefab;
    GameObject Item_0_parent;
    // Start is called before the first frame update
    void Start()
    {
       Item_0_Prefab = Resources.Load("Prefabs/0") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GetItem(RaycastHit hitInfo)
    {
        AudioManage.instance.SetClips(ClipSelect.获取道具);
        GameManager.instance.UI.GetComponent<s_UIControl>().UpdateUI(hitInfo.collider.gameObject.GetComponent<s_Item>().index);

        //避免算式高亮
        hitInfo.collider.gameObject.GetComponent<s_Item>().SetStartMaterial();
        
        //捡到销毁
        hitInfo.collider.gameObject.SetActive(false);


    }

    //
    public void AbandonAnswer(RaycastHit[] hits)
    {

        bool isFormula = false;//是算式
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Formula"))
            {
                isFormula = true;
                Item_0_parent = hit.collider.gameObject;
            }      
        }
        if (!isFormula)
        {
            return;
        }
        AudioManage.instance.SetClips(ClipSelect.选择);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Item"))
            {
                Item_0 = Instantiate(Item_0_Prefab);
                Item_0.transform.SetParent(Item_0_parent.transform);

                GameObject targetObj = hit.collider.gameObject;

                if (hit.collider.name.Contains("空") || hit.collider.name.Contains("answer"))
                {
                    if (hit.collider.name.Contains("空十"))
                    {
                        hit.collider.gameObject.SetActive(false);
                        targetObj = Item_0_parent.transform.Find("answer十位").gameObject;
                        Item_0.transform.localPosition = targetObj.transform.localPosition;
                        Item_0.transform.localRotation = targetObj.transform.localRotation;

                        Item_0.name = targetObj.name;

                    }
                    else if (hit.collider.name.Contains("空个"))
                    {
                        hit.collider.gameObject.SetActive(false);
                        targetObj = Item_0_parent.transform.Find("answer个位").gameObject;
                        Item_0.transform.localPosition = targetObj.transform.localPosition;
                        Item_0.transform.localRotation = targetObj.transform.localRotation;

                        Item_0.name = targetObj.name;

                    }
                    else
                    {
                        Item_0.transform.localPosition = targetObj.transform.localPosition;
                        Item_0.transform.localRotation = targetObj.transform.localRotation;

                        Item_0.name = hit.collider.name;

                    }

                    Destroy(targetObj.gameObject);
                }


            }


        }

        


    }
}
