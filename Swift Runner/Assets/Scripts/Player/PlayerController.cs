using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 2f;

    [Header("Jump Settings")]
    [SerializeField] float jumpHeight = 2.5f;
    [SerializeField] float jumpDuration = 0.4f;
    [SerializeField] float groundCheckOffset = 1.5f;

    Vector2 movement;
    Rigidbody rb;
    PlayerInput playerInput;
    CapsuleCollider capsuleCollider;
    
    bool isJumping = false;
    float jumpTimer = 0f;
    float startYPosition = 0f;
    bool inputEnabled = true; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        
        var jumpAction = playerInput.actions["Jump"];
        jumpAction.performed += OnJumpPerformed;
    }

    void OnDestroy()
    {
        if (playerInput != null)
        {
            var jumpAction = playerInput.actions["Jump"];
            jumpAction.performed -= OnJumpPerformed;
        }
    }

    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return; 
        
        if (!isJumping && IsGrounded())
        {
            isJumping = true;
            jumpTimer = 0f;
            startYPosition = rb.position.y;
        }
    }

    void Update()
    {
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / jumpDuration;
            
            if (normalizedTime >= 1f)
            {
                isJumping = false;
                jumpTimer = 0f;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!inputEnabled) 
        {
            movement = Vector2.zero;
            return;
        }
        
        movement = context.ReadValue<Vector2>();
    }

    public void DisableInput()
    {
        inputEnabled = false;
        movement = Vector2.zero; 
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }

    bool IsGrounded()
    {
        float capsuleBottom = transform.position.y - (capsuleCollider.height / 2f) + capsuleCollider.center.y;
        Vector3 rayStart = new Vector3(transform.position.x, capsuleBottom + 0.1f, transform.position.z);
        float rayDistance = groundCheckOffset;
        
        return Physics.Raycast(rayStart, Vector3.down, rayDistance);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        if (isJumping)
        {
            float normalizedTime = jumpTimer / jumpDuration;
            float jumpProgress = Mathf.Sin(normalizedTime * Mathf.PI);
            newPosition.y = startYPosition + (jumpProgress * jumpHeight);
        }

        rb.MovePosition(newPosition);
    }

    void OnDrawGizmos()
    {
        if (capsuleCollider == null) return;
        
        float capsuleBottom = transform.position.y - (capsuleCollider.height / 2f) + capsuleCollider.center.y;
        Vector3 rayStart = new Vector3(transform.position.x, capsuleBottom + 0.1f, transform.position.z);
        Vector3 rayEnd = rayStart + Vector3.down * groundCheckOffset;
        
        bool grounded = Physics.Raycast(rayStart, Vector3.down, groundCheckOffset);
        
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawLine(rayStart, rayEnd);
    }
}
