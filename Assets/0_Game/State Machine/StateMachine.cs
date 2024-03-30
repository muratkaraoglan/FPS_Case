using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Tick();
    }

}

