using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : Singleton<ExperienceSystem>
{
    [field: SerializeField] public int CurrentLevel { get; set; }
    [SerializeField] private AnimationCurve _experienceCurve;
    [SerializeField] private int _currentXP;
    [SerializeField] private int _targetXP;

    private void Start()
    {
        CalculateTargetXP();
    }


    private void CalculateTargetXP()
    {
        _targetXP = (int)_experienceCurve.Evaluate(CurrentLevel);
    }

    [ContextMenu("XP")]
    public void GainXP()
    {
        _currentXP += 10;

        if (_currentXP >= _targetXP)
        {
            //level up
            CurrentLevel++;
            _currentXP = _currentXP - _targetXP;
            CalculateTargetXP();
           
            //1 talent point
            //update UI
        }
        UIController.Instance.SetXPBar(_currentXP, _targetXP);
    }


}
