using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSController : StateMachine, IDamagable
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _gunParentTransform;
    [SerializeField, Min(.01f)] private float _verticalSensitivity;
    [SerializeField, Min(.01f)] private float _horizontalSensitivity;
    [SerializeField] private BaseGunController _baseGunController;
    [field: SerializeField] public PlayerStatsSO PlayerStats { get; private set; }
    [SerializeField] private int _currentHealth;


    private Vector3 velocity;
    private float verticalRotation, horizontalRotation;
    private void Start()
    {
        PlayerStats.Init();
        _currentHealth = PlayerStats.GetHealthValue();
        UIController.Instance.SetHealthBar(_currentHealth, PlayerStats.GetHealthValue());
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _cameraTransform.SetParent(_gunParentTransform);
        SwitchState(new PlayerMovementState(this));
        _baseGunController.SetCamera(_cameraTransform.GetComponent<Camera>());
    }

    public void Move(CharacterInput input)
    {
        float speedRate = input.Sprint ? PlayerStats.GetSprintSpeedValue() : PlayerStats.GetSpeedValue();

        velocity.x = input.DirectionInput.x * speedRate;
        velocity.z = input.DirectionInput.y * speedRate;
        if (input.Jump) _playerRigidbody.AddForce(Vector3.up * PlayerStats.GetJumpValue());
        velocity.y = _playerRigidbody.velocity.y;

        Quaternion rotationY = Quaternion.Euler(0, _gunParentTransform.rotation.eulerAngles.y, 0);

        _playerRigidbody.velocity = rotationY * velocity;
    }

    public void ApplyRotation(CharacterInput input)
    {
        verticalRotation -= input.LookInput.y * _verticalSensitivity;
        horizontalRotation += input.LookInput.x * _horizontalSensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        _gunParentTransform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    public void Fire(CharacterInput input)
    {
        if (input.Fire)
            _baseGunController.Fire();
    }

    public Vector3 Position => _playerRigidbody.position;

    public bool IsGrounded()
    {
        Ray ray = new Ray(Position, Vector3.down);
        if (Physics.Raycast(ray, .1f)) return true;
        return false;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth<=0)
        {
            //open panel
            InputReader.Instance.Controls.Disable();
        }
        _currentHealth = Mathf.Max(0, _currentHealth);
        UIController.Instance.SetHealthBar(_currentHealth, PlayerStats.GetHealthValue());
    }

    public int CurrentHealth => _currentHealth;
    public bool IsPlayerAlive => _currentHealth > 0;
}

public struct CharacterInput
{
    public Vector2 DirectionInput;
    public Vector2 LookInput;
    public bool Jump;
    public bool Sprint;
    public bool Fire;
}

