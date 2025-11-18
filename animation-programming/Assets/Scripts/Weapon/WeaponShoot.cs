using System;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{

    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private WeaponAnimation _weaponAnimation;
    [SerializeField] GunType _gunType;
    [SerializeField] Transform _bulletSpawnPositionLeft;
    [SerializeField] Transform _bulletSpawnPositionRight;
    [SerializeField] private GameObject _bullet;

    private void OnEnable()
    {
        _weaponAnimation.WeaponFired += Fire;   
    }

    private void OnDisable()
    {
        _weaponAnimation.WeaponFired -= Fire;
    }

    public void Fire()
    {
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue, 5f);
        Instantiate(_bullet, _bulletSpawnPositionLeft.position, _bulletSpawnPositionLeft.rotation);
    }

}
