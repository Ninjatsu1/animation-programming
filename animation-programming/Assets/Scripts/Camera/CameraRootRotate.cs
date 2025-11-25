using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRootRotate : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float _minPitch = -40f;
    public float _maxPitch = 70f;

    private float _yaw;
    private float _pitch;

    private PlayerInputControls _playerInput;
    private Vector2 _lookInput;

    [SerializeField] private GameObject _player;


    private void Start()
    {
        _playerInput = PlayerManager.Instance.PlayerInput;
        _playerInput.Player.Look.performed += OnLook;
        _playerInput.Player.Look.canceled += OnLook;
    }

    private void OnDisable()
    {
        _playerInput.Player.Look.performed -= OnLook;
        _playerInput.Player.Look.canceled -= OnLook;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        _yaw += _lookInput.x * rotationSpeed * Time.deltaTime;
        _pitch -= _lookInput.y * rotationSpeed * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
}
