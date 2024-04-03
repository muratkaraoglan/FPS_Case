using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-9)]
public class InputReader : Singleton<InputReader>, Controls.IPlayerActions
{
    private Controls _controls;
    public Controls Controls
    {
        get { return _controls; }
    }

    public event Action OnJumpPerformed;


    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public bool Sprint { get; private set; }
    public bool Fire { get; private set; }
    private void OnEnable()
    {
        if (_controls != null) return;

        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
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

    public void OnSprint(InputAction.CallbackContext context)
    {
        Sprint = context.ReadValueAsButton();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Fire = context.ReadValueAsButton();
    }

    public void OnTalent(InputAction.CallbackContext context)
    {
        if (context.performed)
            TalentSystem.Instance.gameObject.SetActive(true);
    }
}
