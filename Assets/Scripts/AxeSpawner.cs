using UnityEngine;

public class AxeSpawner : MonoBehaviour
{
    public GameObject[] axePrefabs; // Array of axe prefabs
    public float spawnInterval = 2f; // Time between spawns
    public float zPosition = -40f; // Z position for spawned axes

    private Camera mainCamera;
    private float minX;
    private float maxX;
    private float yPosition;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateSpawnBounds();

        // Start spawning
        InvokeRepeating(nameof(SpawnAxe), 1f, spawnInterval);
    }

    void CalculateSpawnBounds()
    {
        Vector3 screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, zPosition - mainCamera.transform.position.z));
        Vector3 screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, zPosition - mainCamera.transform.position.z));

        minX = screenLeft.x;
        maxX = screenRight.x;
        yPosition = screenLeft.y; // Top edge of the screen
    }

    void SpawnAxe()
    {
        int randomIndex = Random.Range(0, axePrefabs.Length);
        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX), // Random X position at top edge
            yPosition, // Y position at the top edge of the view
            zPosition
        );

        GameObject axe = Instantiate(axePrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Set gravity direction down for Axe spawned from AxeSpawner
        Axe axeScript = axe.GetComponent<Axe>();
        if (axeScript != null)
        {
            axeScript.gravityDirection = Vector3.down; // Move down
        }
    }
}
