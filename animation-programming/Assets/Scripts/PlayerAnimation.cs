using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void WalkingAnimation(float currentSpeed)
    {
        _animator.SetFloat("Speed", currentSpeed);
    }
}
