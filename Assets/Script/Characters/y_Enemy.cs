using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class y_Enemy : MonoBehaviour
{

    [Header("基本属性")]
    [SerializeField] private float enemy_RealHp;
    [SerializeField] private float enemy_RealAttack;
    public float maxHp;
    public float setAttack;
    public bool isGuard;
    public float actionTime;
    public Slider slider;
    void Start()
    {
        enemy_RealHp = maxHp;
        enemy_RealAttack = setAttack;
    }

    void Update()
    {
        DisplayAttribute();
    }
    //被攻击对方调用
    public void WasAttack(float a)
    {
        enemy_RealHp -= a;
    }

    public void DisplayAttribute()
    {
        slider.value = enemy_RealHp / maxHp;
    }
}
