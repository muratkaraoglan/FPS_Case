using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class UIController : Singleton<UIController>
{
    [Header("Damage Indicator")]
    [SerializeField] private Image _damageIndicatorImage;
    [SerializeField] private TextMeshProUGUI _damageText;

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

    [Header("Crosshair")]
    [SerializeField] private Transform _innerCrosshairTransform;
    [SerializeField] private Image _crossHairImage;
    [SerializeField] private Image _innerCrosshairImage;

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

    bool _isPunchScaleFinished = true;
    public void OnHitEnemy()
    {
        if (_isPunchScaleFinished)
        {
            StartCoroutine(PunchScale(_innerCrosshairTransform, .08f, 1.2f));
        }
    }

    bool _isDamageTextAnimaStarted;
    Coroutine _damageTextCoroutine;
    int _targetDamageValue;
    public void ShowDamageText(int damage)
    {
        _targetDamageValue += damage;
        if (_isDamageTextAnimaStarted)
        {
            StopCoroutine(_damageTextCoroutine);
        }
        _damageTextCoroutine = StartCoroutine(AnimateDamageText(.5f, .4f));
    }

    bool _isFadeStarted = false;
    public void OnPlayerTakeDamage()
    {
        if (_isFadeStarted) return;
        StartCoroutine(FadeImage(_damageIndicatorImage, 0, .5f, .4f));
    }

    void ChangeScoreTexts()
    {
        _highScoreText.SetText(StringHelper.HIGH_SCORE + _highScore.ToString());
        _currentScoreText.SetText(StringHelper.CURRENT_SCORE + _currentScore.ToString());
        _killedEnemyCounterText.SetText(StringHelper.KILLED_ENEMY_COUNT + _killedEnemyCount);
    }


    IEnumerator PunchScale(Transform scaleTransform, float duration, float targetScaleRate)
    {
        _isPunchScaleFinished = false;
        Vector3 originalScale = scaleTransform.localScale;
        Vector3 targetScale = Vector3.one * targetScaleRate;
        float currentAnimTime = 0;

        do
        {
            float progress = currentAnimTime / duration;
            scaleTransform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            _crossHairImage.color = Color.Lerp(Color.white, Color.red, progress);
            _innerCrosshairImage.color = Color.Lerp(Color.white, Color.red, progress);
            currentAnimTime += Time.deltaTime;
            yield return null;
        }
        while (currentAnimTime <= duration);

        currentAnimTime = 0f;

        do
        {
            float progress = currentAnimTime / duration;
            scaleTransform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            _crossHairImage.color = Color.Lerp(Color.red, Color.white, progress);
            _innerCrosshairImage.color = Color.Lerp(Color.red, Color.white, progress);
            currentAnimTime += Time.deltaTime;
            yield return null;
        }
        while (currentAnimTime <= duration);
        scaleTransform.localScale = originalScale;
        currentAnimTime = 0f;
        _isPunchScaleFinished = true;
    }

    IEnumerator FadeImage(Image image, float from, float to, float duration)
    {
        float currentFadeTime = 0f;

        _isFadeStarted = true;
        do
        {
            float progress = currentFadeTime / duration;
            Color c = image.color;
            c.a = Mathf.Lerp(from, to, progress);
            image.color = c;
            currentFadeTime += Time.deltaTime;
            yield return null;
        } while (currentFadeTime <= duration);

        currentFadeTime = 0f;
        do
        {
            float t = currentFadeTime / duration;
            Color c = image.color;
            c.a = Mathf.Lerp(to, from, t);
            image.color = c;
            currentFadeTime += Time.deltaTime;
            yield return null;
        } while (currentFadeTime <= duration);
        _isFadeStarted = false;
    }

    int _currentDamage = 0;
    IEnumerator AnimateDamageText(float duration, float screenDuration)
    {
        float elapsedTime = 0f;
        _isDamageTextAnimaStarted = true;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(_currentDamage, _targetDamageValue, progress));
            _damageText.SetText(currentValue.ToString());
            _currentDamage = currentValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(screenDuration);
        _damageText.SetText("");
        _currentDamage = 0;
        _targetDamageValue = 0;
        _isDamageTextAnimaStarted = false;
    }
}
