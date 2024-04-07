using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 每个三角形到指定位置后生成新的三角形，并执行对应逻辑
/// 三角形生成会旋转并移动到指定地点
/// </summary>


public class s_ShowPythagorean : MonoBehaviour
{
    public static s_ShowPythagorean instance;


    [Header("UI")]
    public Text _question_Text;//文本
    public Button _Test_Button;
    public Button _Test_Back_Button;
    public GameObject show_panel;


    //记录物体的transform信息
    [Header("演示的正方形的transform")]
    public List<Transform> transforms;

    int isShow = 0;//限定第一次点击演示按钮触发

    //创建的待清理的游戏物体
    List<GameObject> claearGameObjects;
    GameObject currentGameObject;//当前执行逻辑的物体
    Transform finalTransform;//当前物体最终到达的transform

    bool isReach = false;//当前物体是否到达最后地点
    bool isRotate = false;//是否在旋转
    int index = 1;//第一步三角形的索引
    float rotateSpeed = 20f;
    float moveeSpeed = 10f;

    //演示步骤的索引
    int index2 = 1;


    String[] topic = { "所有的三角形都是短直角边为a，长直角边为b，斜边为c的大小相同的直角三角形",
                        "根据直角三角形可得出小正方形的边长为b-a,大正方形的边长为c",
                        "根据三角形和正方形的关系并参考勾股定理的证明方法将收集到的物品填充进右边的式子中"};



    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        InitData();
        BindButton();
    }

    // Update is called once per frame
    void Update()
    {
        //失活不执行逻辑
        if (isShow != 1)
        {
            return;
        }

        switch (index2)
        {
            case 1:
                //判断全部的bool值
                JudgeAllBool();
                //满足条件创建新物体，并设置信息
                CreateNewObject();
                //更新transform信息
                UpdateTransform();
                UpdateText();
                break;
            case 2:
                StartCoroutine(UpdateSquare());
                break;
            case 3:
                UpdateText();
                break;
        }
    }


    void BindButton()
    {
        //绑定点击键
        _Test_Button.onClick.AddListener(delegate
        {
            if (isShow != 0)
            {
                return;
            }
            show_panel.SetActive(true);
            _question_Text.transform.parent.gameObject.SetActive(true);
            isShow++;
            //清除上一次残余物体
            ClearGameObject();
            InitData();
        });
        //绑返回键
        _Test_Back_Button.onClick.AddListener(delegate
        {
            show_panel.SetActive(false);
            _question_Text.transform.parent.gameObject.SetActive(false);
            isShow = 0;
            transforms[0].gameObject.SetActive(false);
        });
    }

    void InitData()
    {
        claearGameObjects = new List<GameObject>();
        

        isReach = false;//当前物体是否到达最后地点
        isRotate = false;//是否在旋转
        index = 1;//第一步三角形的索引
        //演示步骤的索引
        index2 = 1;
        currentGameObject = transforms[index].gameObject;
        finalTransform = transforms[index];
    }

    //显示小正方形，更新文本,等待两秒更换演示步骤
    IEnumerator UpdateSquare()
    {
        
        transforms[0].gameObject.SetActive(true);
        UpdateText();
        yield return new WaitForSeconds(2f);
        index2++;
    }


    //更新transform信息
    void UpdateTransform()
    {
        if (isRotate)
        {
            currentGameObject.transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
        }
        else
        {
            currentGameObject.transform.position = Vector3.MoveTowards
                (currentGameObject.transform.position, finalTransform.position, moveeSpeed * Time.deltaTime);
        }
    }

    //复制自身 ,设置终点
    void CreateNewObject()
    {
        //复制自身 ,设置终点
        if (isReach)
        {
            index++;
            //更新步骤
            if (index == transforms.Count)
            {
                index2++;
            }

            //达到最大值不在生成
            if (index >= transforms.Count)
            {
                return;
            }

            currentGameObject = Instantiate(currentGameObject);
            //设置为子物体
            currentGameObject.transform.SetParent(this.transform.GetChild(0),false);
            //改变大小
            currentGameObject.transform.localScale *= 1;

            claearGameObjects.Add(currentGameObject);
            
            finalTransform = transforms[index];
            isReach = false;
        }
    }


    //关闭时销毁游戏物体
    void ClearGameObject()
    {
        foreach (var item in claearGameObjects)
        {
            Destroy(item.gameObject);
        }


        claearGameObjects.Clear();
    }


    void JudgeAllBool()
    {
        //判断角度
        if (Quaternion.Angle(currentGameObject.transform.rotation, finalTransform.rotation) <= 1)
        {
            isRotate = false;
        }

        //判断位置
        if (currentGameObject.transform.position == finalTransform.position)
        {
            isReach = true;
            isRotate = true;
        }

        
    }

    void UpdateText()
    {
        string question_Text = "";
        for (int i = 0; i < index2; i++)
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
}
