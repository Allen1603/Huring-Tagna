using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private PlayerInputAction inputActions;
    private Vector2 moveInput;
    private bool jumpRequested;
    private bool isGrounded;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerInputAction();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => jumpRequested = true;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
     

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        // Move the Rigidbody
        Vector3 velocity = move * moveSpeed;
        Vector3 currentVelocity = rb.velocity;
        rb.velocity = new Vector3(velocity.x, currentVelocity.y, velocity.z);

        // Rotate the character to face movement direction (only if there's movement)
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }

        // Set walking animation
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);
    }

    void HandleJump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpRequested = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
