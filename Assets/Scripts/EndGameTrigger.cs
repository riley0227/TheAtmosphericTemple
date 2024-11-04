using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public LevelManager levelManager; // Reference to the LevelManager script

    public void TriggerEndGame()
    {
        Debug.Log("Player reached the temple!");

        // Check if player has collected all items (win condition)
        if (ItemCollector.itemsCollected == ItemCollector.totalItems)
        {
            levelManager.DisplayWinScreen(); // Call win screen
        }
        else
        {
            levelManager.DisplayGameOverScreen(); // Call lose screen if items are missing
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEndGame(); // Trigger end game logic when player reaches the temple
        }
    }
}
