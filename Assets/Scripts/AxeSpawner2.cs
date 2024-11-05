using UnityEngine;

public class AxeSpawner2 : MonoBehaviour
{
    public GameObject[] axePrefabs; // Array of axe prefabs
    public float spawnInterval = 2f; // Time between spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnAxe), 1f, spawnInterval);
    }

    void SpawnAxe()
    {
        // gets random axe
        int randomIndex = Random.Range(0, axePrefabs.Length);
        GameObject axe = Instantiate(axePrefabs[randomIndex], transform.position, Quaternion.identity);

        // Set gravity direction left for Axe spawned from AxeSpawner2
        Axe axeScript = axe.GetComponent<Axe>();
    }
}
