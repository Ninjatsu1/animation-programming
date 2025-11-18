using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRootRotate : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float minPitch = -40f;
    public float maxPitch = 70f;

    private float yaw;
    private float pitch;

    private PlayerInput input;
    private Vector2 lookInput;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Player.Look.performed += OnLook;
        input.Player.Look.canceled += OnLook;
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Look.performed -= OnLook;
        input.Player.Look.canceled -= OnLook;
        input.Player.Disable();
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        yaw += lookInput.x * rotationSpeed * Time.deltaTime;
        pitch -= lookInput.y * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
