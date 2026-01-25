using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void MovementAnimation(float currentSpeed)
    {
        _animator.SetFloat(AnimationParams.SPEED, currentSpeed);
    }

    
    public void MeleeAttack()
    {
        _animator.SetTrigger(AnimationParams.MELEE_ATTACK);        
    }

}
