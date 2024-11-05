using UnityEngine;

public class LightningKill : MonoBehaviour
{
    // Reference to the LevelManager script to handle game-over scenarios
    public LevelManager levelManager;

    // Trigger event that occurs when another collider enters this object's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has player tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("game over madam ;)");
            // Call the DisplayGameOverScreen method in LevelManager to show the game-over screen
            levelManager.DisplayGameOverScreen();
        }
    }
}
