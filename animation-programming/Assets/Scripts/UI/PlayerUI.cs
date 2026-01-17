using UnityEngine;
using TMPro;
using System;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    private CharacterHealth _characterHealth;

    private GameObject _playerStats = null;

    private void Start()
    {
        GetPlayer();
        SetPlayerUI();
    }

    private void GetPlayer()
    {
        _playerStats = PlayerManager.Instance.Player;

    }

    private void SetPlayerUI()
    {
        _playerHealthText.text = "1"; //_playerStats.CurrentHealth.ToString();
    }
}
