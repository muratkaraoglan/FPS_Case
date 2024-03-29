using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    public PlayerMovementState(FPSController controller) : base(controller)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        HandleInput();
        
    }
}

