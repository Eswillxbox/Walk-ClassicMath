using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class s_Text_Interaction : MonoBehaviour
{
    public Text _interaction_Text;//�����ı�



    int index;//��������
    string[] AllText_Array;//�ݴ�ȫ���ı�������

    String[] topic = { "���е������ζ��Ƕ�ֱ�Ǳ�Ϊa����ֱ�Ǳ�Ϊb��б��Ϊc�Ĵ�С��ͬ��ֱ��������",
                        "����ֱ�������οɵó�С�����εı߳�Ϊb-a",
                        "�������εı߳�Ϊc",
                        "���������κ������εĹ�ϵ���ο����ɶ����֤���������ռ�������Ʒ�����ұߵ�ʽ����"};

    // Start is called before the first frame update
    void Start()
    {


        AllText_Array = topic;
        MouseDown();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MouseDown()
    {
        // ���EventTrigger���
        EventTrigger eventTrigger = _interaction_Text.gameObject.AddComponent<EventTrigger>();

        // ����һ���µ�EventTrigger.Entry����PointerDown�¼�
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { Update_InteractionText((PointerEventData)data); });

        // ��PointerDown�¼���ӵ�EventTrigger
        eventTrigger.triggers.Add(pointerDownEntry);
    }




    //�����ı���������
    public void Update_InteractionText(PointerEventData data)
    {
        if (index == AllText_Array.Length - 1)
        {
            return;
        }

        //�����ı�
        _interaction_Text.text = AllText_Array[index];

        index++;//
    }




}
