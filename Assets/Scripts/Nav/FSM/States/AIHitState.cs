using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : AIState
{
    public AIHitState(AIStateAgent agent) : base(agent)
    { }

    public override void OnEnter()
    {
        agent.animator?.SetTrigger("Hit");
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        agent.stateMachine.SetState(nameof(AIIdleState));
    }
}
