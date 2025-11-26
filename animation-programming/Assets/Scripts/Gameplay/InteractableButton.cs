using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    [SerializeField] private GameObject _portToOpen;


    private void OnEnable()
    {
        Bullet.Interact += OnInteract;    
    }

    private void OnDisable()
    {
        Bullet.Interact -= OnInteract;

    }

    private void OnInteract(GameObject collidedObject)
    {
        if(collidedObject == gameObject)
        {
            _portToOpen.SetActive(false);
        }
    }
}
