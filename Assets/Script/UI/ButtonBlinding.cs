using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlinding : MonoBehaviour
{
    [Header("npc交互文本框")]
    public GameObject interaction1_Image;//npc交互文本框1
    public Button interaction1_Image_close_Buton;
    [Header("npc交互文本框2")]
    public GameObject interaction2_Image;//npc交互文本框1
    public Button interaction2_Image_close_Buton;


    [Header("算筹展示")]
    //算筹展示
    public GameObject suanchou_Image;
    public Button suanchou_Image_open_Button, suanchou_Image_close_Button;

    [Header("帮助展示")]
    //算筹展示
    public GameObject help_Image;
    public Button help_Image_open_Button, help_Image_close_Button;

    [Header("对话框")]
    //对话框
    public GameObject DialogDisPlay_Image;
    public Button DialogDisPlay_Image_open_Button;

    //提交
    public Button submit_Button;
    public Button river_Button;

    //text对话文本
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
            MouseManager.instance.setUp = false;
        });

        interaction2_Image_close_Buton.onClick.AddListener(delegate
        {
            interaction2_Image.SetActive(false);
            MouseManager.instance.setUp = false;
        });



        //对话面板
        DialogDisPlay_Image_open_Button.onClick.AddListener(delegate
        {
            DialogDisPlay_Image.SetActive(true);
            //得到算式
            s_Item_UI.instance.GetFormula(textAssets[0]);
            //对话过之后才可以提交
            s_Item_UI.instance.IsSubmit = true;
        });

        //提交
        submit_Button.onClick.AddListener(delegate
        {
            if (s_Item_UI.instance.IsSubmit)
            {
                Text interaction_Text = interaction1_Image.transform.GetChild(1).GetComponent<Text>();
                //提交判断
                if (s_Item_UI.instance.JudgeAnswer())
                {
                    interaction_Text.text = "你对算筹的应用更进一步了";
                    s_Item_UI.instance.IsSubmit = false;

                }
                else
                {
                    interaction_Text.text = "你似乎对算筹的应用还不太理想";

                }
                StartCoroutine(WaitShowText(interaction_Text));
            }
        });


        //过河
        river_Button.onClick.AddListener(delegate
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<s_Player_01>().CrossRiver();
        });


        //算筹展示面板
        suanchou_Image_open_Button.onClick.AddListener(delegate
        {
            suanchou_Image.SetActive(true);
        });

        suanchou_Image_close_Button.onClick.AddListener(delegate
        {
            suanchou_Image.SetActive(false);
        });

        //帮助展示面板
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
        text.text = "你想要做些什么呐！";
    }

    void CrossRiver()
    {

    }
}
