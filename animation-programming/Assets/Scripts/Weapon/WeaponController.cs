using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private PlayerInputControls _playerInput;
    public Action<int> OnShoot;
    private bool _useLeftGunNext = true;
    
    private void Start()
    {
        _playerInput = PlayerManager.Instance.PlayerInput;
        _playerInput.Player.Fire.performed += OnFire;
    }

    private void OnDisable()
    {
        _playerInput.Player.Fire.performed -= OnFire;

    }

    public void OnFire(InputAction.CallbackContext context)
    {

        ShootGunAnimation();
    }

    private void ShootGunAnimation()
    {
        if (_useLeftGunNext)
        {
            OnShoot(AnimationParams.SHOOT_LEFT);
        } 
        else 
        {
            OnShoot(AnimationParams.SHOOT_RIGHT);
        }
        _useLeftGunNext = !_useLeftGunNext;

    }

}
