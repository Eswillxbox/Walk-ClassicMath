using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class s_Text_Interaction : MonoBehaviour
{
    Text _interaction_Text;//交互文本
    Text _tips_Text;//下一页还是关闭



    int index;//数组索引
    string[] AllText_Array;//暂存全部文本的数组

    String[] topic = { "所有的三角形都是短直角边为a，长直角边为b，斜边为c的大小相同的直角三角形",
                        "根据直角三角形可得出小正方形的边长为b-a",
                        "大正方形的边长为c",
                        "根据三角形和正方形的关系并参考勾股定理的证明方法将收集到的物品填充进右边的式子中"};

    // Start is called before the first frame update
    void Start()
    {
        _interaction_Text = transform.GetChild(0).GetComponent<Text>();
        _tips_Text = transform.GetChild(1).GetComponent<Text>();

        AllText_Array = topic;
        MouseDown();


    }



    //绑定UI点击事件
    void MouseDown()
    {
        // 添加EventTrigger组件
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        // 创建一个新的EventTrigger.Entry用于PointerDown事件
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { Update_InteractionText((PointerEventData)data); });

        // 将PointerDown事件添加到EventTrigger
        eventTrigger.triggers.Add(pointerDownEntry);
    }




    //更新文本，点击鼠标
    public void Update_InteractionText(PointerEventData data)
    {
        if (index == AllText_Array.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        _tips_Text.text = "下一页";

        if (index == AllText_Array.Length - 1)
        {
            _tips_Text.text = "关闭";
        }

        //更新文本
        _interaction_Text.text = AllText_Array[index];

        index++;//
    }




}
