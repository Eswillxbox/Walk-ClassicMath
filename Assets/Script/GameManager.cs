using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
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

    }

    void Update()
    {

    }

    public void DisplayBattle(bool isDisplay)
    {
        battle_Display.gameObject.SetActive(isDisplay);
    }
}
