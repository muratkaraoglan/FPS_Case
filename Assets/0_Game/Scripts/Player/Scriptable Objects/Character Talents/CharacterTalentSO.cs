using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character Talent", menuName = "Scriptable Objects/Chracter Talent")]
public class CharacterTalentSO : Talent
{


}

public abstract class Talent : ScriptableObject
{
    [field: SerializeField] public Sprite TalentIcon { get; private set; }
    [field: SerializeField] public string TalentName { get; private set; }
    [field: SerializeField] public string TalentDescription { get; private set; }

    public int CurrentTalentLevel;
    [SerializeField] private int requiredTalentPoint;
    [SerializeField] private int MaxTalentLevel;


    public  int TryLevelUp(int talentPoint)
    {
        if (talentPoint >= requiredTalentPoint)
        {
            CurrentTalentLevel++;
            CurrentTalentLevel = Mathf.Min(CurrentTalentLevel, MaxTalentLevel);
            talentPoint-= requiredTalentPoint;
        }
        return talentPoint;
    }
    public bool IsReachedMaxLevel() => CurrentTalentLevel == MaxTalentLevel;

    public float TalentRate => Mathf.InverseLerp(0f, MaxTalentLevel, CurrentTalentLevel);
}