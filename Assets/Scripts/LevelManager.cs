using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelName; // Name of the current level (e.g., "Level1", "Level2", "Level3")
    public GameObject endGameScreen;
    public TMP_Text highScoreText;
    public TMP_Text completionTimeText;
    public GameObject timeDisplay;
    public AudioSource gameOverSound;
    public AudioSource winSound;
    public AudioSource buttonClickSound;
    public AudioSource shootingSound;

    private float elapsedTime;
    private float highScore;
    private bool isGameEnded;

    public bool IsGameEnded => isGameEnded; // Public read-only property

    private void Start()
    {
        endGameScreen.SetActive(false);
        elapsedTime = 0f;

        // Load the high score for the current level
        highScore = PlayerPrefs.GetFloat(levelName + "_HighScore", float.MaxValue);
        isGameEnded = false;

        UpdateHighScoreText();
    }

    private void Update()
    {
        if (!isGameEnded)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void DisplayEndScreen()
    {
        MuteShootingSound();
        isGameEnded = true;
        Debug.Log("End Game Screen Activated!");

        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }

        if (gameOverSound != null)
        {
            gameOverSound.PlayScheduled(AudioSettings.dspTime);
            gameOverSound.SetScheduledEndTime(AudioSettings.dspTime + 3.0);
        }

        endGameScreen.SetActive(true);
        completionTimeText.text = "Completion Time: " + elapsedTime.ToString("F2") + "s";

        // Check if this is a new high score for the current level
        if (elapsedTime < highScore)
        {
            highScore = elapsedTime;
            PlayerPrefs.SetFloat(levelName + "_HighScore", highScore); // Save with level-specific key
            PlayerPrefs.Save();

            highScoreText.text = "New High Score: " + highScore.ToString("F2") + "s";
        }
        else
        {
            highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
        }

        Time.timeScale = 0f;
    }

    public void DisplayWinScreen()
    {
        MuteShootingSound();
        isGameEnded = true;
        Debug.Log("DisplayWinScreen() called");

        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }

        if (gameOverSound.isPlaying)
        {
            gameOverSound.Stop();
        }

        if (winSound != null)
        {
            Debug.Log("Playing win sound from 13s to 22s!");
            winSound.time = 13f;
            winSound.PlayScheduled(AudioSettings.dspTime);
            winSound.SetScheduledEndTime(AudioSettings.dspTime + 9f);
        }

        endGameScreen.SetActive(true);
        highScoreText.text = "You Won!";
        completionTimeText.text = "Completion Time: " + elapsedTime.ToString("F2") + "s";

        if (elapsedTime < highScore)
        {
            highScore = elapsedTime;
            PlayerPrefs.SetFloat(levelName + "_HighScore", highScore); // Save with level-specific key
            PlayerPrefs.Save();
        }
        
        highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
        Time.timeScale = 0f;
    }

    public void DisplayGameOverScreen()
    {
        MuteShootingSound();
        isGameEnded = true;

        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }

        if (winSound.isPlaying)
        {
            winSound.Stop();
        }

        if (gameOverSound != null)
        {
            Debug.Log("Playing game over sound!");
            gameOverSound.PlayScheduled(AudioSettings.dspTime);
            gameOverSound.SetScheduledEndTime(AudioSettings.dspTime + 3.0);
        }

        endGameScreen.SetActive(true);
        highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
        completionTimeText.text = "Game Over";
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        ButtonSoundManager.PlayButtonClickSound();
        isGameEnded = false;

        UnmuteShootingSound();

        Time.timeScale = 1f;
        ItemCollector.ResetItemCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHomeScreen()
    {
        ButtonSoundManager.PlayButtonClickSound();
        isGameEnded = false;

        UnmuteShootingSound();

        Time.timeScale = 1f;
        ItemCollector.ResetItemCount();
        SceneManager.LoadScene("LandingScreen");
    }

    public void ResetHighScore()
    {
        ButtonSoundManager.PlayButtonClickSound();

        PlayerPrefs.SetFloat(levelName + "_HighScore", float.MaxValue);
        PlayerPrefs.Save();
        highScore = float.MaxValue;
        UpdateHighScoreText();
        Debug.Log("High Score has been reset.");
    }

    private void MuteShootingSound()
    {
        if (shootingSound != null)
        {
            shootingSound.mute = true;
        }
    }

    private void UnmuteShootingSound()
    {
        if (shootingSound != null)
        {
            shootingSound.mute = false;
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScore == float.MaxValue || highScore <= 0f)
        {
            highScore = 10000f;
        }

        highScoreText.text = "High Score: " + highScore.ToString("F2") + "s";
    }
}
