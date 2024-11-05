using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelName;                       // Name of the current level (e.g., "Level1")
    public GameObject endGameScreen;               // UI panel shown at the end of the game
    public TMP_Text highScoreText;                 // Text element for displaying the high score
    public TMP_Text completionTimeText;            // Text element for displaying completion time
    public GameObject timeDisplay;                 // UI element showing the time
    public AudioSource gameOverSound;              // Sound played on game over
    public AudioSource winSound;                   // Sound played when the player wins
    public AudioSource buttonClickSound;           // Sound for button clicks
    public AudioSource shootingSound;              // Sound for shooting action
    private float elapsedTime;  // elapsed time
    private float highScore;  // high score
    private bool isGameEnded; // checks if game is over

    // Public read-only property to check if the game has ended
    public bool IsGameEnded => isGameEnded;

    private void Start()
    {
        endGameScreen.SetActive(false);             // Hide the end game screen initially
        elapsedTime = 0f;

        // Load the high score for the current level from PlayerPrefs
        highScore = PlayerPrefs.GetFloat(levelName + "_HighScore", float.MaxValue);
        isGameEnded = false;

        UpdateHighScoreText();                      // Update the high score text in the UI
    }

    private void Update()
    {
        // Update the elapsed time if the game hasn't ended
        if (!isGameEnded)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    // Method to display the end game screen with time and high score
    public void DisplayEndScreen()
    {
        MuteShootingSound();                         // Mute shooting sound
        isGameEnded = true;
        Debug.Log("End Game Screen Activated!");

        // Hide the time display UI if it exists
        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }

        // Play the game over sound with a scheduled end time
        if (gameOverSound != null)
        {
            gameOverSound.PlayScheduled(AudioSettings.dspTime);
            gameOverSound.SetScheduledEndTime(AudioSettings.dspTime + 3.0);
        }

        // Show end game screen and display completion time
        endGameScreen.SetActive(true);
        completionTimeText.text = "Completion Time: " + elapsedTime.ToString("F2") + "s";

        // Check if the player achieved a new high score
        if (elapsedTime < highScore)
        {
            highScore = elapsedTime;
            PlayerPrefs.SetFloat(levelName + "_HighScore", highScore); // Save new high score
            PlayerPrefs.Save();

            highScoreText.text = "New High Score: " + highScore.ToString("F2") + "s";
        }
        else
        {
            highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
        }

        Time.timeScale = 0f;                         // Pause the game
    }

    // Method to display the win screen with completion time and high score
    public void DisplayWinScreen()
    {
        MuteShootingSound();
        isGameEnded = true;
        Debug.Log("DisplayWinScreen() called");

        // Hide the time display UI
        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }

        // Stop the game over sound if it's playing
        if (gameOverSound.isPlaying)
        {
            gameOverSound.Stop();
        }

        // Play the win sound with a specified start and end time
        if (winSound != null)
        {
            Debug.Log("Playing win sound from 13s to 22s!");
            winSound.time = 13f;
            winSound.PlayScheduled(AudioSettings.dspTime);
            winSound.SetScheduledEndTime(AudioSettings.dspTime + 9f);
        }

        // Show end game screen with a win message and completion time
        endGameScreen.SetActive(true);
        highScoreText.text = "You Won!";
        completionTimeText.text = "Completion Time: " + elapsedTime.ToString("F2") + "s";

        // Check and save new high score if applicable
        if (elapsedTime < highScore)
        {
            highScore = elapsedTime;
            PlayerPrefs.SetFloat(levelName + "_HighScore", highScore); // Save new high score
            PlayerPrefs.Save();
        }
        
        highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
        Time.timeScale = 0f;                         // Pause the game
    }

    // Method to display the game over screen
    public void DisplayGameOverScreen()
    {
        MuteShootingSound();
        isGameEnded = true;

        // Hide the time display UI
        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }

        // Stop the win sound if it's playing
        if (winSound.isPlaying)
        {
            winSound.Stop();
        }

        // Play the game over sound
        if (gameOverSound != null)
        {
            Debug.Log("Playing game over sound!");
            gameOverSound.PlayScheduled(AudioSettings.dspTime);
            gameOverSound.SetScheduledEndTime(AudioSettings.dspTime + 3.0);
        }

        // Show end game screen with game over message and high score
        endGameScreen.SetActive(true);
        highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
        completionTimeText.text = "Game Over";
        Time.timeScale = 0f;                         // Pause the game
    }

    // Method to restart the current level
    public void RestartLevel()
    {
        ButtonSoundManager.PlayButtonClickSound();
        isGameEnded = false;

        UnmuteShootingSound();

        Time.timeScale = 1f;                         // Resume the game
        ItemCollector.ResetItemCount();              // Reset any collected items if applicable
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Method to go back to the home screen
    public void GoToHomeScreen()
    {
        ButtonSoundManager.PlayButtonClickSound();
        isGameEnded = false;

        UnmuteShootingSound();

        Time.timeScale = 1f;                         // Resume the game
        ItemCollector.ResetItemCount();
        SceneManager.LoadScene("LandingScreen");     // Load the home screen scene
    }

    // Method to reset the high score
    public void ResetHighScore()
    {
        ButtonSoundManager.PlayButtonClickSound();

        PlayerPrefs.SetFloat(levelName + "_HighScore", float.MaxValue); // Reset saved high score
        PlayerPrefs.Save();
        highScore = float.MaxValue;
        UpdateHighScoreText();
        Debug.Log("High Score has been reset.");
    }

    // Helper method to mute shooting sound
    private void MuteShootingSound()
    {
        if (shootingSound != null)
        {
            shootingSound.mute = true;
        }
    }

    // Helper method to unmute shooting sound
    private void UnmuteShootingSound()
    {
        if (shootingSound != null)
        {
            shootingSound.mute = false;
        }
    }

    // Helper method to update the high score text display
    private void UpdateHighScoreText()
    {
        // Set a default value if the high score is uninitialized
        if (highScore == float.MaxValue || highScore <= 0f)
        {
            highScore = 10000f;
        }

        highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
    }
}
