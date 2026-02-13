using System;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{

    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private WeaponAnimation _weaponAnimation;
    [SerializeField] GunType _gunType;
    [SerializeField] Transform _bulletSpawnPositionLeft;
    [SerializeField] Transform _bulletSpawnPositionRight;
    
    private GameObject _bullet;

    private void Awake()
    {
        _bullet = _gunType.BulletPrefab;
    }

    private void OnEnable()
    {
        _weaponAnimation.WeaponFired += Fire;   
    }

    private void OnDisable()
    {
        _weaponAnimation.WeaponFired -= Fire;
    }

    public void Fire(string whichWeaponFired)
    {
        Transform spawnPoint = whichWeaponFired == "Left" ? _bulletSpawnPositionLeft : _bulletSpawnPositionRight;

        Vector3 shootDirection = spawnPoint.forward;

        GameObject bullet = Instantiate(_bullet, spawnPoint.position, Quaternion.identity);
        bullet.SetActive(true);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.SetDamage(_gunType.Damage);
        bulletScript.SetDamageSource(gameObject);
        bullet.transform.rotation = Quaternion.LookRotation(shootDirection);
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(shootDirection.normalized * 30f, ForceMode.Impulse);

        Debug.DrawRay(spawnPoint.position, shootDirection * 3f, Color.blue, 5f);

    }

}
