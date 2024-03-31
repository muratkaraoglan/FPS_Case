using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : Singleton<ExperienceSystem>
{
    [field: SerializeField] public int CurrentLevel { get; set; }
    [SerializeField] private AnimationCurve _experienceCurve;
    [SerializeField] private int _currentXP;
    [SerializeField] private int _targetXP;

    [field: SerializeField] public int CurrentTalentPoint { get; set; }

    private void Start()
    {
        CalculateTargetXP();
        UIController.Instance.SetXPBar(_currentXP, _targetXP);
    }


    private void CalculateTargetXP()
    {
        _targetXP = (int)_experienceCurve.Evaluate(CurrentLevel);
    }

    [ContextMenu("XP")]
    public void GainXP(int xp)
    {
        _currentXP += xp;

        if (_currentXP >= _targetXP)
        {
            //level up
            CurrentLevel++;
            _currentXP = _currentXP - _targetXP;
            CalculateTargetXP();
            CurrentTalentPoint++;
            //1 talent point
            //update UI
        }
        UIController.Instance.SetXPBar(_currentXP, _targetXP);
    }


}
