using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentHolder : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button _button;

    public void SetTalent(Talent talent)
    {
        _icon.sprite = talent.TalentIcon;
        descriptionText.SetText(talent.ToString());
        _button.onClick.RemoveAllListeners();
        if (talent.IsReachedMaxLevel())
        {
            _button.interactable = false;
            return;
        }

        _button.onClick.AddListener(() =>
        {
            talent.TryLevelUp();
            TalentSystem.Instance.FillHolders();
        });

    }
}
