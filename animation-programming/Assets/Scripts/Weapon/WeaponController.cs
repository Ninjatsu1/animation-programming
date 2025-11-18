using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private PlayerInput _playerInput;
    public Action<int> OnShoot;
    private bool _useLeftGunNext = true;

    private void Awake()
    {
        _playerInput = new PlayerInput();

    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Fire.performed += OnFire;
    }

    private void OnDisable()
    {
        _playerInput.Player.Fire.performed -= OnFire;
        _playerInput.Disable();

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
