using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    private void OnEnable()
    {
        // Retrieve the Button component attached to this GameObject
        Button button = GetComponent<Button>();
        
        // Check if the Button component was found
        if (button != null)
        {
            // Remove any existing PlayClickSound listeners to avoid duplicate sounds
            button.onClick.RemoveListener(PlayClickSound);
            // Add PlayClickSound method as a listener to the button's onClick event
            button.onClick.AddListener(PlayClickSound);
        }
        else
        {
            Debug.LogWarning("missin button");
        }
    }

    // Method to play the click sound
    private void PlayClickSound()
    {
        // Check if the AudioManager singleton instance is available
        if (AudioManager.Instance != null)
        {
            // Use AudioManager to play the click sound
            AudioManager.Instance.PlayClickSound();
        }
        else
        {
            // Log a warning if the AudioManager instance is not found
            Debug.LogWarning("audio missin");
        }
    }
}
