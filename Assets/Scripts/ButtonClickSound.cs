using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    private void OnEnable()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveListener(PlayClickSound);
            button.onClick.AddListener(PlayClickSound);
        }
        else
        {
            Debug.LogWarning("ButtonClickSound: Missing Button component.");
        }
    }

    private void PlayClickSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound(); // Play click sound from AudioManager
        }
        else
        {
            Debug.LogWarning("ButtonClickSound: AudioManager instance is missing.");
        }
    }
}
