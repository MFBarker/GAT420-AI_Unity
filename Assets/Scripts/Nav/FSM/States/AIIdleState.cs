using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public AIIdleState(AIStateAgent agent) : base(agent)
    { }

    public override void OnEnter()
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit()
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate()
    {
        Debug.Log("Update Idle");
    }
}
