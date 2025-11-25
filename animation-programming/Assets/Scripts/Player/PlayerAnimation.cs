using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerMovement;

    public void MovementAnimation(float currentSpeed)
    {
        Debug.Log("Current speed: " + currentSpeed);
        _animator.SetFloat(AnimationParams.SPEED, currentSpeed);
    }
    
}
