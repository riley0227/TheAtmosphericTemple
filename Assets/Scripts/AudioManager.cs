using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // instance of the AudioManager class
    public static AudioManager Instance { get; private set; }

    // Reference to an AudioSource component that will play when click
    [SerializeField] private AudioSource clickSoundSource;

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (Instance == null)
        {
            // If not, set this instance as the instance above
            Instance = this;
            // Prevent this object from being destroyed when loading a new scene
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // If an instance already exists, destroy this duplicate instance
            Destroy(gameObject); 
        }
    }

    //plays click
    public void PlayClickSound()
    {
        // Check if the AudioSource for the click sound is assigned
        if (clickSoundSource != null)
        {
            // Play the click sound once, without interrupting the main audio track
            clickSoundSource.PlayOneShot(clickSoundSource.clip);
        }
        else
        {
            // Log a warning if the AudioSource is not assigned
            Debug.LogWarning("AudioManager: Click sound source is not assigned.");
        }
    }
}
