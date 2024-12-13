// Fadumo Edited 

using UnityEngine;

public class KeyPickupHandler : MonoBehaviour
{
    public GameObject interactionPrompt; // UI to indicate the player can pick up the key
    public GameObject keyInventorySlot; // Object representing the key in the player's inventory

    private GameObject keyObject; // Reference to the key object
    private bool isWithinRange = false; // Tracks if the player is close enough to interact

    void Start()
    {
        // Disable UI and inventory slot display initially
        interactionPrompt.SetActive(false);
        keyInventorySlot.SetActive(false);

        // Assign the key object
        keyObject = this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isWithinRange = true;
            interactionPrompt.SetActive(true); // Show prompt when player is nearby
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isWithinRange = false;
            interactionPrompt.SetActive(false); // Hide prompt when player moves away
        }
    }

    void Update()
    {
        // Handle key pickup interaction
        if (isWithinRange && Input.GetButtonDown("Interact"))
        {
            interactionPrompt.SetActive(false); // Hide the interaction prompt
            keyInventorySlot.SetActive(true); // Display the key in the inventory
            keyObject.GetComponent<MeshRenderer>().enabled = false; // Hide the key object
        }
    }
}

