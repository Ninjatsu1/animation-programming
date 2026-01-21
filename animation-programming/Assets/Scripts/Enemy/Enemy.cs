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


    private void Awake()
    {
        _agent.stoppingDistance = _agentStoppingDistance;
        _playerLayer = LayerMask.NameToLayer("Player");

    }

    private void Start()
    {
        _player = PlayerManager.Instance.Player;
    }

    private void Update()
    {
        if (IsPlayerInRange(transform.position, _aggroRange))
        {
            _agent.destination = _player.transform.position;

        }
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
