using UnityEngine;

public class LightningKill : MonoBehaviour
{
    public LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit lightning cube! Game Over.");
            levelManager.DisplayGameOverScreen();
        }
    }
}
