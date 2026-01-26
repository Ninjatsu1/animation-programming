using UnityEngine;
using TMPro;
using System;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private CharacterHealth _playerHealth;
    [SerializeField] private CharacterStats _playerStats;

    private GameObject _player = null;

    private void Awake()
    {
        SetPlayerUI();
    }

    private void Start()
    {
        GetPlayer();
        _playerHealth = _player.GetComponent<CharacterHealth>();
        _playerHealth.OnHealthChanged += UpdateHealth;

    }

    private void UpdateHealth(CharacterHealth characterHealth, float currentHealth, float maxHealth)
    {
        Debug.Log("Setting health");
        if (characterHealth.gameObject.CompareTag("Player"))
        { 
            _playerHealthText.text = currentHealth.ToString();
        }
    }

    private void GetPlayer()
    {
        _player = PlayerManager.Instance.Player;

    }

    private void SetPlayerUI()
    {
       _playerHealthText.text = _playerStats.CurrentHealth.ToString();
    }
}
