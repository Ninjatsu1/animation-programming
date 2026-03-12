using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private string _sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
