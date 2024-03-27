using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class s_ShowPythagorean_1 : MonoBehaviour
{
    [Header ("��Դ")]
    public List<Image> _triangle;//չʾ����ļ���������
    public Image _square;//չʾ�����������
    public GameObject _show_Panel;//չʾ�������
    public Button _show,_back;//չʾ��ť�ͷ��ذ�ť
    public TextMeshProUGUI _question_Text;//�����ı�


    bool is_Show;//�Ƿ����չʾ
    float change_Time;//չʾ����仯ʱ��
    int show_GameObject;//չʾ���������ִ���



    // Start is called before the first frame update
    void Start()
    {
        InitData();
        //�󶨰�ťչʾ������壩
        _show.onClick.AddListener(delegate
        {
            _show_Panel.SetActive(true);
            is_Show = true;
        });

        //�󶨰�ť���أ��ر���壩
        _back.onClick.AddListener(delegate
        {
            _show_Panel.SetActive(false);
            InitData();
        });
    }

    // Update is called once per frame
    void Update()
    {
        //չʾ
        if (is_Show)
        {
            Show();
        }


    }

    //��ʼ������
    void InitData()
    {
        is_Show = false;//�Ƿ����չʾ
        change_Time = 0f;//չʾ����仯ʱ��
        show_GameObject = 0;//չʾ���������ִ���
    }


    //ִ��չʾ�����ʾ
    void Show()
    {
       
        //���¼�ʱ��
        change_Time -= Time.deltaTime;

        //������˸����
        if (change_Time <= 0)
        {
            show_GameObject += 1;

            change_Time = 3f;
            //�����ı�
            UpdateQuestionText(show_GameObject);
        }


        
        print(show_GameObject);
        switch (show_GameObject)
        {
            //������Ϊ1ʱ��������˸
            case 1:
                foreach (var item in _triangle)
                {
                    ChangeColor(item);
                }
                break;

            //������Ϊ2ʱ��������˸
            case 2:

                ChangeColor(_square);
                break;

            //��ʱ��������������������
            case 3:
                for (int i = 0; i < _triangle.Count; i++)
                {
                    //���һ���ǰ��������β���Ҫ����
                    if (i == _triangle.Count - 1)
                    {
                        break;
                    }
                    ChangeColor(_triangle[i]);
                }
                ChangeColor(_square);

                break;
            case 4:
                print(show_GameObject);
                //������������Text
                //UpdateQuestionText(topic.Length);
                is_Show = false;
                break;
        }

        
    }


    //����ı���ɫ�ĺ���
    void ChangeColor(Image image)
    {
        if (change_Time % 1 >= 0.5)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
        }
    }




    void UpdateQuestionText(int index)
    {
        string question_Text = "";
        for (int i = 0; i < index; i++)
        {
            //��һ�β���Ҫ����
            if (i == 0)
            {
                question_Text = topic[i];
            }
            else
            {
                question_Text += "\n" + "\n" + topic[i];
            }
            
        }
        _question_Text.text = question_Text;
    }




    String[] topic = { "���е������ζ��Ƕ�ֱ�Ǳ�Ϊa����ֱ�Ǳ�Ϊb��б��Ϊc�Ĵ�С��ͬ��ֱ��������",
                        "����ֱ�������οɵó�С�����εı߳�Ϊb-a",
                        "�������εı߳�Ϊc",
                        "���������κ������εĹ�ϵ���ο����ɶ����֤���������ռ�������Ʒ�����ұߵ�ʽ����"};
}
