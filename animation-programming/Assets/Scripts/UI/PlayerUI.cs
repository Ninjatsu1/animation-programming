using UnityEngine;
using TMPro;
using System;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private CharacterStats _playerStats;

    private CharacterHealth _playerHealth;

    private GameObject _player = null;

    private void Awake()
    {
        SetPlayerUI();
    }

    private void OnEnable()
    {
        WeaponController.UpdateAmmoCount += UpdateAmmoCount;
    }

    private void Start()
    {
        GetPlayer();
        _playerHealth = _player.GetComponent<CharacterHealth>();
        _playerHealth.OnHealthChanged += UpdateHealth;

    }

    private void UpdateHealth(CharacterHealth characterHealth, float currentHealth, float maxHealth)
    {
        if (characterHealth.gameObject.CompareTag("Player"))
        { 
            _playerHealthText.text = currentHealth.ToString();
        }
    }

    private void UpdateAmmoCount(WeaponController weaponController, int ammoCount)
    {
        Debug.Log("Update ammo count");
        if(weaponController.gameObject == _player)
        _ammoCountText.text = ammoCount.ToString();
    }

    private void GetPlayer()
    {
        _player = PlayerManager.Instance.Player;

    }

    private void SetPlayerUI()
    {
       _playerHealthText.text = _playerStats.CurrentHealth.ToString();
    }


    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= UpdateHealth;
        WeaponController.UpdateAmmoCount -= UpdateAmmoCount;

    }
}
