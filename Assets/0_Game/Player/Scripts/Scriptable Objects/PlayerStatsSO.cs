using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stat", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Speed Parameters")]
    [SerializeField, Min(10)] private float baseMovementSpeed;
    [SerializeField, Min(12)] private float baseSprintSpeed;
    [SerializeField, Min(1)] private float movementSpeedIncreaseAmount;
    [SerializeField] private AnimationCurve movementSpeedCurve;
    [Header("Jump Parameters")]
    [SerializeField, Min(300)] private float baseJumpSpeed;
    [SerializeField, Min(1)] private float jumpSpeedIncreaseAmount;
    [SerializeField] private AnimationCurve jumpSpeedCurve;
    [Header("Health Parameters")]
    [SerializeField] private float baseHealth;
    [SerializeField] private float healthIncreaseAmount;
    [SerializeField] private AnimationCurve healthCurve;
    [Header("Character Talents")]
    [SerializeField] private CharacterTalentSO walkAndSprintTalent;
    [SerializeField] private CharacterTalentSO jumpTalent;
    [SerializeField] private CharacterTalentSO healthTalent;

    public void Init()
    {
        if (walkAndSprintTalent != null)
            walkAndSprintTalent.CurrentTalentLevel = 0;
        if (jumpTalent != null)
            jumpTalent.CurrentTalentLevel = 0;
        if (healthTalent != null)
            healthTalent.CurrentTalentLevel = 0;
    }

    public float GetSpeedValue()
    {
        float speedIncrement = walkAndSprintTalent != null ? movementSpeedCurve.Evaluate(walkAndSprintTalent.TalentRate) * movementSpeedIncreaseAmount : 0f;
        return speedIncrement + baseMovementSpeed;
    }

    public float GetSprintSpeedValue()
    {
        float speedIncrement = walkAndSprintTalent != null ? movementSpeedCurve.Evaluate(walkAndSprintTalent.TalentRate) * movementSpeedIncreaseAmount : 0f;
        return speedIncrement + baseSprintSpeed;
    }

    public float GetJumpValue()
    {
        float jumpIncrement = jumpTalent != null ? jumpSpeedCurve.Evaluate(jumpTalent.TalentRate) * jumpSpeedIncreaseAmount : 0f;
        return jumpIncrement + baseJumpSpeed;
    }

}
