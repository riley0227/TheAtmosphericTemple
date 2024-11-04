using TMPro;
using UnityEngine;

public class CurrentTimeDisplay : MonoBehaviour
{
    public TMP_Text timeText;

    void Update()
    {
        timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2") + "s";
    }
}
