using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : Singleton<ExperienceSystem>
{

    [SerializeField] private AnimationCurve _experienceCurve;
    [SerializeField] private int _currentXP;
    [SerializeField] private int _targetXP;
    [field: SerializeField] public int CurrentLevel { get; set; }
    [field: SerializeField] public int CurrentTalentPoint { get; set; }

    public event Action OnLevelUp;

    private void Start()
    {
        CalculateTargetXP();
        UIController.Instance.SetXPBar(_currentXP, _targetXP);
    }

    private void CalculateTargetXP()
    {
        _targetXP = (int)_experienceCurve.Evaluate(CurrentLevel);
    }

    public void GainXP(int xp)
    {
        _currentXP += xp;

        if (_currentXP >= _targetXP)
        {
            CurrentLevel++;
            _currentXP = _currentXP - _targetXP;
            CalculateTargetXP();
            CurrentTalentPoint++;
            OnLevelUp?.Invoke();
        }
        UIController.Instance.SetXPBar(_currentXP, _targetXP);
    }

}
