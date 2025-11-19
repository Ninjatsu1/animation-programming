using System;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{

    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private WeaponAnimation _weaponAnimation;
    [SerializeField] GunType _gunType;
    [SerializeField] Transform _bulletSpawnPositionLeft;
    [SerializeField] Transform _bulletSpawnPositionRight;

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
        GameObject bullet = Instantiate(_gunType.bulletPrefab, _bulletSpawnPositionLeft.position, _bulletSpawnPositionLeft.rotation);
        bullet.GetComponent<Bullet>().SetDamage(_gunType.damage);
        bullet.SetActive(true);
    }

}
