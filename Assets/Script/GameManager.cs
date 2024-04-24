using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("ModelBuilding")]
    public GameObject[] effect_Perhaps;
    private GameObject yangHui;
    private GameObject player_basic;

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
        this.player_basic = yangHui.GetComponent<y_Yanghui>().player_Basic;
    }

    public bool IsInYangHui()
    {
        return yangHui == null ? false : true;
    }

    public void YangHuiMove(bool isLeft)
    {
        if (player_basic == null) return;
        if (isLeft && player_basic.GetComponent<y_Basic>().leftBasic != null)
        {
            player.GetComponent<PlayerController>().MoveToTarget(player_basic.GetComponent<y_Basic>().leftBasic.transform.position);
            yangHui.GetComponent<y_Yanghui>().back_Basic.Push(player_basic);
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic = player_basic.GetComponent<y_Basic>().leftBasic;
            player_basic.GetComponent<y_Basic>().DownBasic();
            BasicEnd(player_basic.GetComponent<y_Basic>().basic_kind);
        }
        else if (!isLeft && player_basic.GetComponent<y_Basic>().rightBasic != null)
        {
            player.GetComponent<PlayerController>().MoveToTarget(player_basic.GetComponent<y_Basic>().rightBasic.transform.position);
            yangHui.GetComponent<y_Yanghui>().back_Basic.Push(player_basic);
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic = player_basic.GetComponent<y_Basic>().rightBasic;
            player_basic.GetComponent<y_Basic>().DownBasic();
            BasicEnd(player_basic.GetComponent<y_Basic>().basic_kind);
        }
        UI.SetActive(false);
        Invoke("DisplayUI", 2.0f);
        return;
    }

    public void YangHuiBack()
    {
        if (yangHui.GetComponent<y_Yanghui>().back_Basic.Count != 0)
        {
            GameObject back = yangHui.GetComponent<y_Yanghui>().back_Basic.Pop();
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic = back;
            player.GetComponent<PlayerController>().MoveToTarget(back.transform.position);
            player.transform.position = back.transform.position;
            player_basic.GetComponent<y_Basic>().DownBasic();
        }
        return;
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
            diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(2);
            diaLogDisplay.SetActive(true);
        }
        if (player_basic.GetComponent<y_Basic>().basicNum == yangHui.GetComponent<y_Yanghui>().targetNum)
        {
            DisplayUI(false);
            ExitYangHuiScene();
            diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(3);
            diaLogDisplay.SetActive(true);
        }
        return;
    }

    public void YangHuiScene(bool isInYangHui)
    {
        MouseManager.instance.SwitchSetUp(isInYangHui);
        player.transform.position = player_basic.transform.position;
        player.GetComponent<PlayerController>().MoveToTarget(player_basic.transform.position);
        DisplayUI(true);
    }

    public void ExitYangHuiScene()
    {
        yangHui.GetComponent<y_Yanghui>().ReloadYangHui();
        MouseManager.instance.SwitchSetUp(false);
        DisplayUI(false);
    }

    public void DisplayUI(bool l)
    {
        UI.SetActive(l);
    }

    public void DisplayUI()
    {
        UI.SetActive(true);
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
