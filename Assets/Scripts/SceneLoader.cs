using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this function when the button is clicked
    public void LoadScene(string sceneName)
    {
        // Load the specified scene immediately
        SceneManager.LoadScene(sceneName);
    }
}
