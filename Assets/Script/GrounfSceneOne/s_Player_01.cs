using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class s_Player_01 : MonoBehaviour
{
    public Transform NPC2;

    bool isRiverRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CrossRiver()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        if (isRiverRight)
        {
            isRiverRight = false;
            transform.localPosition = new Vector3(25, 0, 21);
            NPC2.localPosition = new Vector3(-16.6f, -1, 8.5f);
            NPC2.localRotation = Quaternion.Euler(new Vector3(-15, 234, 0));
        }
        else
        {
            isRiverRight = true;
            transform.localPosition = new Vector3(38, 0, 30);
            NPC2.localPosition = new Vector3(-9.6f, -1, 16);
            NPC2.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        GetComponent<NavMeshAgent>().enabled = true;
    }
}