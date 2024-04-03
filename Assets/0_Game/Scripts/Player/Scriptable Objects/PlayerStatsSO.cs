using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stat", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Speed Parameters")]
    [SerializeField, Min(10)] private float _baseMovementSpeed;
    [SerializeField, Min(12)] private float _baseSprintSpeed;
    [SerializeField, Min(1)] private float _movementSpeedIncreaseAmount;
    [SerializeField] private AnimationCurve _movementSpeedCurve;

    [Header("Jump Parameters")]
    [SerializeField, Min(300)] private float _baseJumpSpeed;
    [SerializeField, Min(1)] private float _jumpSpeedIncreaseAmount;
    [SerializeField] private AnimationCurve _jumpSpeedCurve;

    [Header("Health Parameters")]
    [SerializeField, Min(100)] private int _baseHealth;
    [SerializeField, Min(1)] private int _healthIncreaseAmount;
    [SerializeField] private AnimationCurve _healthCurve;

    [Header("Character Talents")]
    [SerializeField] private CharacterTalentSO _walkAndSprintTalent;
    [SerializeField] private CharacterTalentSO _jumpTalent;
    [SerializeField] private CharacterTalentSO _healthTalent;

    public void Init()
    {
        if (_walkAndSprintTalent != null)
            _walkAndSprintTalent.CurrentTalentLevel = 0;
        if (_jumpTalent != null)
            _jumpTalent.CurrentTalentLevel = 0;
        if (_healthTalent != null)
            _healthTalent.CurrentTalentLevel = 0;

    }

    public float GetSpeedValue()
    {
        float speedIncrease = _walkAndSprintTalent != null ? _movementSpeedCurve.Evaluate(_walkAndSprintTalent.TalentRate) * _movementSpeedIncreaseAmount : 0f;
        return speedIncrease + _baseMovementSpeed;
    }

    public float GetSprintSpeedValue()
    {
        float speedIncrease = _walkAndSprintTalent != null ? _movementSpeedCurve.Evaluate(_walkAndSprintTalent.TalentRate) * _movementSpeedIncreaseAmount : 0f;
        return speedIncrease + _baseSprintSpeed;
    }

    public float GetJumpValue()
    {
        float jumpIncrease = _jumpTalent != null ? _jumpSpeedCurve.Evaluate(_jumpTalent.TalentRate) * _jumpSpeedIncreaseAmount : 0f;
        return jumpIncrease + _baseJumpSpeed;
    }

    public int GetHealthValue()
    {
        int healthIncrease = _healthTalent != null ? (int)(_healthCurve.Evaluate(_healthTalent.TalentRate) * _healthIncreaseAmount) : 0;
        return healthIncrease + _baseHealth;
    }

    public int PreviousHealthValue()
    {
        int previousIncrease = (int)(_healthCurve.Evaluate(_healthTalent.PreviousTalentRate) * _healthIncreaseAmount);
        return previousIncrease + _baseHealth;
    }


    public Talent GetWalkTalent() => _walkAndSprintTalent;
    public Talent GetJumpTalent() => _jumpTalent;
    public Talent GetHealthTalent() => _healthTalent;
}
