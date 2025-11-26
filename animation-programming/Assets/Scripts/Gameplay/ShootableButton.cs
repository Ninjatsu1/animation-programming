using UnityEngine;

public class ShootableButton : MonoBehaviour
{

    private int _bulletLayer = LayerMask.NameToLayer("Projectile");

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
