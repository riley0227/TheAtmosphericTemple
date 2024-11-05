using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destructionDelay = 0.05f; // Very small delay before destruction
    public float speed = 40f; // Speed of the bullet
    private float fixedZPosition = -40f; // Fixed Z position for the bullet

    private void Start()
    {

        // Set bullet rotation to face the direction of the mouse click
        RotateTowardsMouse();
    }

    private void Update()
    {
        // Move the bullet forward based on its speed, but lock it to z = -40
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Lock Z position
        Vector3 lockedPosition = transform.position;
        lockedPosition.z = fixedZPosition;
        transform.position = lockedPosition;
    }

    private void RotateTowardsMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Keep same z depth
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate direction from bullet to target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Calculate the rotation to make the bullet face the direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Lock the Y-axis rotation to 90 or -90 and apply the rotation
        rotation = Quaternion.Euler(rotation.eulerAngles.x, 90f * Mathf.Sign(direction.x), rotation.eulerAngles.z);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe")) // If it hits an axe
        {

            // Delay the destruction of both bullet and axe to ensure sound plays
            Destroy(other.gameObject, destructionDelay); 
            Destroy(gameObject, destructionDelay); 
        }
        else if (other.CompareTag("Platform")) // If it hits a platform
        {
            Destroy(gameObject); // Only destroy the bullet
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy bullet when off-screen
    }
}
