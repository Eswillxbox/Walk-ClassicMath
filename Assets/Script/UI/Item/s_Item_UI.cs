using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_Item_UI : MonoBehaviour
{
    public static s_Item_UI instance;

    public Transform[] suanchous;//��¼ȫ�������
    public Sprite[] sprites;//��¼��Ʒչʾ��ͼƬ
    public Sprite[] sprites2;//��¼��Ʒչʾ��ͼƬ

    public List<Transform> items_UI;//2dȫ��������
    Transform item_UI;//ĳ��������

    public Text formula_Text;
    //�ж��Ƿ�չʾ����ʽ
    public bool isShowNewFormula = false;

    public Text rightSubmitCount_Text;
    public int isRightSubmitCount = 0;
    bool isSubmit = false;
    

    //ͼ������
    int number1 = 0;
    int number2 = 0;
    //�����ʽ����
    int n1;
    int n2;

    public Transform Item_UI { get => item_UI; set => item_UI = value; }
    public bool IsSubmit { get => isSubmit; set => isSubmit = value; }

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
        rightSubmitCount_Text.text = isRightSubmitCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Item_UI = ChioseItem_UI();
        ShowNewFormula();
        AbandonItem();

    }

    void ShowNewFormula()
    {
        if (isShowNewFormula)
        {
            formula_Text.text = n1 + "+" + n2 + "=";
            isShowNewFormula = false;
        }
    }

    Transform ChioseItem_UI()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            items_UI[0].parent.localScale = new Vector3(1.15f, 1.15f, 1);
            items_UI[1].parent.localScale = new Vector3(1, 1, 1);
            return items_UI[0];
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            items_UI[1].parent.localScale = new Vector3(1.15f, 1.15f, 1);
            items_UI[0].parent.localScale = new Vector3(1, 1, 1);
            return items_UI[1];
        }
        else
        {
            return Item_UI;
        }
    }

    public void UpdateUI(int index)
    {
        

        

        //��¼����ֵ��ֵ
        if (item_UI == items_UI[0])
        {
            number1 = index;
            item_UI.GetComponent<Image>().sprite = sprites[index - 1];
        }
        else
        {
            number2 = index;
            item_UI.GetComponent<Image>().sprite = sprites2[index - 1];
        }
        RandomChangeItem();
    }

    //ÿ�λ�õ��߸�������λ��
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

    //�õ���ʽ
    public void GetFormula(TextAsset file)
    {
        n1 = Random.Range(0, 100);
        n2 = Random.Range(0, 100 - n1);


        GameManager.instance.UI.GetComponentInChildren<y_TextDisplay>().GetTextFormFile1(file,n1, n2);
    }

    public bool JudgeAnswer()
    {
        if ((n1 + n2) == (number1 + number2 * 10))
        {
            isRightSubmitCount += 1;
            rightSubmitCount_Text.text = isRightSubmitCount.ToString();

            if (isRightSubmitCount == 3)
            {
                GameManager.instance.ToNextScene();
            }
            return true;
        }

        return false;
    }

    void AbandonItem()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //����������Ϊ0
            if (item_UI == items_UI[0])
            {
                number1 = 0;
                items_UI[0].GetComponent<Image>().sprite = null;
                item_UI = items_UI[0];
            }
            else
            {
                number2 = 0;
                items_UI[1].GetComponent<Image>().sprite = null;
                item_UI = items_UI[1];
            }
        }
    }

}
