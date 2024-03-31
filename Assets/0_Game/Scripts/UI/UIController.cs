using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [Header("Health Bar")]
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthFillImage;

    [Header("Ammo")]
    [SerializeField] private TextMeshProUGUI _currentAmmoText;

    [Header("XP Bar")]
    [SerializeField] private TextMeshProUGUI _xpText;
    [SerializeField] private Image _xpFillImage;
    [SerializeField] private TextMeshProUGUI _levelText;
    public void SetHealthBar(int currentHealth, int maxHealth)
    {
        _healthText.SetText(currentHealth + "/" + maxHealth);
        _healthFillImage.fillAmount = Mathf.InverseLerp(0, maxHealth, currentHealth);
    }

    public void SetCurrentAmmoText(int ammoCount)
    {
        _currentAmmoText.SetText(ammoCount.ToString());
    }

    public void SetXPBar(int currentXP, int maxXP)
    {
        _xpText.SetText(currentXP + "/" + maxXP);
        _xpFillImage.fillAmount = Mathf.InverseLerp(0, maxXP, currentXP);
        _levelText.SetText("Level: " + ExperienceSystem.Instance.CurrentLevel.ToString());
    }
}
