using TMPro;
using UnityEngine;

public class CurrentTimeDisplay : MonoBehaviour
{
    // Reference to text component for displaying the time
    public TMP_Text timeText;

    // Called once per frame
    void Update()
    {
        // Set the text to display the time since the level was loaded
        timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2") + "s";
    }
}
