using UnityEngine;

public class ItemCheck : MonoBehaviour
{
    [SerializeField] private GameObject _itemRequired;

    private bool _playerInInteractionZone = false;
    private GameObject _player;
    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _playerInInteractionZone = true;
        }
    }

    public void CheckPlayerItem(GameObject itemToCheck)
    {
        var inventory = _player.GetComponent<Inventory>();

    }


    
}
