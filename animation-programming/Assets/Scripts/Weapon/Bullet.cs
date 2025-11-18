using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 10f;
    [SerializeField] private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(transform.forward * _forceAmount, ForceMode.Impulse);    
    }

    
}
