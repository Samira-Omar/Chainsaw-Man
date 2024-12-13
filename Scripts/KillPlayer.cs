// Samira Edited
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    public string nextSceneName; 
    public float delay = 0.5f; 

    [Header("UI Settings")]
    public GameObject fadeout; // Fadeout UI element.

    private bool isPlayerInsideTrigger = false; // Track if the player is inside the trigger area.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true; // Mark that the player is in the trigger zone.
            if (fadeout != null)
            {
                fadeout.SetActive(true); // Activate the fadeout UI if assigned.
            }
            Invoke(nameof(LoadNextScene), delay); // Schedule the scene load after the delay.
        }
    }

    private void LoadNextScene()
    {
        if (isPlayerInsideTrigger)
        {
            SceneManager.LoadScene(nextSceneName); // Load the specified scene.
        }
    }
}


