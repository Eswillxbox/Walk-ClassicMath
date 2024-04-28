using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [Header("Character")]
    private GameObject player;
    //public GameObject enemy;
    [Header("UI")]
    public GameObject UI;
    public Image player_Hp;
    public Text player_HpNum;
    public GameObject diaLogDisplay;
    [Header("YangHui")]
    public GameObject[] effect_Perhaps;
    private GameObject yangHui;
    private GameObject player_basic;
    public Text message;
    public int waitChooseBasic = 0;
    public bool moveOrBack = true;

    [Header("DataSpace")]
    //待删除
    public int sceneIndex = 0;

    private void Awake()
    {
        if (instance != null)
            GameManager.Destroy(instance);
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null) DisplayAttribute();
        if (IsInYangHui() && player_basic != null) SetIsWaitChose(player_basic);
    }

    // public void DisplayBattle(bool isDisplay)
    // {
    //     battle_Display.gameObject.SetActive(isDisplay);
    // }

    //UI血量显示
    private void DisplayAttribute()
    {
        if (player_HpNum != null && player_Hp != null)
        {
            player_HpNum.text = player.GetComponent<y_Player>().GetRealHp().ToString();
            player_Hp.fillAmount = player.GetComponent<y_Player>().GetRealHp() / player.GetComponent<y_Player>().maxHp;
        }
        return;
    }

    //角色攻击
    // public void PlayerAttack()
    // {
    //     //enemy.GetComponent<y_Enemy>().WasAttack(player.GetComponent<y_Player>().GetAttackValue());
    // }

    //获取YangHui[杨辉三角模块]
    public void GetYangHui(GameObject gameObject)
    {
        this.yangHui = gameObject;
    }


    public bool IsInYangHui()
    {
        return yangHui == null ? false : true;
    }

    public void YangHuiScene(bool isInYangHui, int basicNum, int targetNum)
    {
        this.yangHui.GetComponent<y_Yanghui>().SetBasicNum(basicNum);
        this.yangHui.GetComponent<y_Yanghui>().targetNum = targetNum;
        this.yangHui.GetComponent<y_Yanghui>().CreateBasic();
        this.player_basic = yangHui.GetComponent<y_Yanghui>().player_Basic;
        MouseManager.instance.SwitchSetUp(isInYangHui);
        player.transform.position = player_basic.transform.position;
        player_basic.GetComponent<y_Basic>().DownBasic();
        player.GetComponent<PlayerController>().MoveToTarget(player_basic.transform.position);
        //DisplayUI(true);
    }

    public void ExitYangHuiScene()
    {
        yangHui.GetComponent<y_Yanghui>().ReloadYangHui();
        MouseManager.instance.SwitchSetUp(false);
        player_basic = yangHui.GetComponent<y_Yanghui>().player_Basic;
        player_basic.GetComponent<y_Basic>().isBackBasic = false;
        player.GetComponent<y_Player>().Healing(true);
        //DisplayUI(false);
    }

    private void SetIsWaitChose(GameObject basic)
    {
        if (player_basic.GetComponent<y_Basic>().leftBasic != null)
            basic.GetComponent<y_Basic>().leftBasic.GetComponent<y_Basic>().isWaitChoose = -1;

        if (player_basic.GetComponent<y_Basic>().rightBasic != null)
            basic.GetComponent<y_Basic>().rightBasic.GetComponent<y_Basic>().isWaitChoose = +1;
    }

    private void YangHuiMove()
    {
        if (player_basic == null) return;
        if (waitChooseBasic == -1 && player_basic.GetComponent<y_Basic>().leftBasic != null)
        {
            player.GetComponent<PlayerController>().MoveToTarget(player_basic.GetComponent<y_Basic>().leftBasic.transform.position);
            player_basic.GetComponent<y_Basic>().isBackBasic = true;
            yangHui.GetComponent<y_Yanghui>().back_Basic.Push(player_basic);
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic.GetComponent<y_Basic>().rightBasic.GetComponent<y_Basic>().isWaitChoose = 0;
            player_basic = player_basic.GetComponent<y_Basic>().leftBasic;
            player_basic.GetComponent<y_Basic>().DownBasic();
            BasicEnd(player_basic.GetComponent<y_Basic>().basic_kind);
        }
        else if (waitChooseBasic == +1 && player_basic.GetComponent<y_Basic>().rightBasic != null)
        {
            player.GetComponent<PlayerController>().MoveToTarget(player_basic.GetComponent<y_Basic>().rightBasic.transform.position);
            player_basic.GetComponent<y_Basic>().isBackBasic = true;
            yangHui.GetComponent<y_Yanghui>().back_Basic.Push(player_basic);
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic.GetComponent<y_Basic>().leftBasic.GetComponent<y_Basic>().isWaitChoose = 0;
            player_basic = player_basic.GetComponent<y_Basic>().rightBasic;
            player_basic.GetComponent<y_Basic>().DownBasic();
            BasicEnd(player_basic.GetComponent<y_Basic>().basic_kind);
        }
        TimeOffUI(0.8f);
        return;
    }

    private void YangHuiBack()
    {
        if (yangHui.GetComponent<y_Yanghui>().back_Basic.Count != 0)
        {
            GameObject back = yangHui.GetComponent<y_Yanghui>().back_Basic.Pop();
            back.GetComponent<y_Basic>().isBackBasic = false;
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic = back;
            player.GetComponent<PlayerController>().MoveToTarget(back.transform.position);
            player.transform.position = back.transform.position;
            player_basic.GetComponent<y_Basic>().DownBasic();
        }
        return;
    }

    public void YangHuiMoveOrBack()
    {
        if (moveOrBack) YangHuiMove();
        else YangHuiBack();
    }

    public void YangHuiMessage(string basic_kind)
    {
        switch (basic_kind)
        {
            case "得与失": message.text += "失去10点数,知晓前方节点的数字"; break;
            case "陷阱": message.text += "失去若干点数，数值为此节点数字的一半"; break;
            case "帮助": message.text += "恢复若干点数"; break;
            case "无用": message.text += "来这里似乎并没有什么作用"; break;
            default: break;
        }
        return;
    }

    public void YangHuiSetMessage(string text)
    {
        message.text = "";
        message.text += text;
    }

    //效果[陷阱,回退,无用,Buff,Debuff,回血,显示数字]
    public GameObject GetEffect(String e_Name)
    {
        int i = UnityEngine.Random.Range(0, 2);
        switch (e_Name)
        {
            case "得与失": return effect_Perhaps[5];
            case "陷阱": return effect_Perhaps[1];
            case "帮助": return effect_Perhaps[3];
            case "无用": return effect_Perhaps[2];
            default: break;
        }
        return null;
    }

    public GameObject GetPlayerBasic()
    {
        return player_basic;
    }

    private void BasicEnd(String e_Name)
    {
        switch (e_Name)
        {
            case "得与失":
                player.GetComponent<y_Player>().Attacked(10);
                if (player_basic.GetComponent<y_Basic>().leftBasic != null)
                    player_basic.GetComponent<y_Basic>().leftBasic.GetComponent<y_Basic>().numText.gameObject.SetActive(true);
                if (player_basic.GetComponent<y_Basic>().rightBasic != null)
                    player_basic.GetComponent<y_Basic>().rightBasic.GetComponent<y_Basic>().numText.gameObject.SetActive(true);
                break;
            case "陷阱": player.GetComponent<y_Player>().Attacked(player_basic.GetComponent<y_Basic>().basicNum / 2 + 1); break;
            case "帮助": player.GetComponent<y_Player>().Healing(player_basic.GetComponent<y_Basic>().basicNum / 4 + 1); break;
            case "无用": break;
            default: break;
        }
        if (!player_basic.GetComponent<y_Basic>().leftBasic && !player_basic.GetComponent<y_Basic>().rightBasic)
        {
            // diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(2);
            // diaLogDisplay.SetActive(true);
        }
        if (player_basic.GetComponent<y_Basic>().basicNum == yangHui.GetComponent<y_Yanghui>().targetNum)
        {
            //DisplayUI(false);
            ExitYangHuiScene();
            diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(3);
            diaLogDisplay.SetActive(true);
            switch (yangHui.GetComponent<y_Yanghui>().GetBasicNum())
            {
                case 5: diaLogDisplay.GetComponent<y_YangHuiDisplay>().OnEnableChooseButton(1); break;
                case 7: diaLogDisplay.GetComponent<y_YangHuiDisplay>().OnEnableChooseButton(2); break;
                case 10: diaLogDisplay.GetComponent<y_YangHuiDisplay>().OnEnableChooseButton(3); break;
                case 15: break;
                default: break;
            }
        }
        return;
    }


    public void DisplayUI()
    {
        UI.SetActive(true);
    }
    public void OffUI()
    {
        UI.SetActive(false);
    }

    public void TimeOffUI(float time)
    {
        Invoke("OffUI", time);
    }

    public void ToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ChioseScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public GameObject GetPlayer()
    {
        if (player != null) return player;
        return null;
    }

}
