using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pierce Talent", menuName = "Scriptable Objects/Pierce Talent")]
public class PierceTalentSO : Talent
{
    public bool IsPierceOpened => IsReachedMaxLevel();

}
