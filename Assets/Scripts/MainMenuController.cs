using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // UI Elements
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public TextMeshProUGUI nameOfGameText;

    void Start()
    {
        // Make sure to assign the buttons in the Unity Inspector
        if (level1Button != null)
            level1Button.onClick.AddListener(() => LoadLevel("Level1"));

        if (level2Button != null)
            level2Button.onClick.AddListener(() => LoadLevel("Level2"));

        if (level3Button != null)
            level3Button.onClick.AddListener(() => LoadLevel("Level3"));
    }

    // Function to load a level
    void LoadLevel(string levelName)
    {
        // Load the scene by name
        Debug.Log("Loading " + levelName);
        SceneManager.LoadScene(levelName);  // Ensure the scenes are added in Build Settings
    }
}
