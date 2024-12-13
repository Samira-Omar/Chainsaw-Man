
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    private Canvas mainMenuCanvas;
    private Canvas optionsMenuCanvas;
    private Canvas extrasMenuCanvas;
    private Canvas loadingCanvas;

    public AudioSource buttonSound;

    void Start()
    {
        mainMenuCanvas = GameObject.Find("MainMenuCanvas")?.GetComponent<Canvas>();
        optionsMenuCanvas = GameObject.Find("OptionsCanvas")?.GetComponent<Canvas>();
        extrasMenuCanvas = GameObject.Find("ExtrasCanvas")?.GetComponent<Canvas>();
        loadingCanvas = GameObject.Find("LoadingCanvas")?.GetComponent<Canvas>();

        if (mainMenuCanvas == null || optionsMenuCanvas == null || extrasMenuCanvas == null || loadingCanvas == null)
        {
            Debug.LogError("Missing canvas references. Ensure all menus are correctly named.");
            return;
        }

        mainMenuCanvas.enabled = true;
        optionsMenuCanvas.enabled = false;
        extrasMenuCanvas.enabled = false;
        loadingCanvas.enabled = false;
    }

    public void OnStartGame()
    {
        if (buttonSound) buttonSound.Play();

        loadingCanvas.enabled = true;
        mainMenuCanvas.enabled = false;

        SceneManager.LoadScene("SampleScene");
    }

    public void ShowOptionsMenu()
    {
        if (buttonSound) buttonSound.Play();

        mainMenuCanvas.enabled = false;
        optionsMenuCanvas.enabled = true;
    }

    public void ShowExtrasMenu()
    {
        if (buttonSound) buttonSound.Play();

        mainMenuCanvas.enabled = false;
        extrasMenuCanvas.enabled = true;
    }

    public void QuitGame()
    {
        if (buttonSound) buttonSound.Play();

        Debug.Log("Game Exited");
        Application.Quit();
    }

    public void GoBackToMainMenu()
    {
        if (buttonSound) buttonSound.Play();

        mainMenuCanvas.enabled = true;
        optionsMenuCanvas.enabled = false;
        extrasMenuCanvas.enabled = false;
    }
}
