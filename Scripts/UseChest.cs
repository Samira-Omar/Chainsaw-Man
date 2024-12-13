// Fadumo Edited

using UnityEngine;

public class UseChest : MonoBehaviour
{
    [Header("Chest Interaction Settings")]
    public GameObject handUI; // UI prompt to display when in reach.
    public GameObject objToActivate; // Object to activate when the chest is opened.

    private bool isPlayerInReach = false; // Tracks if the player is within interaction range.
    private Animator chestAnimator; // Reference to the chest's Animator component.
    private BoxCollider chestCollider; // Reference to the chest's BoxCollider component.

    private void Start()
    {
        // Initialize components and set initial states.
        handUI.SetActive(false); // Ensure the hand UI is hidden initially.
        objToActivate.SetActive(false); // Ensure the object to activate is disabled initially.

        chestAnimator = GetComponent<Animator>();
        chestCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the player enters the chest's trigger zone.
        if (other.CompareTag("Reach"))
        {
            isPlayerInReach = true;
            handUI.SetActive(true); // Display the hand UI prompt.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detect if the player exits the chest's trigger zone.
        if (other.CompareTag("Reach"))
        {
            isPlayerInReach = false;
            handUI.SetActive(false); // Hide the hand UI prompt.
        }
    }

    private void Update()
    {
        // Check if the player is in reach and presses the "Interact" button.
        if (isPlayerInReach && Input.GetButtonDown("Interact"))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        // Hide the hand UI and activate the associated object.
        handUI.SetActive(false);
        objToActivate.SetActive(true);

        // Trigger the chest's open animation and disable its collider.
        if (chestAnimator != null)
        {
            chestAnimator.SetBool("open", true);
        }

        if (chestCollider != null)
        {
            chestCollider.enabled = false;
        }
    }
}
