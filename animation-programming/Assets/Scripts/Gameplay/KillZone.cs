using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("player collided");
            GameObject player = other.gameObject;
            player.GetComponent<CharacterHealth>().InstaKill();
        }
    }
}
