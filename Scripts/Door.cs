// Winta Edited

using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // UI element to indicate interaction possibility
    public GameObject warningMessage; // UI element for feedback when the door can't be opened
    public GameObject playerKey; // Simulates the inventory key
    public GameObject transitionEffect; // Visual effect for scene transition
    public string targetScene; // Name of the scene to load

    private bool isPlayerNearby = false; // Tracks whether the player is near the door

    void Start()
    {
        // Initially deactivate all UI elements and effects
        interactionPrompt.SetActive(false);
        warningMessage.SetActive(false);
        playerKey.SetActive(false);
        transitionEffect.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isPlayerNearby = true;
            interactionPrompt.SetActive(true); // Show interaction prompt when in range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isPlayerNearby = false;
            interactionPrompt.SetActive(false); // Hide interaction prompt when out of range
            warningMessage.SetActive(false); // Clear warning message
        }
    }

    void Update()
    {
        // Handle interaction logic
        if (isPlayerNearby && Input.GetButtonDown("Interact"))
        {
            if (!playerKey.activeInHierarchy)
            {
                // If the key is not collected, show a warning
                warningMessage.SetActive(true);
            }
            else
            {
                // If the key is present, trigger transition and load the next scene
                interactionPrompt.SetActive(false);
                warningMessage.SetActive(false);
                transitionEffect.SetActive(true);
                StartCoroutine(LoadTargetScene());
            }
        }
    }

    IEnumerator LoadTargetScene()
    {
        // Add a slight delay before transitioning to the next scene
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(targetScene);
    }
}

