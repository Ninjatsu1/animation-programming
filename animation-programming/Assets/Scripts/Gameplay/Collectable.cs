using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemType ItemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<Inventory>();
            inventory.AddItem(gameObject);
            gameObject.SetActive(false);
        }
    }
}

public enum ItemType
{
    Collectible,
    KeyItem
}
