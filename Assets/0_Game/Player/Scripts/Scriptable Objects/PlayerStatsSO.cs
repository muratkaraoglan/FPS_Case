using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stat", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float movementSpeedIncreaseAmount;
    [SerializeField] private float baseSprintSpeed;
    [SerializeField] private float baseJumpSpeed;
    [SerializeField] private float baseHealth;
    [SerializeField] private AnimationCurve movementSpeedCurve;

    public float GetSpeedValue(int level)
    {
        return movementSpeedCurve.Evaluate(level) * movementSpeedIncreaseAmount + baseMovementSpeed;
    }

}
