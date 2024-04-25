using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Message : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetBool("Detaildown", true);
    }

    public void UpMessage()
    {
        anim.SetBool("Detaildown", false);
    }
}
