using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected readonly FPSController controller;
    public bool Jump;


    protected PlayerBaseState(FPSController controller)
    {
        this.controller = controller;
    }

    public void HandleInput()
    {
        CharacterInput characterInput = new CharacterInput();
        characterInput.DirectionInput = InputReader.Instance.Move;
        characterInput.LookInput = InputReader.Instance.Look;

        controller.Move(characterInput);
    }
}
