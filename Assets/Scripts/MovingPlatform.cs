using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the platform's movement
    public float height = 1.0f; // Maximum height for vertical movement
    public float width = 1.0f; // Maximum width for horizontal movement

    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
{
    startPosition = transform.position; // Store the starting position of the platform
    rb = GetComponent<Rigidbody>(); // Get Rigidbody component
}

void FixedUpdate()
{
    // Calculate the new positions
    float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
    float newX = startPosition.x + Mathf.Sin(Time.time * speed) * width;

    // Update the platform's position using MovePosition for smooth physics-based movement
    rb.MovePosition(new Vector3(newX, newY, startPosition.z));
}

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the platform, make the player a child of the platform
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player exits the platform, remove the player from being a child of the platform
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
