using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public static int itemsCollected = 0;
    public static int totalItems = 3; // Adjust this to your item count
    public AudioSource soundPlayer; // Reference to external AudioSource

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemsCollected++;
            Debug.Log("Item Collected! Total: " + itemsCollected);

            // Play the sound from the external AudioSource
            if (soundPlayer != null)
            {
                soundPlayer.PlayOneShot(soundPlayer.clip);
            }

            gameObject.SetActive(false); // Hide the item immediately
        }
    }

    public static void ResetItemCount()
    {
        // resets items to 0
        itemsCollected = 0;
    }
}
