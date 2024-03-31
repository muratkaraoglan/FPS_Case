using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour, IDamagable
{
    [SerializeField, Min(1)] private int _maxHealth;
    [SerializeField] private int _exprienceAmount;
    [SerializeField] private Image _healthBarImage;
    [field: SerializeField] public int Damage { get; private set; }
    private int _currentHealth = 0;
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _healthBarImage.fillAmount = 1f;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        float fillAmount = Mathf.InverseLerp(0, _maxHealth, _currentHealth);
        _healthBarImage.fillAmount = fillAmount;
        if (_currentHealth <= 0)
        {
            ExperienceSystem.Instance.GainXP(_exprienceAmount);
            //dead
        }
    }
}
