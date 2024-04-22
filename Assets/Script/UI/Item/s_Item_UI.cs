using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_Item_UI : MonoBehaviour
{
    public static s_Item_UI instance;
    public Transform[] suanchous;//记录全部的算筹
    public Sprite[] sprites;//记录物品展示的图片

    public List<Transform> items_UI;
    Transform item_UI;

    //图标数字
    int number1;
    int number2;
    //随机算式数字
    int n1;
    int n2;

    public Transform Item_UI { get => item_UI; set => item_UI = value; }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        items_UI.Add(transform.GetChild(0).transform.GetChild(0));
        items_UI.Add(transform.GetChild(1).transform.GetChild(0));
        item_UI = items_UI[0];
    }

    // Update is called once per frame
    void Update()
    {
        Item_UI = ChioseItem_UI();
    }

    Transform ChioseItem_UI()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return items_UI[0];
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            return items_UI[1];
        }
        else
        {
            return Item_UI;
        }
    }

    public void UpdateUI(int index)
    {
        print(index);
        item_UI.GetComponent<Image>().sprite = sprites[index - 1];

        RandomChangeItem();

        //记录两数值的值
        if (item_UI == items_UI[0])
        {
            number1 = index;
        }
        else
        {
            number2 = index;
        }
    }

    //每次获得道具更换道具位置
    void RandomChangeItem()
    {
        for (int i = 0; i < suanchous.Length; i++)
        {
            int j = Random.Range(0, suanchous.Length);

            Vector3 suanchouPosion = suanchous[i].position;
            suanchous[i].position = suanchous[j].position;
            suanchous[j].position = suanchouPosion;
        }
    }

    //得到算式
    string GetFormula()
    {
        n1 = Random.Range(0, 100);
        n2 = Random.Range(0, 100 - n1);


        return n1 + "+" + n2 + "=";
    }

    bool JudgeAnswer()
    {
        if ((n1 + n2) == (number1 + number2))
        {
            return true;
        }

        return false;
    }
}
