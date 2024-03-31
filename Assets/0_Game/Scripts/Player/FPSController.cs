using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSController : StateMachine
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform gunParentTransform;
    [SerializeField, Min(.01f)] private float verticalSensitivity;
    [SerializeField, Min(.01f)] private float horizontalSensitivity;
    [SerializeField] private BaseGunController baseGunController;
    [field: SerializeField] public PlayerStatsSO PlayerStats { get; private set; }
    [SerializeField] private int currentHealth;


    private Vector3 velocity;
    private float verticalRotation, horizontalRotation;
    private void Start()
    {
        PlayerStats.Init();
        currentHealth = PlayerStats.GetHealthValue();
        UIController.Instance.SetHealthBar(currentHealth, PlayerStats.GetHealthValue());
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cameraTransform.SetParent(gunParentTransform);
        SwitchState(new PlayerMovementState(this));
        baseGunController.SetCamera(cameraTransform.GetComponent<Camera>());
    }

    public void Move(CharacterInput input)
    {
        float speedRate = input.Sprint ? PlayerStats.GetSprintSpeedValue() : PlayerStats.GetSpeedValue();

        velocity.x = input.DirectionInput.x * speedRate;
        velocity.z = input.DirectionInput.y * speedRate;
        if (input.Jump) playerRigidbody.AddForce(Vector3.up * PlayerStats.GetJumpValue());
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

    public void Fire(CharacterInput input)
    {
        if (input.Fire)
            baseGunController.Fire();
    }

    public Vector3 Position => playerRigidbody.position;

    public bool IsGrounded()
    {
        Ray ray = new Ray(Position, Vector3.down);
        if (Physics.Raycast(ray, .1f)) return true;
        return false;
    }
}

public struct CharacterInput
{
    public Vector2 DirectionInput;
    public Vector2 LookInput;
    public bool Jump;
    public bool Sprint;
    public bool Fire;
}

