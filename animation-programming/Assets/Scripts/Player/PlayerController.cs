using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 3f;
    public float SprintSpeed = 6f;
    public float jumpHeight = 2f;
    public float slidingSpeedIncrease = 3f;

    [SerializeField] private float _rotationSmoothTime = 0.1f;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    
    private PlayerInputControls _playerInput;
    
    private bool isSprinting = false;
    private static float _gravity = 9.81f;
    private PlayerAnimation _playerAnimation;
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _mouseInput;
    private Vector3 _velocity;
    private Vector3 _previousPosition;
    private float rotationVelocity;
    private PlayerManager _playerManager;
    private bool isSliding = false;
    private bool isCrouching = false;

    private void Awake()
    {

        _controller = GetComponent<CharacterController>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        _playerInput = _playerManager.PlayerInput;
        _playerInput = _playerManager.PlayerInput;

        _playerInput.Player.Sprint.performed += OnSprint;
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Sprint.performed += OnSprint;
        _playerInput.Player.Sprint.canceled += OnSprintCancel;
        _playerInput.Player.Slide.performed += OnSliding;
        _playerInput.Player.Slide.canceled += OnSlidingCancel;
        _playerInput.Player.Crouch.performed += OnCrouch;
        _playerInput.Player.Crouch.canceled += OnCrouchCancel;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.canceled -= OnMove;
        _playerInput.Player.Jump.performed -= OnJump;
        _playerInput.Player.Sprint.performed -= OnSprint;
        _playerInput.Player.Sprint.performed -= OnSliding;
        _playerInput.Player.Slide.canceled -= OnSlidingCancel;
        _playerInput.Player.Crouch.performed -= OnCrouch;
        _playerInput.Player.Crouch.canceled -= OnCrouchCancel;

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

        _velocity.y -= _gravity * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }

    public void OnSprintCancel(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }

    public void OnSliding(InputAction.CallbackContext context)
    {
        isSliding = true;
    }

    public void OnSlidingCancel(InputAction.CallbackContext context)
    {
        isSliding = false;
        Slide(isSliding);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = true;
        Crouch(isCrouching);
    }

    public void OnCrouchCancel(InputAction.CallbackContext context)
    {
        isCrouching = false;
        Crouch(isCrouching);
    }

    private void CreateGroundCheck()
    {
        Ray ray = new Ray(_groundCheck.position, Vector3.down);
        _isGrounded = Physics.SphereCast(ray, 0.3f, _groundDistance, _groundMask);
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
        Vector3 move;
        Vector3 moveDir = camForward * _moveInput.y + camRight * _moveInput.x;

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        if(!isSprinting)
        {
            move = moveDir.normalized * Speed + Vector3.up * _velocity.y;

            _controller.Move(move * Time.deltaTime);
            _playerAnimation.MovementAnimation(moveDir.magnitude * Speed);

        }
        else
        {
             move = moveDir.normalized * SprintSpeed + Vector3.up * _velocity.y;
            if(isSliding)
            {
                move = moveDir.normalized * (SprintSpeed + slidingSpeedIncrease) + Vector3.up * _velocity.y;
                Slide(isSliding);
            }
            _controller.Move(move * Time.deltaTime);
            _playerAnimation.MovementAnimation(moveDir.magnitude * SprintSpeed);

        }

    }

    private bool IsActuallyGrounded()
    {
        return _isGrounded && _velocity.y <= 0f;
    }

    private void Jump()
    {
        if (IsActuallyGrounded())
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * 2f * _gravity);
        }
    }

    private void Slide(bool isSliding)
    {
        _playerAnimation.SlidingAnimation(isSliding);
    }

    private void Crouch(bool isCrouching)
    {
        _playerAnimation.CrouchingAnimation(isCrouching);
    }

    private void UpdateVelocity()
    {
        _velocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
    }

}