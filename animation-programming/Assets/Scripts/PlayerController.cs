using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSmoothTime = 0.1f;

    public float Speed = 5f;
    public Vector3 Move;

    private PlayerAnimation _playerAnimation;
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _mouseInput;
    private Vector3 _velocity;
    private Vector3 _previousPosition;
    private float rotationVelocity;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        _playerInput.Player.Look.performed += OnLook;
        _playerInput.Player.Look.canceled += OnLook;

    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.canceled -= OnMove;
        _playerInput.Player.Move.performed -= OnLook;
        _playerInput.Player.Look.canceled -= OnLook;
        _playerInput.Player.Disable();
    }

    private void Update()
    {
        MovePlayer();
        UpdateVelocity();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
        Debug.Log("Mouse is moving ");
    }

    private void MovePlayer()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * _moveInput.y + camRight * _moveInput.x;

        if (moveDir.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            _controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }

        _playerAnimation.WalkingAnimation(_velocity.magnitude);
    }


    private void UpdateVelocity()
    {
        _velocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
    }
}