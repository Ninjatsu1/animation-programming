using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 10f;
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _bulletDamage;

    private int _enemyLayer;
    private int _interactableLayer;
    private GameObject _damageSource;
    public static Action<GameObject> Interact;
   

    private void Awake()
    {
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _interactableLayer = LayerMask.NameToLayer("Interactable");
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) 
        {
            DespawnBullet();
        }
    }

    private void DespawnBullet()
    {
        GameObject.Destroy(gameObject); //Make pooling later
    }

    public void SetDamage(float damage)
    {
        _bulletDamage = damage;
    }

    public void SetDamageSource(GameObject damageSource)
    {
        _damageSource = damageSource;
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("Collided: " + collision.gameObject.name);
        var collidedObject = collision.gameObject;

        if(collidedObject.layer == _enemyLayer)
        {
            var health = collidedObject.GetComponent<CharacterHealth>();

            DamageInfo damageInfo = new DamageInfo();
            damageInfo.DamageAmount = 1;
            damageInfo.Source = _damageSource;

            health.DamageHealth(damageInfo);
        }

        if (collidedObject.layer == _interactableLayer)
        {
            Interact?.Invoke(collidedObject);
        }
        
        DespawnBullet();

    }

}
