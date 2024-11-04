using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public AudioSource jumpSound; // Reference to external jump sound AudioSource

    private Animator animator;
    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumpAndFall();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveX, 0, 0).normalized;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Set movement-related parameters
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetBool("IsMoving", moveX != 0);

        // Rotate character based on direction
        if (moveX > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);  // Facing right
        else if (moveX < 0)
            transform.rotation = Quaternion.Euler(0, 270, 0);  // Facing left
    }

    void HandleJumpAndFall()
    {
        if (characterController.isGrounded)
        {
            if (velocity.y < 0) velocity.y = -2f;  // Keep character grounded

            animator.SetBool("IsGrounded", true);
            animator.SetBool("IsFalling", false);

            // Check for jump input
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
                animator.SetBool("IsJumping", true);

                // Play jump sound on external AudioSource
                if (jumpSound != null)
                {
                    jumpSound.Play();
                }
            }
        }
        else
        {
            animator.SetBool("IsGrounded", false);

            // Determine if falling or still jumping
            if (velocity.y < 0)
            {
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsJumping", false);
            }
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
