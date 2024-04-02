using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(FPSController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Jump = true;
        HandleInput();
        ApplyMovement();
        DelayForGroundCheck();
    }

    public override void Exit()
    {

    }
    bool checkGround;
    public override void Tick()
    {
        // if (controller.IsGrounded) 
        HandleInput();
        ApplyRotation();
        ApplyFire();
        if (!checkGround) return;
        if (controller.IsGrounded()) controller.SwitchState(new PlayerMovementState(controller));
    }

    private async void DelayForGroundCheck()
    {
        await Task.Delay(30);
        checkGround = true;
    }

}
