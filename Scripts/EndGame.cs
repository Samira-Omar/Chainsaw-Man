// Fadumo Edited
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "Game"; // Name of the scene to load when starting the game.

    /// <summary>
    /// Loads the game scene to start the game.
    /// </summary>
    public void B_LoadScene()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("Game scene name is not set. Please set it in the inspector.");
        }
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void B_QuitGame()
    {
        Debug.Log("Quit button pressed. Application will exit.");
        Application.Quit();

#if UNITY_EDITOR
        // Ensures the quit function works during testing in the editor.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
