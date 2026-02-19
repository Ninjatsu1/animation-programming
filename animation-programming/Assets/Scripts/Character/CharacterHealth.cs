using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    public Action<CharacterHealth, float, float> OnHealthChanged;
    //Current health, max health
    public static event Action<CharacterHealth, DamageInfo> OnDeath;

    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private float _currentHealth = 1f;
    [SerializeField] private float _maximumHealth = 1f;

    private void Awake()
    {
        _maximumHealth = _characterStats.MaximumHealth;
        _currentHealth = _characterStats.MaximumHealth;
        OnHealthChanged?.Invoke(this, _currentHealth, _maximumHealth);
    }


   public void DamageHealth(DamageInfo damageInfo)
    {
        _currentHealth -= damageInfo.DamageAmount;
        OnHealthChanged?.Invoke(this, _currentHealth, _maximumHealth);
        if (_currentHealth <= 0f)
        {
            Die(damageInfo);
        }
    }

    public void InstaKill()
    {
        OnHealthChanged?.Invoke(this, 0, _maximumHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maximumHealth;
        OnHealthChanged(this, _currentHealth, _maximumHealth);

    }

    private void Die(DamageInfo damageInfo)
    {
        OnDeath?.Invoke(this, damageInfo);
    }
}

public struct DamageInfo
{
    public float DamageAmount;
    public GameObject Source;
}
