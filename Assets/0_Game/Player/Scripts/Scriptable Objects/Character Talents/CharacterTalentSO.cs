using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character Talent", menuName = "Scriptable Objects/Chracter Talent")]
public class CharacterTalentSO : ScriptableObject
{
    [field: SerializeField] public Sprite TalentIcon { get; private set; }
    [field: SerializeField] public string TalentName { get; private set; }
    [field: SerializeField] public string TalentDescription { get; private set; }

    public int CurrentTalentLevel;
    [SerializeField] private int requiredTalentPoint;
    [SerializeField] private int MaxTalentLevel;


    public bool TryLevelUp(int talentPoint)
    {
        if (requiredTalentPoint > talentPoint) return false;
        else
        {
            CurrentTalentLevel++;
            return true;
        }
    }
    public bool IsReachedMaxLevel() => CurrentTalentLevel == MaxTalentLevel;

    public float TalentRate => Mathf.InverseLerp(0f, (float)MaxTalentLevel, (float)CurrentTalentLevel);


}
