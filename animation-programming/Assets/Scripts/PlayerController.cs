using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerAnimation _playerAnimation;
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private Vector2 moveInput;
    private Vector3 _velocity;
    private Vector3 _previousPosition;

    public float Speed = 5f;
    public Vector3 Move;


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
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.canceled -= OnMove;
        _playerInput.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MovePlayer();
        UpdateVelocity();
    }

    private void MovePlayer()
    {
        Move = new Vector3(moveInput.x, 0f, moveInput.y);
        _controller.Move(Move * Speed * Time.deltaTime);
        _playerAnimation.WalkingAnimation(_velocity.magnitude);
    }

    private void UpdateVelocity()
    {
        _velocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
    }
}