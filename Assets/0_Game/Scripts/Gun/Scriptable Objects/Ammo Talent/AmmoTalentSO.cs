using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Talent", menuName = "Scriptable Objects/Ammo Talent")]
public class AmmoTalentSO : Talent
{
    [SerializeField] private int baseAmmoCount;
    [SerializeField, Min(10)] private int ammoIncreaseAmount;
    [SerializeField] private AnimationCurve ammoCurve;
 
    public int GetMaxAmmoCount()
    {
        int ammoIncrease = (int)(ammoCurve.Evaluate(TalentRate) * ammoIncreaseAmount);
        return ammoIncrease + baseAmmoCount;

    }
}
