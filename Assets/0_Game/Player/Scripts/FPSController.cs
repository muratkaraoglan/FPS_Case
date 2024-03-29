using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : StateMachine
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform gunParentTransform;
    [field: SerializeField] public PlayerStatsSO PlayerStats { get; private set; }


    private Vector2 velocity;
    private float verticalRotation, horizontalRotation;
    private void Start()
    {
        cameraTransform.SetParent(gunParentTransform);
        SwitchState(new PlayerMovementState(this));
    }

    public void Move(CharacterInput input)
    {
        velocity.x = input.DirectionInput.x * PlayerStats.GetSpeedValue(0);
        velocity.y = input.DirectionInput.y * PlayerStats.GetSpeedValue(0);


        Quaternion rotationY = Quaternion.Euler(0, gunParentTransform.rotation.eulerAngles.y, 0);
        playerRigidbody.velocity = rotationY * new Vector3(velocity.x, 0, velocity.y);
    }
}

public struct CharacterInput
{
    public Vector2 DirectionInput;
    public Vector2 LookInput;
    public bool Jump;

}

