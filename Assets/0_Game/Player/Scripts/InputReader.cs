using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-9)]
public class InputReader : Singleton<InputReader>, Controls.IPlayerActions
{
    private Controls controls;
    public Controls Controls
    {
        get { return controls; }
    }

    public event Action OnJumpPerformed;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public bool Jump { get; private set; }

    private void OnEnable()
    {
        if (controls != null) return;

        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) OnJumpPerformed?.Invoke();

    }
}
