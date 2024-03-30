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
        Debug.Log("Move State");
        InputReader.Instance.OnJumpPerformed += OnJumpPerformed;
    }

    private void OnJumpPerformed()
    {   
        controller.SwitchState(new PlayerJumpState(controller));
    }

    public override void Exit()
    {
        InputReader.Instance.OnJumpPerformed -= OnJumpPerformed;
    }
    public override void Tick()
    {
        HandleInput();
        ApplyRotation();
        ApplyMovement();
    }
}

