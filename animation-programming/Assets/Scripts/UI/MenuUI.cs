using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private PlayerInputControls _input;
    private bool _menuOpen = false;

    [SerializeField] private GameObject _menuRoot;
    [SerializeField] private Button _exitGameButton;

    private void Awake()
    {
        _exitGameButton.onClick.AddListener(OnExitButton);
    }

    private void Start()
    {
        _input = PlayerManager.Instance.PlayerInput;

        _input.Player.MenuToggle.performed += OnMenuToggle;
        _input.UI.MenuToggle.performed += OnMenuToggle;
        _input.UI.Disable();
    }

    private void OnDisable()
    {
        if (_input != null)
        {
            _input.Player.MenuToggle.performed -= OnMenuToggle;
            _input.UI.MenuToggle.performed -= OnMenuToggle;
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Event system: " + EventSystem.current);
        }
    }

    private void OnMenuToggle(InputAction.CallbackContext context)
    {
        _menuOpen = !_menuOpen;
        _menuRoot.SetActive(_menuOpen);

        StartCoroutine(SwitchMapsNextFrame(_menuOpen));
    }

    private IEnumerator SwitchMapsNextFrame(bool isOpen)
    {
        yield return null;

        if (isOpen)
        {
            _input.Player.Disable();
            _input.UI.Enable();
            Debug.Log("Selected object" + EventSystem.current.currentSelectedGameObject);
            Debug.Log("UI enabled: " + _input.UI.enabled);
            Debug.Log("Player enabled: " + _input.Player.enabled);
        }
        else
        {
            _input.UI.Disable();
            _input.Player.Enable();
        }
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