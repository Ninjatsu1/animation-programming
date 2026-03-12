using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private string _sceneName;

    private void Awake()
    {
        _exitGameButton.onClick.AddListener(OnExitButton);
        _startGameButton.onClick.AddListener(OnStartGameButton);
    }

    


    private void OnStartGameButton()
    {
        _exitGameButton.onClick.RemoveListener(OnExitButton);
        _startGameButton.onClick.RemoveListener(OnStartGameButton);
        SceneManager.LoadScene(_sceneName);
    }

    public void OnExitButton()
    {
        Debug.Log("Exit Game");

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
