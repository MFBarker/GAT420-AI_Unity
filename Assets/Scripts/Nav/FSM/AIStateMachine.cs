using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>();
    public AIState CurrentState { get; private set; } = null;

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void AddState(string name, AIState state)
    {
        Debug.Assert(!states.ContainsKey(name), "State machine already contains state" + name);

        states[name] = state; //states[KEY] = node
    }

    public void SetState(string name) 
    {
        Debug.Assert(states.ContainsKey(name), "State machine already contains state" + name);
        var nextState = states[name];

        //don't reenter state
        if (CurrentState == nextState) return;

        //exit curent state
        CurrentState?.OnExit();
        //set next state
        CurrentState = nextState;
        //Enter New State
        CurrentState?.OnEnter();
    }
}
