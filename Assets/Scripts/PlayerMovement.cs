using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 9.81f;
    public float jumpHeight = 2f;
    public AudioSource jumpSound; // Reference to external jump sound AudioSource

    private CharacterController characterController;
    private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Handle movement
        float move = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 moveDirection = new Vector3(move, 0f, 0f);

        // Apply gravity and check if grounded
        if (characterController.isGrounded)
        {
            velocity.y = -2f; // Small value to keep grounded state

            // Jump if grounded and 'W' is pressed
            if (Input.GetKeyDown(KeyCode.W))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);

                // Play jump sound
                if (jumpSound != null)
                {
                    Debug.Log("Playing jump sound!");
                    jumpSound.PlayOneShot(jumpSound.clip);
                }
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime; // Apply gravity when not grounded
        }

        // Move the character
        characterController.Move((moveDirection + velocity) * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // Search for the EndGameTrigger component in the parent hierarchy
        EndGameTrigger endGameTrigger = hit.gameObject.GetComponentInParent<EndGameTrigger>();

        if (endGameTrigger != null)
        {
            endGameTrigger.TriggerEndGame(); // Call the public method
        }
    }
}
