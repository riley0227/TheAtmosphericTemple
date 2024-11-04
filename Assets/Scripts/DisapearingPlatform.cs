using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearDelay = 1.0f; // Time before the platform disappears after being hit
    public float reappearDelay = 5.0f;  // Time before the platform reappears after disappearing

    private Renderer platformRenderer;
    
    public Collider mainCollider; // The main platform collider (not a trigger)
    public Collider triggerCollider; // The trigger collider for detection

    void Start()
    {
        // Get the platform's Renderer component
        platformRenderer = GetComponent<Renderer>();

        if (platformRenderer == null || mainCollider == null || triggerCollider == null)
        {
            Debug.LogError("Missing Renderer or Collider component on the platform!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the platform
        if (other.CompareTag("Player"))
        {
            // Start the disappearing process
            StartCoroutine(DisappearAndReappear());
        }
    }

    private IEnumerator DisappearAndReappear()
{
    yield return new WaitForSeconds(disappearDelay);

    platformRenderer.enabled = false;
    mainCollider.enabled = false;
    triggerCollider.enabled = false;
    Debug.Log("Platform disappeared");

    yield return new WaitForSeconds(reappearDelay);

    platformRenderer.enabled = true;
    mainCollider.enabled = true;
    triggerCollider.enabled = true;
    Debug.Log("Platform reappeared");
}

}
