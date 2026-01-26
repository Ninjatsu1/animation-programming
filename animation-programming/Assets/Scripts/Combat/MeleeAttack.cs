using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private Collider _collider;

    public void TurnCollisionOnMeleeEvent()
    {
        _collider.enabled = true;
    }

    public void TurnCollisionOffMeleeEvent()
    {
        _collider.enabled = false;
    }
    
}
