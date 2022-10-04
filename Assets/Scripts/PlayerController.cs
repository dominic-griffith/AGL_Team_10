using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerColor playerColor;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpVelocity = 15f;
    [SerializeField] private Transform floor;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private InputActionMap actionMap;
    private Vector2 moveInput;
    private bool jump = false;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerInputActions(); //context for input can be either (1) Started, (2) Performed, (3) Canceled

        if (playerColor.Equals(PlayerColor.Blue))
        {
            playerInputActions.BluePlayer.Enable();
            actionMap = playerInputActions.BluePlayer.Get();
        }
        else
        {
            playerInputActions.RedPlayer.Enable();
            actionMap = playerInputActions.RedPlayer.Get();
        }

    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        MovePlayer();
    }

    private void GetInput()
    {
        moveInput = actionMap.FindAction("Move").ReadValue<Vector2>();
        if (moveInput.y > 0)
            jump = true;
        else
            jump = false;
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(floor.position, groundCheckRadius, groundLayer);
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        if (jump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
            
    }
}

public enum PlayerColor
{
    Blue,
    Red
}
