using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Finish : MonoBehaviour
{
    public bool finish = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finish = true;
        }
    }
}
