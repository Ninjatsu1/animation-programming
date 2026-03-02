using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> KeyItems = new List<GameObject>();
    public List<GameObject> Collectibles = new List<GameObject>();

    public void AddItem(GameObject itemToAdd)
    {
        if (itemToAdd != null) 
        {
            var itemType = itemToAdd.GetComponent<Collectable>().ItemType;

            if (itemType == ItemType.Collectible)
            {
                Collectibles.Add(itemToAdd);
            } else
            {
                KeyItems.Add(itemToAdd);
            }

        }
    }

    public bool GetKeyItem(GameObject itemToGet)
    {
        if (!KeyItems.Contains(itemToGet))
        {
            return false;
        } else
        {
            return true;
        }
    }
}
