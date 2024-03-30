using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected readonly FPSController controller;
    public bool Jump;
    CharacterInput characterInput;

    protected PlayerBaseState(FPSController controller)
    {
        this.controller = controller;
    }

    public void HandleInput()
    {
        characterInput = new CharacterInput();
        characterInput.DirectionInput = InputReader.Instance.Move;
        characterInput.LookInput = InputReader.Instance.Look;
        characterInput.Jump = Jump;
    }

    public void ApplyMovement()
    {
        controller.Move(characterInput);
    }
    public void ApplyRotation()
    {
        controller.ApplyRotation(characterInput);
    }

 
}
