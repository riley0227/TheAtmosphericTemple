using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        // Ensure AudioSource component is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only play the sound if colliding with an object tagged "Item"
        if (collision.gameObject.CompareTag("Item"))
        {
            audioSource.PlayOneShot(audioSource.clip); // Play sound once on collision
        }
    }
}
