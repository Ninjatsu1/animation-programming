using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInputControls _playerInput;
    private PlayerManager _playerManager;
    public static event Action PlayerInteracted;

    void Start()
    {
        _playerManager = PlayerManager.Instance;
        _playerInput = _playerManager.PlayerInput;
        _playerInput.Player.Interact.performed += OnInteract;
        
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        PlayerInteracted?.Invoke();
    }
}
