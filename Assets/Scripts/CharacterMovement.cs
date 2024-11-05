using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // Character's movement speed
    public float jumpHeight = 2f;    // Jump height
    public float gravity = 9.81f;    // Gravity strength
    public AudioSource jumpSound;    // Reference to an external AudioSource for the jump sound
    private Animator animator;                   // Animator component for controlling animations
    private CharacterController characterController; // CharacterController component for movement
    private Vector3 velocity;                    // Velocity vector for vertical movement

    void Start()
    {
        animator = GetComponent<Animator>();             // Get Animator component
        characterController = GetComponent<CharacterController>(); // Get CharacterController component
    }

    // Called once per frame
    void Update()
    {
        HandleMovement();        // Handle horizontal movement
        HandleJumpAndFall();     // Handle jumping and falling
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");     // Get horizontal input (A/D or Left/Right keys)
        Vector3 move = new Vector3(moveX, 0, 0).normalized; // Create movement vector on x-axis

        characterController.Move(move * moveSpeed * Time.deltaTime); // Move character

        // Update animator parameters based on movement
        animator.SetFloat("Speed", Mathf.Abs(moveX));          // Set speed parameter for running animation
        animator.SetBool("IsMoving", moveX != 0);              // Set movement flag if moving

        // Rotate character to face the direction of movement
        if (moveX > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);   // Facing right
        else if (moveX < 0)
            transform.rotation = Quaternion.Euler(0, 270, 0);  // Facing left
    }
    void HandleJumpAndFall()
    {
        // Check if character is on the ground
        if (characterController.isGrounded)
        {
            // Keep character grounded when not moving vertically
            if (velocity.y < 0) velocity.y = -2f;

            // Set grounded and falling animations
            animator.SetBool("IsGrounded", true);
            animator.SetBool("IsFalling", false);

            // Check for jump input
            if (Input.GetButtonDown("Jump"))
            {
                // Calculate jump velocity
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
                animator.SetBool("IsJumping", true);    // Set jumping animation

                // Play jump sound if an AudioSource is assigned
                if (jumpSound != null)
                {
                    jumpSound.Play();
                }
            }
        }
        else
        {
            animator.SetBool("IsGrounded", false);  // Not grounded

            // Determine if the character is falling or still in the jump
            if (velocity.y < 0)
            {
                animator.SetBool("IsFalling", true); // Set falling animation
                animator.SetBool("IsJumping", false); // Disable jumping animation
            }
        }

        // Apply gravity over time for realistic falling
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime); // Move character based on updated velocity
    }
}
