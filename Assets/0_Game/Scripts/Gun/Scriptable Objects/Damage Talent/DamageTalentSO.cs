using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Talent", menuName = "Scriptable Objects/Damage Talent")]
public class DamageTalentSO : Talent
{
    [SerializeField] private int baseDamage;
    [SerializeField, Min(10)] private int damageIncreaseAmount;
    [SerializeField] private AnimationCurve damageCurve;

    public int GetDamageAmount()
    {
        int damageIncrease = (int)(damageCurve.Evaluate(TalentRate) * damageIncreaseAmount);

        return damageIncrease + baseDamage;
    }
}
