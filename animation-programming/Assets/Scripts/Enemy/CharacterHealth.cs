using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    public Action<float, float> OnHealthChanged;
    //Current health, max health

    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] float _currentHealth = 1f;
    [SerializeField] float _maximumHealth =1f;

    private void Awake()
    {
        _maximumHealth = _characterStats.MaximumHealth;
        _currentHealth = _characterStats.MaximumHealth;
        OnHealthChanged?.Invoke(_currentHealth, _maximumHealth);
    }


   public void DamageHealth(float damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth, _maximumHealth);
        if (_currentHealth <= 0f)
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false); //Not sure if object should be destroyed. Might changed later if enemies spawns
    }
}
