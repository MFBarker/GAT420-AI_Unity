using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(AIStateAgent agent) : base(agent)
    { }

    public override void OnEnter()
    {
        Debug.Log("Enter Attack");
    }

    public override void OnExit()
    {
        Debug.Log("Exit Attack");
    }

    public override void OnUpdate()
    {
        Debug.Log("Update Attack");
    }
}
