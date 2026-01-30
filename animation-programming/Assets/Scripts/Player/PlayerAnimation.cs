using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private bool _isInCombat = false; //This information should be recieved in player/level manager

    private int _fullBody = 0;
    private int _upperBody = 1;

    private void Start()
    {
        if (_isInCombat)
        {
            _animator.SetLayerWeight(_upperBody, 1);
        }
    }

    public void MovementAnimation(float currentSpeed)
    {
        _animator.SetFloat(AnimationParams.SPEED, currentSpeed);
    }

    public void SlidingAnimation(bool isSliding)
    {
        _animator.SetBool(AnimationParams.IS_SLIDING, isSliding);
    }

    public void CrouchingAnimation(bool isCrouching)
    {
        _animator.SetBool(AnimationParams.CROUCHING, isCrouching);
    }
    

    public void DeathAnimation()
    {
        Debug.Log("Player death animation");    
    }
}
