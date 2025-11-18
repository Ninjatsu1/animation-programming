using UnityEngine;

[CreateAssetMenu(fileName = "GunType", menuName = "Scriptable Objects/GunType")]
public class GunType : ScriptableObject
{

    [Header("Gun Info")]
    public string gunName;
    public WeaponType.Weapon weapon;

    [Header("Stats")]
    public float damage = 10f;
    public float fireRate = 0.25f;
    public float bulletSpeed = 50f;
    public int ammo = 30;

    [Header("Effects")] //Implement these later!
    public GameObject bulletPrefab;
    public ParticleSystem muzzleFlash;
    public AudioClip fireSound;
}

public class WeaponType
{
    public enum Weapon
    {
        Pistol,
        DualPistol,
        LaserGun,
        AssaultRifle,
        ShotGun,

    }

}