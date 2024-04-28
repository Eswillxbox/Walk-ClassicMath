using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class s_UIControl : MonoBehaviour
{
    
    private Transform help_Panel,CalculateChip_Panel;
    private Button helpOpen_Button,helpClose_BUtton;
    private Button CalculateChip_Open_Button,CalculateChip_Close_BUtton;
    private Button nextScene_Button;

    [Header ("3d算筹的父物体")]
    public Transform CalculateChipsParent;
    private Dictionary<int, Transform> calculateChips = new Dictionary<int, Transform>();
    [Header ("算筹UI的父物体")]
    public Transform CalculateChipsParent_UI;
    private Dictionary<int, Transform> calculateChips_UI = new Dictionary<int, Transform>();

    //道具数量
    Text targetCount_Text;
    int targetCount_1 = 5;
    int targetCount_2 = 18;
    int targetCount = 0;
    int currentCount = 0;

    //算式
    bool isFirstFormula = true;//是否是第一次生成算式
    bool isCreateFormula = true;//是否允许创建算式
    bool isRight = false;//答案是否正确
    GameObject[] formulas;
    public List<int> singleDigits = new List<int>();
    public List<int> TenFigureses = new List<int>();
    public List<int> answers = new List<int>();




    TaskProgress taskProgress;

    public GameObject[] Formulas { get => formulas; set => formulas = value; }

    private void Start()
    {
        ButtonBlinding();
        Init();
    }

    private void Update()
    {
        Test();
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            for (int i = 0; i < calculateChips.Count; i++)
            {
                calculateChips[i].transform.localPosition = new Vector3(60,1,-80);
            }
        }
    }



    //初始化，给数据赋值
    void Init()
    {

        for (int i = 0; i < CalculateChipsParent.childCount; i++)
        {
            //为脚本参数赋值方便使用
            CalculateChipsParent.GetChild(i).GetComponent<s_Item>().index = i;
            CalculateChipsParent_UI.GetChild(i).GetComponent<s_Item>().index = i;


            CalculateChipsParent_UI.GetChild(i).GetComponent<s_Item>().itemObj = CalculateChipsParent.GetChild(i).gameObject;

            calculateChips.Add(i,CalculateChipsParent.GetChild(i));
            calculateChips_UI.Add(i, CalculateChipsParent_UI.GetChild(i));
        }

        targetCount_Text = GameObject.Find("TargetCount").GetComponent<Text>();
        formulas = GameObject.FindGameObjectsWithTag("Formula");
        //找到之后隐藏
        foreach (GameObject t in formulas)
        {
            t.gameObject.SetActive(false);
        }

        //随机更改道具位置
        RandomChangeItemPosition();

    }


    //绑定按钮对应的功能
    void ButtonBlinding()
    {
        help_Panel = transform.Find("Panel").transform.Find("帮助");
        CalculateChip_Panel = transform.Find("Panel").transform.Find("算筹简介");

        helpOpen_Button = transform.Find("Button").transform.Find("帮助").GetComponent<Button>();
        helpClose_BUtton = transform.Find("Panel").transform.Find("帮助").GetComponentInChildren<Button>();

        CalculateChip_Open_Button = transform.Find("Button").transform.Find("算筹简介").GetComponent<Button>();
        CalculateChip_Close_BUtton = transform.Find("Panel").transform.Find("算筹简介").GetComponentInChildren<Button>();

        nextScene_Button = transform.Find("Button").transform.Find("下一关").GetComponent<Button>();

        //帮助
        helpOpen_Button.onClick.AddListener(delegate{
            help_Panel.gameObject.SetActive(true);
        });

        helpClose_BUtton.onClick.AddListener(delegate
        {
            help_Panel.gameObject.SetActive(false);
            MouseManager.instance.closedMouseControl = false;
        });


        //算筹简介
        CalculateChip_Open_Button.onClick.AddListener(delegate {
            CalculateChip_Panel.gameObject.SetActive(true);
        });

        CalculateChip_Close_BUtton.onClick.AddListener(delegate
        {
            CalculateChip_Panel.gameObject.SetActive(false);
            MouseManager.instance.closedMouseControl = false;
        });

        //下一关
        nextScene_Button.onClick.AddListener(delegate {
            GameManager.instance.ToNextScene();
        });


        //默认设置成false
        help_Panel.gameObject.SetActive(false);
        CalculateChip_Panel.gameObject.SetActive(false);
        nextScene_Button.gameObject.SetActive(false);


    }


    void RandomChangeItemPosition()
    {
        for (int i = 0; i < calculateChips.Count - 1; i++)
        {
            Vector3 calculateChip = calculateChips[i].localPosition;
            calculateChips[i].localPosition = calculateChips[i + 1].localPosition;
            calculateChips[i + 1].localPosition = calculateChip;
        }
    }


    public void UpdateUI(int  index)
    {

        currentCount++;
        if (currentCount >= targetCount)
        {
            
            targetCount = currentCount;
        }


        targetCount_Text.text = currentCount + "/" + targetCount;

        calculateChips_UI[index].GetChild(1).gameObject.SetActive(false);
        calculateChips_UI[index].GetComponent<s_UseItem>().IsPermittedUseItem = true;


        //为生成算式准备
        int number = calculateChips[index].GetComponent<s_Item>().number;
        SetAnsowerItemCount(number);
    }


    void SetAnsowerItemCount(int number)
    {

        if (isFirstFormula)
        {
            if ((singleDigits.Count + TenFigureses.Count) <= 5)
            {
                if (number > 9)
                {
                    TenFigureses.Add(number);
                }
                else
                {
                    singleDigits.Add(number);
                }
            }
        }
        else
        {
            TenFigureses.Clear();
            singleDigits.Clear();
            isFirstFormula = false;
            for (int i = 0; i < calculateChips.Count; i++)
            {
                int calculateChip_number = calculateChips[i].GetComponent<s_Item>().number;
                if (calculateChip_number > 9)
                {
                    TenFigureses.Add(calculateChip_number);
                }
                else
                {
                    singleDigits.Add(calculateChip_number);
                }
            }
        }
    }



    void Createformula()
    {
        if (!isCreateFormula)
        {
            return;
        }

        isCreateFormula = false;

        if (answers != null)
        {
            answers.Clear();
        }

        for (int i = 0; i < formulas.Length; i++)
        {
            //失活则跳过
            if (formulas[i].gameObject.activeSelf == false)
            {
                continue;
            }

            int singleDigit = singleDigits.Count == 0 ? 0 : singleDigits[Random.Range(0, singleDigits.Count)];
            int TenFigures = TenFigureses.Count == 0 ? 0 : TenFigureses[Random.Range(0, TenFigureses.Count)];
            //添加答案
            answers.Add(singleDigit + TenFigures);

            //算式的数
            int number1 = Random.Range(1, TenFigures + singleDigit);
            int number2 = TenFigures + singleDigit - number1;

            int number1_TenFigures = number1 / 10 * 10;
            int number1_singleDigit = number1 % 10;
            int number2_TenFigures = number2 / 10 * 10;
            int number2_singleDigit = number2 % 10;

/*            print("number1:  " + number1);
            print("number2:  " + number2);
            print("number1_TenFigures:  " + number1_TenFigures);
            print("number1_singleDigit:  " + number1_singleDigit);
            print("number2_TenFigures:  " + number2_TenFigures);
            print("number2_singleDigit:  " + number2_singleDigit);*/
            

            CreateFormulaObject(number1_TenFigures, number1_singleDigit, number2_TenFigures, number2_singleDigit, formulas[i]);

        }
    }

    void CreateFormulaObject(int number1_TenFigures, int number1_singleDigit, int number2_TenFigures, int number2_singleDigit,GameObject formula)
    {
        for (int i = 0; i < calculateChips.Count; i++)
        {
            //第一个数十位
            SetFormulaTransform(i, number1_TenFigures,formula, "number1十位");

            //第一个数个位
            SetFormulaTransform(i, number1_singleDigit, formula, "number1个位");


            //第2个数个位
            SetFormulaTransform(i, number2_TenFigures, formula, "number2十位");

            //第2个数个位
            SetFormulaTransform(i, number2_singleDigit, formula, "number2个位");
        }

        formula.transform.Find("answer十位").gameObject.SetActive(false);
        formula.transform.Find("answer个位").gameObject.SetActive(false);
        formula.transform.Find("空十位").gameObject.SetActive(true);
        formula.transform.Find("空个位").gameObject.SetActive(true);
    }

    void SetFormulaTransform(int i,int number, GameObject formula,string name)
    {
        if (calculateChips[i].GetComponent<s_Item>().number == number)
        {
            GameObject number1_TenFigures_Obj = Instantiate(calculateChips[i].gameObject);
            
            number1_TenFigures_Obj.transform.SetParent(formula.transform);
            number1_TenFigures_Obj.transform.localPosition = formula.transform.Find(name).localPosition;
            number1_TenFigures_Obj.transform.localRotation = formula.transform.Find(name).localRotation;
            number1_TenFigures_Obj.name = name;

            number1_TenFigures_Obj.gameObject.SetActive(true);

            Destroy(formula.transform.Find(name).gameObject);
        }
    }

    public  bool JudgeAnswer()
    {
        isRight = true;
        if (answers.Count == 0 || answers == null)
        {
            isRight = false;
        }
        for (int i = 0; i < answers.Count; i++)
        {
            int tenFigures = 0;
            int singleDigit = 0;
            
            if (formulas[i].transform.Find("answer十位").gameObject.activeSelf)
            {
                tenFigures = formulas[i].transform.Find("answer十位").GetComponent<s_Item>().number;
            }

            if (formulas[i].transform.Find("answer个位").gameObject.activeSelf)
            {
                singleDigit = formulas[i].transform.Find("answer个位").GetComponent<s_Item>().number;
            }


            if ((tenFigures + singleDigit) != answers[i])
            {
                isRight = false;
            }
        }

        return isRight;
    }


    


    public void SetTask()
    {
        int j = 0;

        while (j < 2)
        {
            j++;
            switch (taskProgress)
            {
                case TaskProgress.介绍算筹:
                    //对话，弹出算筹简介

                    MouseManager.instance.closedMouseControl = true;
                    taskProgress += 1;
                    GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(0);
                    break;


                case TaskProgress.捡拾算筹:

                    targetCount = targetCount_1;
                    targetCount_Text.text = currentCount + "/" + targetCount;
                    if (currentCount >= targetCount)
                    {
                        taskProgress += 1;
                        continue;
                    }
                    else
                    {
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(1);
                    }
                    break;


                case TaskProgress.做第一题:
                    //生成算式并判断是否正确，正确跳转下一条

                    formulas[0].gameObject.SetActive(true);
                    Createformula();

                    if (JudgeAnswer())
                    {
                        taskProgress += 1;
                        isCreateFormula = true;
                        continue;
                    }
                    else
                    {
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(2);
                    }
                    break;

                case TaskProgress.提示做正确并让找算筹:
                    GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(3);
                    targetCount = targetCount_1;
                    targetCount_Text.text = currentCount + "/" + targetCount;
                    taskProgress += 1;
                    break;

                case TaskProgress.继续做题:
                    //生成算式并判断是否正确，正确跳转下一条

                    formulas[0].gameObject.SetActive(true);
                    formulas[1].gameObject.SetActive(true);
                    //更新待选择的答案
                    isFirstFormula = false;
                    SetAnsowerItemCount(0);
                    
                    Createformula();
                    if (JudgeAnswer())
                    {
                        taskProgress += 1;
                        isCreateFormula = true;
                        continue;
                    }
                    else
                    {
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(4);
                    }
                    break;

                case TaskProgress.提示正确继续做题:
                    //生成算式并判断是否正确，正确跳转下一条

                    formulas[0].gameObject.SetActive(true);
                    formulas[1].gameObject.SetActive(true);
                    formulas[2].gameObject.SetActive(true);
                    Createformula();
                    if (JudgeAnswer())
                    {
                        taskProgress += 1;
                        isCreateFormula = true;
                        continue;
                    }
                    else
                    {
                        GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(5);
                    }
                    break;

                case TaskProgress.下一关提示:
                    GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(6);
                    nextScene_Button.gameObject.SetActive(true);
                    break;
            }

            GameManager.instance.diaLogDisplay.SetActive(true);
            j++;
        }
    }
}

public enum TaskProgress
{
    介绍算筹,捡拾算筹,做第一题,提示做正确并让找算筹,继续做题,提示正确继续做题,下一关提示
}
