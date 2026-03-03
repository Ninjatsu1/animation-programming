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

    
    private Vector3 TargetPoint()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
          return  targetPoint = hit.point;
        }
        else
        {
          return  targetPoint = ray.GetPoint(1000f);
        }
    }


    public void Fire(string whichWeaponFired)
    {
        Transform spawnPoint = whichWeaponFired == "Left" ? _bulletSpawnPositionLeft : _bulletSpawnPositionRight;

        Vector3 shootDirection = (TargetPoint() - spawnPoint.position).normalized;

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
