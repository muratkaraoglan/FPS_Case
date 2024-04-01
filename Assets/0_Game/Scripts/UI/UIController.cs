using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _killedEnemyCounterText;

    [Header("Panels")]
    [SerializeField] private GameObject _gameOverPanel;

    private int _currentScore;
    private int _highScore;
    private int _killedEnemyCount;

    private void OnEnable()
    {
        _highScore = PlayerPrefs.GetInt(StringHelper.HIGH_SCORE_PREF, 0);
        ChangeScoreTexts();
    }
    private void Start()
    {
        GameManager.Instance.FPSController.OnPlayerDie += FPSController_OnPlayerDie;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(StringHelper.HIGH_SCORE_PREF, _highScore);
    }

    private void FPSController_OnPlayerDie()
    {
        _gameOverPanel.SetActive(true);
    }

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

    public void SetScoreTexts(int score)
    {
        _killedEnemyCount++;
        _currentScore += score;

        if (_currentScore > _highScore) _highScore = _currentScore;
        ChangeScoreTexts();
    }

    public void OnRestartButtonClicled()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void ChangeScoreTexts()
    {
        _highScoreText.SetText(StringHelper.HIGH_SCORE + _highScore.ToString());
        _currentScoreText.SetText(StringHelper.CURRENT_SCORE + _currentScore.ToString());
        _killedEnemyCounterText.SetText(StringHelper.KILLED_ENEMY_COUNT + _killedEnemyCount);
    }
}
