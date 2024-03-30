using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class FPSController : StateMachine
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform gunParentTransform;
    [SerializeField, Min(.01f)] private float verticalSensitivity;
    [SerializeField, Min(.01f)] private float horizontalSensitivity;
    [field: SerializeField] public PlayerStatsSO PlayerStats { get; private set; }


    private Vector3 velocity;
    private float verticalRotation, horizontalRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cameraTransform.SetParent(gunParentTransform);
        SwitchState(new PlayerMovementState(this));
    }

    public void Move(CharacterInput input)
    {
        velocity.x = input.DirectionInput.x * PlayerStats.GetSpeedValue(0);
        velocity.z = input.DirectionInput.y * PlayerStats.GetSpeedValue(0);
        if (input.Jump) playerRigidbody.AddForce(Vector3.up * PlayerStats.GetJumpValue(0));
        velocity.y = playerRigidbody.velocity.y;

        

        Quaternion rotationY = Quaternion.Euler(0, gunParentTransform.rotation.eulerAngles.y, 0);

        playerRigidbody.velocity = rotationY * velocity;
    }

    public void ApplyRotation(CharacterInput input)
    {
        verticalRotation -= input.LookInput.y * verticalSensitivity;
        horizontalRotation += input.LookInput.x * horizontalSensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        gunParentTransform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    public Vector3 Position => playerRigidbody.position;

    public bool IsGrounded()
    {
        Ray ray = new Ray(Position, Vector3.down);
        if (Physics.Raycast(ray, .01f)) return true;
        return false;
    }



}

public struct CharacterInput
{
    public Vector2 DirectionInput;
    public Vector2 LookInput;
    public bool Jump;

}

