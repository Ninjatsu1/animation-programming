using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private CharacterHealth _playerHealth;
    public Action PlayerDied;

    private void Awake()
    {
        _playerHealth = GetComponent<CharacterHealth>();
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += Die;
    }

    private void Die(CharacterHealth character, float currentHealth, float maximumHealth)
    {
        if(currentHealth <= 0)
        {
            PlayerDied?.Invoke();
        }  
    }
}
