using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{


    public float Speed = 5f;
    public float jumpHeight = 2f;

    [SerializeField] private float _rotationSmoothTime = 0.1f;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    
    private float _gravity = 9.81f;
    private PlayerAnimation _playerAnimation;
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _mouseInput;
    private Vector3 _velocity;
    private Vector3 _previousPosition;
    private float rotationVelocity;
    private Vector3 _move;


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
        _playerInput.Player.Sprint.performed += OnSprint;
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        _playerInput.Player.Look.performed += OnLook;
        _playerInput.Player.Look.canceled += OnLook;
        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Jump.canceled += OnJump;
        _playerInput.Player.Fire.performed += OnFire;

    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.canceled -= OnMove;
        _playerInput.Player.Move.performed -= OnLook;
        _playerInput.Player.Look.canceled -= OnLook;
        _playerInput.Player.Jump.performed -= OnJump;
        _playerInput.Player.Jump.canceled -= OnJump;
        _playerInput.Player.Fire.performed -= OnFire;
        _playerInput.Player.Disable();
    }

    private void Update()
    {
        CreateGroundCheck();
        MovePlayer();
        ApplyGravity();
        _previousPosition = transform.position;
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y -= _gravity * Time.deltaTime; // fall down
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


    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        Jump();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprinting");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FireWeapon();
        }
    }

    private void CreateGroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
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

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        Vector3 move = moveDir.normalized * Speed + Vector3.up * _velocity.y;
        _controller.Move(move * Time.deltaTime);

        _playerAnimation.MovementAnimation(moveDir.magnitude * Speed);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * 2f * _gravity); // go up

        }
    }


    private void UpdateVelocity()
    {
        _velocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
    }

    private void FireWeapon()
    {
        Debug.Log("Bang");
    }


}