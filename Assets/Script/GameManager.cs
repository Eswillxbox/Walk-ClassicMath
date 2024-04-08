using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [Header("Character")]
    private GameObject player;
    public GameObject enemy;
    [Header("UI")]
    public Image player_Hp;
    public Text player_HpNum;
    [Header("ModelBuilding")]
    public GameObject battle_Display;

    private void Awake()
    {
        if (instance != null)
            GameManager.Destroy(instance);
        instance = this;
    }
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // DisplayAttribute();
    }

    public void DisplayBattle(bool isDisplay)
    {
        battle_Display.gameObject.SetActive(isDisplay);
    }

    //UI血量显示
    public void DisplayAttribute()
    {
        player_HpNum.text = player.GetComponent<y_Player>().GetRealHp().ToString();
        player_Hp.fillAmount = player.GetComponent<y_Player>().GetRealHp() / player.GetComponent<y_Player>().maxHp;
    }

    //角色攻击
    public void PlayerAttack()
    {
        enemy.GetComponent<y_Enemy>().WasAttack(player.GetComponent<y_Player>().GetAttackValue());
    }

}
