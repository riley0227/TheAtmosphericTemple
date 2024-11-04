using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource clickSoundSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void PlayClickSound()
    {
        if (clickSoundSource != null)
        {
            clickSoundSource.PlayOneShot(clickSoundSource.clip);
        }
        else
        {
            Debug.LogWarning("AudioManager: Click sound source is not assigned.");
        }
    }
}
