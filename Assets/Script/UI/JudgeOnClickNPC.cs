using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeOnClickNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public void JudgeNPC(string str)
    {
        if (str.Contains("1"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(str.Contains("2"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
