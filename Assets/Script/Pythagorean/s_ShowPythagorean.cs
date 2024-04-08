using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ÿ�������ε�ָ��λ�ú������µ������Σ���ִ�ж�Ӧ�߼�
/// ���������ɻ���ת���ƶ���ָ���ص�
/// </summary>


public class s_ShowPythagorean : MonoBehaviour
{
    public static s_ShowPythagorean instance;


    [Header("UI")]
    public Text _question_Text;//�ı�
    public Button _Test_Button;
    public Button _Test_Back_Button;
    public GameObject show_panel;


    //��¼�����transform��Ϣ
    [Header("��ʾ�������ε�transform")]
    public List<Transform> transforms;

    int isShow = 0;//�޶���һ�ε����ʾ��ť����

    //�����Ĵ��������Ϸ����
    List<GameObject> claearGameObjects;
    GameObject currentGameObject;//��ǰִ���߼�������
    Transform finalTransform;//��ǰ�������յ����transform

    bool isReach = false;//��ǰ�����Ƿ񵽴����ص�
    bool isRotate = false;//�Ƿ�����ת
    int index = 1;//��һ�������ε�����
    float rotateSpeed = 20f;
    float moveeSpeed = 10f;

    //��ʾ���������
    int index2 = 1;


    String[] topic = { "���е������ζ��Ƕ�ֱ�Ǳ�Ϊa����ֱ�Ǳ�Ϊb��б��Ϊc�Ĵ�С��ͬ��ֱ��������",
                        "����ֱ�������οɵó�С�����εı߳�Ϊb-a,�������εı߳�Ϊc",
                        "���������κ������εĹ�ϵ���ο����ɶ����֤���������ռ�������Ʒ�����ұߵ�ʽ����"};



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
        //ʧ�ִ���߼�
        if (isShow != 1)
        {
            return;
        }

        switch (index2)
        {
            case 1:
                //�ж�ȫ����boolֵ
                JudgeAllBool();
                //�����������������壬��������Ϣ
                CreateNewObject();
                //����transform��Ϣ
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
        //�󶨵����
        _Test_Button.onClick.AddListener(delegate
        {
            if (isShow != 0)
            {
                return;
            }
            show_panel.SetActive(true);
            _question_Text.transform.parent.gameObject.SetActive(true);
            isShow++;
            //�����һ�β�������
            ClearGameObject();
            InitData();
        });
        //�󷵻ؼ�
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
        

        isReach = false;//��ǰ�����Ƿ񵽴����ص�
        isRotate = false;//�Ƿ�����ת
        index = 1;//��һ�������ε�����
        //��ʾ���������
        index2 = 1;
        currentGameObject = transforms[index].gameObject;
        finalTransform = transforms[index];
    }

    //��ʾС�����Σ������ı�,�ȴ����������ʾ����
    IEnumerator UpdateSquare()
    {
        
        transforms[0].gameObject.SetActive(true);
        UpdateText();
        yield return new WaitForSeconds(2f);
        index2++;
    }


    //����transform��Ϣ
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

    //�������� ,�����յ�
    void CreateNewObject()
    {
        //�������� ,�����յ�
        if (isReach)
        {
            index++;
            //���²���
            if (index == transforms.Count)
            {
                index2++;
            }

            //�ﵽ���ֵ��������
            if (index >= transforms.Count)
            {
                return;
            }

            currentGameObject = Instantiate(currentGameObject);
            //����Ϊ������
            currentGameObject.transform.SetParent(this.transform.GetChild(0),false);
            //�ı��С
            currentGameObject.transform.localScale *= 1;

            claearGameObjects.Add(currentGameObject);
            
            finalTransform = transforms[index];
            isReach = false;
        }
    }


    //�ر�ʱ������Ϸ����
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
        //�жϽǶ�
        if (Quaternion.Angle(currentGameObject.transform.rotation, finalTransform.rotation) <= 1)
        {
            isRotate = false;
        }

        //�ж�λ��
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
}
