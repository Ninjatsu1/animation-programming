using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    public void MovementAnimation(float currentSpeed)
    {
        _animator.SetFloat(AnimationParams.SPEED, currentSpeed);
    }
    
}
