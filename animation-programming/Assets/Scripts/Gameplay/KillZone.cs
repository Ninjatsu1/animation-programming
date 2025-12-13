using UnityEngine;

public class KillZone : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }



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
