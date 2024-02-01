using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    float timer;
    public AIIdleState(AIStateAgent agent) : base(agent)
    { }

    public override void OnEnter()
    {
        Debug.Log("Enter Idle");
        timer = Time.time + Random.Range(1,2);
    }

    public override void OnExit()
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate()
    {
        if (Time.time <= timer)
        {
            agent.stateMachine.SetState(nameof(AIPatrolState));
        }

        var enemies = agent.enemyperception.GetGameObjects();

        if (enemies.Length > 0)
        {
            agent.stateMachine.SetState(nameof(AIAttackState));
        }

    }
}
