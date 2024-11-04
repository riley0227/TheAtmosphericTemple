using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    private static ButtonSoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public static void PlayButtonClickSound()
    {
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.Play();
        }
    }
}
