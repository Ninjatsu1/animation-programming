using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    public Action<CharacterHealth, float, float> OnHealthChanged;
    //Current health, max health
    public event Action<CharacterHealth, DamageInfo> OnDeath;

    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] float _currentHealth = 1f;
    [SerializeField] float _maximumHealth = 1f;

    private bool _isPlayer  => GetComponent<Player>() != null;
    private bool _isEnemy => GetComponent<Enemy>() != null;


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

    private void Die(DamageInfo damageInfo)
    {
        OnDeath?.Invoke(this, damageInfo);
        Despawn();
    }

    public void InstaKill()
    {
        if (_isPlayer)
        {
            OnHealthChanged?.Invoke(this, _maximumHealth, 0);
        }
        else if (_isEnemy)
        { 
            gameObject.SetActive(false);
        }
       
    }
    


    public void Despawn()
    {
        gameObject.SetActive(false);
    }
}

public struct DamageInfo
{
    public float DamageAmount;
    public GameObject Source;
}
