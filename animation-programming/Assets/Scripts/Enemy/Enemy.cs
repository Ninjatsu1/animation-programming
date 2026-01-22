using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    private Transform _targetDestination;
    private bool _isAttacking = false;
    private int _playerLayer;
    private Collider[] results = new Collider[1];

    [SerializeField] private float _agentStoppingDistance = 2.5f;
    [SerializeField] private float _aggroRange = 5f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    

    private void Awake()
    {
        _agent.stoppingDistance = _agentStoppingDistance;
        _playerLayer = LayerMask.NameToLayer("Player");
        _agent.speed = _movementSpeed;
        
    }

    private void Start()
    {
        _player = PlayerManager.Instance.Player;
    }

    private void Update()
    {
        SetMovementAnimation();

        if (IsPlayerInRange(transform.position, _aggroRange))
        {
                  
            _agent.destination = _player.transform.position;

        }
    }

    private void SetMovementAnimation()
    {
        float speed = _agent.velocity.magnitude;
        _enemyAnimation.MovementAnimation(speed);
    }

    bool IsPlayerInRange(Vector3 position, float radius)
    {
        int count = Physics.OverlapSphereNonAlloc(
            position,
            radius,
            results,
            _playerLayer
        );

        return count > 0;
    }
}
