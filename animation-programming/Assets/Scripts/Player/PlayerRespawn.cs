using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform _respawnLocation;
    private string _playerRespawn;
    private PlayerDeath _playerDied;
    private CharacterController _characterController;


    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private CharacterHealth _characterHealth;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerDied = GetComponent<PlayerDeath>();
        _playerRespawn = Tags.PLAYER_RESPAWN;
        GetRespawnLocation();
    }

    private void OnEnable()
    {
        //_playerDied.PlayerDied += OnPlayerDeath;
    }

    private void GetRespawnLocation() //Object should be found in levelmanager
    {
        _respawnLocation = GameObject.FindWithTag(_playerRespawn).transform;

    }

    public void PlayerDeathEvent()
    {
        RespawnPlayer();
        PlayerIsAliveAnimation();
        ResetPlayerHealth();
    }

    private void RespawnPlayer()
    {
        _characterController.enabled = false;
        transform.position = _respawnLocation.position;
        _characterController.enabled = true;
    }

    private void PlayerIsAliveAnimation()
    {
        _playerAnimation.PlayerIsAlive(true);
    }

    private void ResetPlayerHealth()
    {
        _characterHealth.ResetHealth();
    }

}
