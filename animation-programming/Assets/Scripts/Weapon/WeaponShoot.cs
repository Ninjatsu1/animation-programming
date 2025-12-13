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
        // Determine spawn point based on which weapon fired
        Transform spawnPoint = whichWeaponFired == "Left" ? _bulletSpawnPositionLeft : _bulletSpawnPositionRight;

        // Determine shooting direction
        Vector3 shootDirection = spawnPoint.forward;

        // Instantiate bullet at spawn position
        GameObject bullet = Instantiate(_bullet, spawnPoint.position, Quaternion.identity);
        bullet.SetActive(true);

        // Get components
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Set bullet properties
        bulletScript.SetDamage(_gunType.Damage);
        bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

        // Reset velocity and apply force
        rb.linearVelocity = Vector3.zero;  // Use rb.velocity, not rb.linearVelocity
        rb.AddForce(shootDirection.normalized * 30f, ForceMode.Impulse);

        // Optional: debug ray
        Debug.DrawRay(spawnPoint.position, shootDirection * 3f, Color.blue, 5f);

        // Debug log
        Debug.Log($"{whichWeaponFired} pistol fired!");
    }

}
