using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject howToPlayPanel; // Reference to the "How to Play" panel

    public void ShowHowToPlayPanel()
    {
        Debug.Log("ShowHowToPlayPanel called"); // Debug message
        howToPlayPanel.SetActive(true); // Show the panel
    }

    public void HideHowToPlayPanel()
    {
        Debug.Log("HideHowToPlayPanel called"); // Debug message
        howToPlayPanel.SetActive(false); // Hide the panel
    }
}
