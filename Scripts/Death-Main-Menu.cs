// Sofiya Edited
using UnityEngine;
using UnityEngine.SceneManagement;
// I wrote this part of the code to end the game
public class MenuController : MonoBehaviour
{
    // Method to load the game scene
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    // Method to exit the application
    public void ExitGame()
    {
        Application.Quit();
    }
}
