using System;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    public Action<string> WeaponFired;

    [SerializeField] private WeaponShoot _weaponShoot;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private Animator _animator;


    private void OnEnable()
    {
        _weaponController.OnShoot += ShootAnimation;
    }

    private void OnDisable()
    {
        _weaponController.OnShoot -= ShootAnimation;
    }

    private void ShootAnimation(int animationHash)
    {
        _animator.SetTrigger(animationHash);
    }

    public void FireEvent(string whichWeapon)
    {
        WeaponFired?.Invoke(whichWeapon);
    }
}
