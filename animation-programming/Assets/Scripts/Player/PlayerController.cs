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
    [SerializeField] private float _slideFriction = 5f;
    [SerializeField] float _slideSpeedBoost = 1.3f;

    [SerializeField] float _standHeight = 1.7f;
    [SerializeField] float _slideHeight = 1.0f;
    [SerializeField] float _heightChangeSpeed = 10f;


    [SerializeField] LayerMask _ceilingMask;

    private float _targetHeight;
    private Vector3 _targetCenter;


    private PlayerInputControls _playerInput;
    
    private bool isSprinting = false;
    private static float _gravity = 9.81f;
    private PlayerAnimation _playerAnimation;
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector3 _currentVelocity;
    private Vector3 _previousPosition;
    private float rotationVelocity;
    private PlayerManager _playerManager;
    private bool isSliding = false;
    private bool isCrouching = false;
    private Vector3 _slideVelocity;
    private bool _wasSliding;
    

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
        EnableInputs();

    }

    private void OnDisable()
    {
        DisableInputs();
    }

    private void Update()
    {
        CreateGroundCheck();
        MovePlayer();
        ApplyGravity();
        HandleCollisionHeight();
        _previousPosition = transform.position;

    }

    private void ApplyGravity()
    {
        if (_isGrounded && _currentVelocity.y < 0)
            _currentVelocity.y = -2f;

        _currentVelocity.y -= _gravity * Time.deltaTime;
    }


    private void CreateGroundCheck()
    {
        Ray ray = new Ray(_groundCheck.position, Vector3.down);
        _isGrounded = Physics.SphereCast(ray, 0.3f, _groundDistance, _groundMask);
    }

    private void MovePlayer()
    {
        // --- Camera-relative movement ---
        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * _moveInput.y + camRight * _moveInput.x;

        // --- Rotation ---
        if (moveDir.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        Vector3 horizontalVelocity;

        // --- Movement logic ---
        if (!isSprinting)
        {
            horizontalVelocity = moveDir.normalized * Speed;
            _slideVelocity = Vector3.zero;
        }
        else if (isSliding)
        {
            if (!_wasSliding)
            {
                horizontalVelocity = moveDir.normalized * SprintSpeed * _slideSpeedBoost;
                _slideVelocity = horizontalVelocity;
            }

            _slideVelocity = Vector3.Lerp(_slideVelocity,  Vector3.zero, _slideFriction * Time.deltaTime);

            horizontalVelocity = _slideVelocity;
            Slide(true);
        }
        else
        {
            horizontalVelocity = moveDir.normalized * SprintSpeed;
            _slideVelocity = Vector3.zero;
        }

        _wasSliding = isSliding;

        Vector3 move = horizontalVelocity + Vector3.up * _currentVelocity.y;

        _controller.Move(move * Time.deltaTime);

        _playerAnimation.MovementAnimation(horizontalVelocity.magnitude);
    }

    private void HandleCollisionHeight()
    {
        float desiredHeight = isSliding ? _slideHeight : _standHeight;

        if (!isSliding && !CanStandUp())
        {
            desiredHeight = _slideHeight;
        }
        _targetHeight = desiredHeight;
        _targetCenter = new Vector3(0f, desiredHeight * 0.5f, 0f);
        _controller.height = Mathf.Lerp(_controller.height, _targetHeight, _heightChangeSpeed * Time.deltaTime);
        _controller.center = Vector3.Lerp(_controller.center, _targetCenter, _heightChangeSpeed * Time.deltaTime);
    }

    private bool CanStandUp()
    {
        float castDistance = _standHeight - _controller.height + 0.05f;

        Vector3 origin = transform.position + Vector3.up * _controller.height * 0.5f;

        return !Physics.SphereCast(origin, _controller.radius, Vector3.up, out _, castDistance, _ceilingMask);
    }

    private bool IsActuallyGrounded()
    {
        return _isGrounded && _currentVelocity.y <= 0f;
    }

    private void Jump()
    {
        if (IsActuallyGrounded())
        {
            _currentVelocity.y = Mathf.Sqrt(jumpHeight * 2f * _gravity);
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
        _currentVelocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
    }

    private void EnableInputs()
    {
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

    private void DisableInputs()
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

}