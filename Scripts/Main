// Edited By Samira
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    private Canvas mainMenuCanvas; // Reference to the main menu UI canvas.
    private Canvas optionsMenuCanvas; // Reference to the options menu UI canvas.
    private Canvas extrasMenuCanvas; // Reference to the extras menu UI canvas.
    private Canvas loadingCanvas; // Reference to the loading screen UI canvas.

    public AudioSource buttonSound; // Audio to play on button interactions.

    void Start()
    {
        // Find the relevant UI canvases by their names.
        mainMenuCanvas = GameObject.Find("MainMenuCanvas")?.GetComponent<Canvas>();
        optionsMenuCanvas = GameObject.Find("OptionsCanvas")?.GetComponent<Canvas>();
        extrasMenuCanvas = GameObject.Find("ExtrasCanvas")?.GetComponent<Canvas>();
        loadingCanvas = GameObject.Find("LoadingCanvas")?.GetComponent<Canvas>();

        // Log error if any canvas references are missing.
        if (mainMenuCanvas == null || optionsMenuCanvas == null || extrasMenuCanvas == null || loadingCanvas == null)
        {
            Debug.LogError("Missing canvas references. Ensure all menus are correctly named.");
            return;
        }

        // Set the initial state of menus at the start.
        mainMenuCanvas.enabled = true;
        optionsMenuCanvas.enabled = false;
        extrasMenuCanvas.enabled = false;
        loadingCanvas.enabled = false;
    }

    /// <summary>
    /// Handles starting the game by loading the main game scene.
    /// </summary>
    public void OnStartGame()
    {
        if (buttonSound) buttonSound.Play(); // Play a button sound.

        loadingCanvas.enabled = true; // Show the loading screen.
        mainMenuCanvas.enabled = false; // Hide the main menu.

        SceneManager.LoadScene("SampleScene"); // Load the game scene.
    }

    /// <summary>
    /// Displays the options menu and hides the main menu.
    /// </summary>
    public void ShowOptionsMenu()
    {
        if (buttonSound) buttonSound.Play(); // Play a button sound.

        mainMenuCanvas.enabled = false;
        optionsMenuCanvas.enabled = true;
    }

    /// <summary>
    /// Displays the extras menu and hides the main menu.
    /// </summary>
    public void ShowExtrasMenu()
    {
        if (buttonSound) buttonSound.Play(); // Play a button sound.

        mainMenuCanvas.enabled = false;
        extrasMenuCanvas.enabled = true;
    }

    /// <summary>
    /// Handles quitting the game when the user clicks the quit button.
    /// </summary>
    public void QuitGame()
    {
        if (buttonSound) buttonSound.Play(); // Play a button sound.

        Debug.Log("Game Exited");
        Application.Quit(); // Quit the application.
    }

    /// <summary>
    /// Returns the player back to the main menu from the options or extras menu.
    /// </summary>
    public void GoBackToMainMenu()
    {
        if (buttonSound) buttonSound.Play(); // Play a button sound.

        mainMenuCanvas.enabled = true;
        optionsMenuCanvas.enabled = false;
        extrasMenuCanvas.enabled = false;
    }
}
