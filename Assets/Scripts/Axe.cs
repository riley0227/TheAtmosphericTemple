using UnityEngine;

public class Axe : MonoBehaviour
{
    public float fallSpeed = 50f; // Speed of movement
    public Vector3 gravityDirection = Vector3.down; // down movement

    private void Update()
    {
        // Apply movement in the specified gravity direction
        transform.Translate(gravityDirection * fallSpeed * Time.deltaTime);

        // Destroy the axe if it goes off-screen
        if (transform.position.y <= -100f || transform.position.x <= -100f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by axe! Game Over.");
            FindObjectOfType<LevelManager>().DisplayGameOverScreen(); // Trigger game over
            Destroy(gameObject); // Destroy axe on collision
        }
        else if (other.CompareTag("Platform"))
        {
            Destroy(gameObject); // Destroy axe on platform collision
        }
    }
}
