using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    private Transform _targetDestination;
    
   [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _player = PlayerManager.Instance.Player;
        _targetDestination = _player.transform;
        _agent.destination = _targetDestination.position;
    }

    private void Update()
    {
        if(_agent.remainingDistance <= 1f)
        {
            Debug.Log("attacking mode");
        }
    }
}
