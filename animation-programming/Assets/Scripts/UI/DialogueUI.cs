using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueUI;

    private void OnEnable()
    {
        DialogueTrigger.DisplayDialogueUI += DisplayDialogueUI;
    }

    private void DisplayDialogueUI(bool isDisplayingUI)
    {
        if (isDisplayingUI)
        {
            _dialogueUI.SetActive(true);
        }
        else 
        {
            _dialogueUI.SetActive(false); 
        }
    }
}
