using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlinding : MonoBehaviour
{
    [Header("npc�����ı���")]
    public GameObject interaction1_Image;//npc�����ı���1
    public Button interaction1_Image_close_Buton;
    [Header("npc�����ı���2")]
    public GameObject interaction2_Image;//npc�����ı���1
    public Button interaction2_Image_close_Buton;


    [Header("���չʾ")]
    //���չʾ
    public GameObject suanchou_Image;
    public Button suanchou_Image_open_Button, suanchou_Image_close_Button;

    [Header("����չʾ")]
    //���չʾ
    public GameObject help_Image;
    public Button help_Image_open_Button, help_Image_close_Button;

    [Header("�Ի���")]
    //�Ի���
    public GameObject DialogDisPlay_Image;
    public Button DialogDisPlay_Image_open_Button;

    //�ύ
    public Button submit_Button;
    public Button river_Button;

    //text�Ի��ı�
    public TextAsset[] textAssets;

    // Start is called before the first frame update
    void Start()
    {
        OnButtonBinding();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnButtonBinding()
    {
        interaction1_Image_close_Buton.onClick.AddListener(delegate
        {
            interaction1_Image.SetActive(false);
            MouseManager.instance.closedMouseControl = false;
        });

        interaction2_Image_close_Buton.onClick.AddListener(delegate
        {
            interaction2_Image.SetActive(false);
            MouseManager.instance.closedMouseControl = false;
        });



        //�Ի����
        DialogDisPlay_Image_open_Button.onClick.AddListener(delegate
        {
            DialogDisPlay_Image.SetActive(true);
            //�õ���ʽ
            s_Item_UI.instance.GetFormula(textAssets[0]);
            //�Ի���֮��ſ����ύ
            s_Item_UI.instance.IsSubmit = true;
        });

        //�ύ
        submit_Button.onClick.AddListener(delegate
        {
            if (s_Item_UI.instance.IsSubmit)
            {
                Text interaction_Text = interaction1_Image.transform.GetChild(1).GetComponent<Text>();
                //�ύ�ж�
                if (s_Item_UI.instance.JudgeAnswer())
                {
                    interaction_Text.text = "�������Ӧ�ø���һ����";
                    s_Item_UI.instance.IsSubmit = false;

                }
                else
                {
                    interaction_Text.text = "���ƺ�������Ӧ�û���̫����";

                }
                StartCoroutine(WaitShowText(interaction_Text));
            }
        });


        //����
        river_Button.onClick.AddListener(delegate
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<s_Player_01>().CrossRiver();
        });


        //���չʾ���
        suanchou_Image_open_Button.onClick.AddListener(delegate
        {
            suanchou_Image.SetActive(true);
        });

        suanchou_Image_close_Button.onClick.AddListener(delegate
        {
            suanchou_Image.SetActive(false);
        });

        //����չʾ���
        help_Image_open_Button.onClick.AddListener(delegate
        {
            help_Image.SetActive(true);
        });

        help_Image_close_Button.onClick.AddListener(delegate
        {
            help_Image.SetActive(false);
        });


    }

    IEnumerator WaitShowText(Text text)
    {
        yield return new WaitForSeconds(1.5f);
        text.text = "����Ҫ��Щʲô�ţ�";
    }

    void CrossRiver()
    {

    }
}
