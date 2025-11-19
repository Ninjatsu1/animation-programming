using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 10f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _bulletDamage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(transform.forward * _forceAmount, ForceMode.Impulse);    
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
        Debug.Log("Collided");
        var collidedObject = collision.gameObject;

        if (collidedObject.gameObject.CompareTag("Enemy")) 
        {
            collidedObject.GetComponent<CharacterHealth>().DamageHealth(_bulletDamage);
            Debug.Log("Enemy detected");
        }
        DespawnBullet();

    }

}
