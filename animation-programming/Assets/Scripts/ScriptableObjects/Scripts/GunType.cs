using UnityEngine;

[CreateAssetMenu(fileName = "GunType", menuName = "Scriptable Objects/GunType")]
public class GunType : ScriptableObject
{

    [Header("Gun Info")]
    public string gunName;
    public WeaponType.Weapon weapon;

    [Header("Stats")]
    public float Damage = 10f;
    public float FireRate = 0.25f;
    public float BulletSpeed = 50f;
    public int Ammo = 30;

    [Header("Effects")] //Implement these later!
    public GameObject BulletPrefab;
    public ParticleSystem MuzzleFlash;
    public AudioClip FireSound;
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