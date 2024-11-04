using UnityEngine;

public class PistolVisibility : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false); // Pistol hidden at start
    }

    public void ShowPistol(float duration)
    {
        CancelInvoke(nameof(HidePistol)); // Cancel any previous HidePistol invokes
        gameObject.SetActive(true); // Show the pistol
        Invoke(nameof(HidePistol), duration); // Schedule to hide after duration
    }

    private void HidePistol()
    {
        gameObject.SetActive(false); // Hide the pistol
    }
}
