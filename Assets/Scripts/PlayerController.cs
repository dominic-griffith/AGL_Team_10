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
    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private InputActionMap actionMap;
    private Vector2 moveInput;
    private bool jump = false;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    private bool _isInstanceNullPlatformManager;

    private void Start()
    {
        _isInstanceNullPlatformManager = PlatformManager.Instance == null;
    }

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
        //Movement Input
        moveInput = actionMap.FindAction("Move").ReadValue<Vector2>();
        if (moveInput.y > 0)
            jump = true;
        else
            jump = false;

        if (_isInstanceNullPlatformManager) return; //in case we using TilePlatformManager
        //Togggle Platforms Input
        if(actionMap.FindAction("PlatformToggle").IsPressed())
        {
            if(playerColor.Equals(PlayerColor.Blue))
            {
                PlatformManager.Instance.BluePressedToggle();
            }
            else
            {
                PlatformManager.Instance.RedPressedToggle();
            }
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(floor.position, groundCheckRadius, groundLayer);
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        
        if(moveInput.x>0){
            if(isGrounded)
                spriteAnimator.enabled=true;
            else
                spriteAnimator.enabled=false;
            spriteRenderer.flipX=false;
        }
        else if(moveInput.x<0){
            if(isGrounded)
                spriteAnimator.enabled=true;
            else
                spriteAnimator.enabled=false;
            spriteRenderer.flipX=true;
        }
        else{
            spriteAnimator.enabled=false;
        }
        if (jump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
            
    }
    
    //function that enables movement when called for the according color/player
    public void EnablePlayerMovement()
    {
        if (playerColor.Equals(PlayerColor.Blue))
        {
            playerInputActions.BluePlayer.Enable();
        }
        else
        {
            playerInputActions.RedPlayer.Enable();
        }
    }
    
    //function that disables movement when called for the according color/player
    public void DisablePlayerMovement()
    {
        if (playerColor.Equals(PlayerColor.Blue))
        {
            playerInputActions.BluePlayer.Disable();
        }
        else
        {
            playerInputActions.RedPlayer.Disable();
        }
    }
}

public enum PlayerColor
{
    Blue,
    Red
}
