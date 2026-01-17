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


}
