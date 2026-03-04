using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    private Transform _targetDestination;
    [SerializeField] LayerMask _playerLayer;
    private Collider[] results = new Collider[1];
    private bool _attackOnCooldown;

    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private float _agentStoppingDistance = 2.5f;
    [SerializeField] private float _aggroRange = 5f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private float _enemyMeleeDistance = 1f;
    [SerializeField] private float _meleeAttackCooldown = 1f;

    private void Awake()
    {
        _agent.stoppingDistance = _agentStoppingDistance;
        _agent.speed = _movementSpeed;
        
    }

    private void OnEnable()
    {
        CharacterHealth.OnDeath += Die;
    }

    private void Start()
    {
        _player = PlayerManager.Instance.Player;
    }

    private void Update()
    {
        IsPlayerInDetectionRange();
        SetMovementAnimation();
        IsPlayerInMeleeRange();
        if(IsPlayerInDetectionRange())
        {
            _agent.destination = _player.transform.position;

        }

        if (_isAttacking && !_attackOnCooldown)
        {
            StartCoroutine(MeleeAttack());
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

        yield return new WaitForSeconds(_meleeAttackCooldown);

        _attackOnCooldown = false;
    }

    private void Die(CharacterHealth characterHealth, DamageInfo damageInfo)
    {
        if(characterHealth.gameObject == gameObject)
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsPlayerInDetectionRange()
    {

        Vector3 distance = _player.transform.position - transform.position;
        if (distance.sqrMagnitude <= _aggroRange * _aggroRange)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
