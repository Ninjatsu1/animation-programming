using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    private Transform _targetDestination;
    private int _playerLayer;
    private Collider[] results = new Collider[1];
    private bool _attackOnCooldown;

    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private float _agentStoppingDistance = 2.5f;
    [SerializeField] private float _aggroRange = 5f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private float _enemyMeleeDistance = 1f;

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
        IsPlayerInMeleeRange();

        if (_isAttacking && !_attackOnCooldown)
        {
            StartCoroutine(MeleeAttack());
        }

        if (IsPlayerInDetectionRange(transform.position, _aggroRange))
        {
            _agent.destination = _player.transform.position;
        }

    }

    private void SetMovementAnimation()
    {
        float speed = _agent.velocity.magnitude;
        _enemyAnimation.MovementAnimation(speed);
    }

    private void IsPlayerInMeleeRange()
    {
        float sqrDistance = (transform.position - _player.transform.position).sqrMagnitude;

        _isAttacking = sqrDistance <= _enemyMeleeDistance;
    }

    private IEnumerator MeleeAttack()
    {
        _attackOnCooldown = true;

        _enemyAnimation.MeleeAttack();

        yield return new WaitForSeconds(1f);

        _attackOnCooldown = false;
    }

    bool IsPlayerInDetectionRange(Vector3 position, float radius)
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
