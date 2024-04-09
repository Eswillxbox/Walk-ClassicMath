using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Player : MonoBehaviour
{
    [Header("基本属性")]
    [SerializeField] private float player_RealHp;
    [SerializeField] private float player_RealAttack;
    [SerializeField] private bool player_InBattle;
    [SerializeField] private GameObject battle_Target;
    public GameObject target;
    public float maxHp;
    public bool deFend;
    public float setAttack;
    public float actionTime;
    public bool isInYangHui;
    void Start()
    {
        //初始化
        player_InBattle = false;
        player_RealHp = maxHp;
        player_RealAttack = setAttack;
    }

    void Update()
    {
        if (target != null) MoveToTarget();
        if (isInYangHui) YangHuiScene();
    }
    //被攻击对方调用
    public void WasAttack(float a)
    {
        player_RealHp -= a;
    }
    //移动到目标
    public void MoveToTarget()
    {
        if (Vector3.Distance(target.gameObject.transform.position, transform.position) >= 2.5f)
        {
            Vector3 vc = target.gameObject.transform.position - transform.position;
            GetComponent<PlayerController>().MoveToTarget(transform.position + vc * 0.6f);
        }
        else
        {
            battle_Target = target;
            player_InBattle = true;
            target = null;
            GameManager.instance.DisplayBattle(true);
            //GetComponent<PlayerController>().StopAgent();
        }
    }
    public float GetAttackValue()
    {
        return player_RealAttack;
    }
    public float GetRealHp()
    {
        return player_RealHp;
    }
    public void YangHuiScene()
    {
    }
}
