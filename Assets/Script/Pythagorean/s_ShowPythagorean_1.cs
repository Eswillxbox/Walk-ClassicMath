using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class s_ShowPythagorean_1 : MonoBehaviour
{
    [Header ("资源")]
    public List<Image> _triangle;//展示界面的几个三角形
    public Image _square;//展示界面的正方形
    public GameObject _show_Panel;//展示界面面板
    public Button _show,_back;//展示按钮和返回按钮
    public TextMeshProUGUI _question_Text;//问题文本


    bool is_Show;//是否可以展示
    float change_Time;//展示对象变化时间
    int show_GameObject;//展示对象，用数字代表



    // Start is called before the first frame update
    void Start()
    {
        InitData();
        //绑定按钮展示（打开面板）
        _show.onClick.AddListener(delegate
        {
            _show_Panel.SetActive(true);
            is_Show = true;
        });

        //绑定按钮返回（关闭面板）
        _back.onClick.AddListener(delegate
        {
            _show_Panel.SetActive(false);
            InitData();
        });
    }

    // Update is called once per frame
    void Update()
    {
        //展示
        if (is_Show)
        {
            Show();
        }


    }

    //初始化数据
    void InitData()
    {
        is_Show = false;//是否可以展示
        change_Time = 0f;//展示对象变化时间
        show_GameObject = 0;//展示对象，用数字代表
    }


    //执行展示后的演示
    void Show()
    {
       
        //更新计时器
        change_Time -= Time.deltaTime;

        //更换闪烁物体
        if (change_Time <= 0)
        {
            show_GameObject += 1;

            change_Time = 3f;
            //更新文本
            UpdateQuestionText(show_GameObject);
        }


        
        print(show_GameObject);
        switch (show_GameObject)
        {
            //当物体为1时三角形闪烁
            case 1:
                foreach (var item in _triangle)
                {
                    ChangeColor(item);
                }
                break;

            //当物体为2时正方形闪烁
            case 2:

                ChangeColor(_square);
                break;

            //此时让整个大正方形亮起来
            case 3:
                for (int i = 0; i < _triangle.Count; i++)
                {
                    //最后一个是案例三角形不需要闪亮
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
                //更新最后的问题Text
                //UpdateQuestionText(topic.Length);
                is_Show = false;
                break;
        }

        
    }


    //定义改变颜色的函数
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
            //第一次不需要换行
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




    String[] topic = { "所有的三角形都是短直角边为a，长直角边为b，斜边为c的大小相同的直角三角形",
                        "根据直角三角形可得出小正方形的边长为b-a",
                        "大正方形的边长为c",
                        "根据三角形和正方形的关系并参考勾股定理的证明方法将收集到的物品填充进右边的式子中"};
}
