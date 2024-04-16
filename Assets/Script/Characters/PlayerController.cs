using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }

    public void MoveToYangHuiBasic(Vector3 basic_Position)
    {

    }

    public void SwitchAgent()
    {
        if (agent.isStopped) agent.isStopped = false;
        else agent.isStopped = true;
    }
}
