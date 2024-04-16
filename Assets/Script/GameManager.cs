using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [Header("Character")]
    private GameObject player;
    //public GameObject enemy;
    [Header("UI")]
    public Image player_Hp;
    public Text player_HpNum;
    [Header("ModelBuilding")]
    public GameObject battle_Display;
    public GameObject[] effect_Perhaps;
    private GameObject yangHui;
    private GameObject player_basic;

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
        // DisplayAttribute();
    }

    // public void DisplayBattle(bool isDisplay)
    // {
    //     battle_Display.gameObject.SetActive(isDisplay);
    // }

    //UI血量显示
    public void DisplayAttribute()
    {
        player_HpNum.text = player.GetComponent<y_Player>().GetRealHp().ToString();
        player_Hp.fillAmount = player.GetComponent<y_Player>().GetRealHp() / player.GetComponent<y_Player>().maxHp;
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

    public void YangHuiMove(bool isLeft)
    {
        if (isLeft && player_basic.GetComponent<y_Basic>().leftBasic != null)
        {
            player.GetComponent<PlayerController>().MoveToTarget(player_basic.GetComponent<y_Basic>().leftBasic.transform.position);
            yangHui.GetComponent<y_Yanghui>().back_Basic.Push(player_basic);
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic = player_basic.GetComponent<y_Basic>().leftBasic;
            player_basic.GetComponent<y_Basic>().DownBasic();
        }
        else if (!isLeft && player_basic.GetComponent<y_Basic>().rightBasic != null)
        {
            player.GetComponent<PlayerController>().MoveToTarget(player_basic.GetComponent<y_Basic>().rightBasic.transform.position);
            yangHui.GetComponent<y_Yanghui>().back_Basic.Push(player_basic);
            player_basic.GetComponent<y_Basic>().UpBasic();
            player_basic = player_basic.GetComponent<y_Basic>().rightBasic;
            player_basic.GetComponent<y_Basic>().DownBasic();
        }
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
    public GameObject GetEffect(String effectName)
    {
        int i = UnityEngine.Random.Range(0, 2);
        switch (effectName)
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
}
