using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _worldCanvas;
    [SerializeField] private bool _itemCheckRequired = false;
    [SerializeField] private GameObject _requiredItem;
    [SerializeField] private string dialogueLockedText = "Text...";
    [SerializeField] private ExitLevel _exitLevel;

    private bool _playerInInteractionZone = false;
    private bool _isDialogueOpen = false;
    private PlayerManager _playerManager;
    private Inventory _playerInventory;

    public static Action<bool> PlayerInInteractionZone;
    public static Action<bool> DisplayDialogueUI;

    private void Start()
    {
        PlayerInteract.PlayerInteracted += CheckPlayerInteract;
        _playerManager = PlayerManager.Instance;
        _playerInventory = _playerManager.Player.GetComponent<Inventory>();


    }

    private void CheckPlayerInteract()
    {
        if (_playerInInteractionZone)
        {
            CheckItem();
            _isDialogueOpen = !_isDialogueOpen;
            if (!CheckItem()) 
            {
                DisplayDialogueUI?.Invoke(_isDialogueOpen);
            } else
            {
                _exitLevel.LoadScene();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {        
        _worldCanvas.SetActive(true);
        _playerInInteractionZone = true;
        PlayerInInteractionZone?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _worldCanvas.SetActive(false);
        _playerInInteractionZone = false;
        PlayerInInteractionZone?.Invoke(false);
        DisplayDialogueUI?.Invoke(false);

    }

    private bool CheckItem()
    {
        if(_playerInventory.GetKeyItem(_requiredItem))
        {
            return true;
        } else
        {
            return false;
        }
    }

}
