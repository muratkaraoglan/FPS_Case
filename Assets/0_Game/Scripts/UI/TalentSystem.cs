using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalentSystem : Singleton<TalentSystem>
{
    [SerializeField] private TextMeshProUGUI _currentTalentPointText;
    [SerializeField] private TalentHolder _walkAndSprintTH;
    [SerializeField] private TalentHolder _jumpTH;
    [SerializeField] private TalentHolder _maxHealthTH;
    [SerializeField] private TalentHolder _ammoDamageTH;
    [SerializeField] private TalentHolder _ammoCapacityTH;
    [SerializeField] private TalentHolder _pierceTH;

    private BaseGunController _baseGunController;
    private FPSController _fPSController;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InputReader.Instance.Controls.Disable();
        Cursor.visible = true;
        Time.timeScale = 0f;

        SetCurrentTalentPointText();
        if (_baseGunController == null)
        {
            _baseGunController = FindObjectOfType<BaseGunController>();
        }
        _fPSController = GameManager.Instance.FPSController;

        FillHolders();
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        InputReader.Instance.Controls.Enable();
        Time.timeScale = 1f;
    }

    public void SetCurrentTalentPointText()
    {
        _currentTalentPointText.SetText("Current Talent Point: " + ExperienceSystem.Instance.CurrentTalentPoint.ToString());
    }

    public void FillHolders()
    {
        _walkAndSprintTH.SetTalent(_fPSController.PlayerStats.GetWalkTalent());
        _jumpTH.SetTalent(_fPSController.PlayerStats.GetJumpTalent());
        _maxHealthTH.SetTalent(_fPSController.PlayerStats.GetHealthTalent());
        _ammoDamageTH.SetTalent(_baseGunController.DamageTalent);
        _ammoCapacityTH.SetTalent(_baseGunController.AmmoTalent);
        _pierceTH.SetTalent(_baseGunController.PierceTalent);
        SetCurrentTalentPointText();
    }
    public void OnContinueButtonClicked()
    {
        gameObject.SetActive(false);
        int currentHealth = _fPSController.CurrentHealth;
        int currentMaxHealth = _fPSController.PlayerStats.GetHealthValue();
        if (_fPSController.PlayerStats.PreviousHealthValue() == _fPSController.CurrentHealth)
        {
            currentHealth = currentMaxHealth;
        }

        UIController.Instance.SetHealthBar(currentHealth, currentMaxHealth);
        //set max health 
        //set max ammo
    }
}
