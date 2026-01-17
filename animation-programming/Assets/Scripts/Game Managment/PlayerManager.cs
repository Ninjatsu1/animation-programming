using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    
    public GameObject Player;

    public static PlayerManager Instance
    {
        get { return _instance; }
    }

    public PlayerInputControls PlayerInput;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        PlayerInput = new PlayerInputControls();
        PlayerInput.Enable();
    }
}
