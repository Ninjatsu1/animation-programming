using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 10f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _lifeTime = 10f;

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
            BulletDespawn();
        }
    }

    private void BulletDespawn()
    {
        GameObject.Destroy(gameObject); //Make pooling later
    }

}
