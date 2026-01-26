using UnityEngine;

public class MeleeHitDetector : MonoBehaviour
{

    private LayerMask _playerLayer;

    private void Awake()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        var collidedObject = other.gameObject;

        if (collidedObject.layer == _playerLayer)
        {
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.DamageAmount = 1;
            damageInfo.Source = gameObject;

            collidedObject.GetComponent<CharacterHealth>().DamageHealth(damageInfo);
        }
    }

}
