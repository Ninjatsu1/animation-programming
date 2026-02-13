using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public static Action<WeaponController, int> UpdateAmmoCount;

    private PlayerInputControls _playerInput;
    public Action<int> OnShoot;
    private bool _useLeftGunNext = true;
    private int _magazineSize = 1;
    private int _currentAmmoCount = 1;

    [SerializeField] private GunType _gunType;
    [SerializeField] private bool _automaticReloadOn = true;
    [SerializeField] private WeaponAnimation _weaponAnimation;

    private void Awake()
    {
        _magazineSize = _gunType.MagazineSize;
        _currentAmmoCount = _magazineSize;
    }

    private void Start()
    {
        _playerInput = PlayerManager.Instance.PlayerInput;
        _playerInput.Player.Fire.performed += OnFire;
        _playerInput.Player.Reload.performed += OnReload;
        UpdateAmmoCount?.Invoke(this, _currentAmmoCount);
    }

    private void OnDisable()
    {
        _playerInput.Player.Fire.performed -= OnFire;

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        _currentAmmoCount -= 1;
        UpdateAmmoCount?.Invoke(this, _currentAmmoCount);
        if(_currentAmmoCount >= 0 )
        {
            ShootGunAnimation();

        } else
        {
            if(_automaticReloadOn)
            ReloadGun();
        }
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

    private void OnReload(InputAction.CallbackContext context)
    {
        ReloadGun();
    }

    private void ReloadGun()
    {
        _currentAmmoCount = _magazineSize;
        UpdateAmmoCount?.Invoke(this, _currentAmmoCount);
        _weaponAnimation.ReloadAnimation();
    }

}
