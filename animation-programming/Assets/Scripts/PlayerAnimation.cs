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

    public void MovementAnimation(float currentSpeed)
    {
        _animator.SetFloat("Speed", currentSpeed);
    }

    public void FireWeaponAnimation()
    {
        //_animator.SetBool("IsShooting", true);
        _animator.SetTrigger("IsShootingT");
    }
}
