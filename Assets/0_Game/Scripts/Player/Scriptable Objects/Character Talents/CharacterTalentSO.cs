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


    public void TryLevelUp()
    {
        if (ExperienceSystem.Instance.CurrentTalentPoint >= requiredTalentPoint)
        {
            CurrentTalentLevel++;
            CurrentTalentLevel = Mathf.Min(CurrentTalentLevel, MaxTalentLevel);
            ExperienceSystem.Instance.CurrentTalentPoint -= requiredTalentPoint;
        }

    }
    public bool IsReachedMaxLevel() => CurrentTalentLevel == MaxTalentLevel;

    public float TalentRate => Mathf.InverseLerp(0f, MaxTalentLevel, CurrentTalentLevel);

    public float PreviousTalentRate => Mathf.InverseLerp(0f, MaxTalentLevel, Mathf.Max(0, CurrentTalentLevel - 1));
    public override string ToString()
    {
        return TalentName + "\n" + TalentDescription + "\n" + "Current Level: " + CurrentTalentLevel + "/" + MaxTalentLevel + "\n" + "Required Talent Point: " + requiredTalentPoint;
    }
}