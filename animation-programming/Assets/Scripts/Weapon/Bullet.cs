using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 10f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _bulletDamage;

    private int _enemyLayer;
    private int _interactableLayer;

    public static Action<GameObject> Interact;
   

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided: " + collision.gameObject.name);
        var collidedObject = collision.gameObject;

        if(collidedObject.layer == _enemyLayer)
        {
            var health = collidedObject.GetComponent<CharacterHealth>();

            health.DamageHealth(_bulletDamage);
        }

        if (collidedObject.layer == _interactableLayer)
        {
            Interact?.Invoke(collidedObject);
        }
        
        DespawnBullet();

    }

}
