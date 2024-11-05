using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    // instance of ButtonSoundManager
    private static ButtonSoundManager instance;
    
    // Reference to an AudioSource component
    private AudioSource audioSource;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // Set this instance as the singleton instance
            instance = this;
            // Prevent this object from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
            // Get the AudioSource component attached to this GameObject
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // If an instance already exists, destroy this duplicate instance
            Destroy(gameObject);
        }
    }

    // Static method to play the button click sound
    public static void PlayButtonClickSound()
    {
        // Check if the instance and audioSource are valid
        if (instance != null && instance.audioSource != null)
        {
            // Play the button click sound
            instance.audioSource.Play();
        }
    }
}
