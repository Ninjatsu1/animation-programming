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
        _playerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (currentHealth <= 0) 
        {
            Debug.Log("ded");
            PlayerDied?.Invoke();
        }
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= OnHealthChanged;
    }

}
