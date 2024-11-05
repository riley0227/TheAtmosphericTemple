using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;          // Prefab of the bullet to instantiate
    public Transform firePoint;              // The position from which the bullet is fired
    public float bulletSpeed = 20f;          // Speed of the bullet
    public PistolVisibility pistolVisibility; // Reference to control pistol visibility effect
    public AudioSource shootSound;           // Sound played when shooting

    // Reference to LevelManager to check game state
    private LevelManager levelManager;

    // Initialize references
    void Start()
    {
        // Find LevelManager instance in the scene
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Check for player input on each frame
    void Update()
    {
        // Detect left mouse button click and ensure the game is not ended
        if (Input.GetMouseButtonDown(0) && levelManager != null && !levelManager.IsGameEnded)
        {
            Shoot(); // Call Shoot method if conditions are met
        }
    }

    // Method to handle shooting behavior
    void Shoot()
    {
        // Play shooting sound if assigned
        if (shootSound != null)
        {
            shootSound.Play();
        }

        // Make the pistol visible briefly
        pistolVisibility.ShowPistol(.5f);

        // Instantiate the bullet at the firePoint position with no rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Get the mouse position and convert it to a world position for aiming
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(firePoint.position).z; // Set depth for ScreenToWorldPoint
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate the direction from firePoint to the target position
        Vector3 direction = (targetPos - firePoint.position).normalized;
        
        // Apply velocity to the bullet to make it move towards the target
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;
    }
}
