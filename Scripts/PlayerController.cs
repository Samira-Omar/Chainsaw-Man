// Edited by Samira
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public GameObject hud; 
    public GameObject inventory; 
    public GameObject deathScreen; 
    public GameObject player; 

    public float health = 100f; // Tracks player's health.

    void Start()
    {
        // Ensure the death screen is hidden when starting the game.
        deathScreen.SetActive(false);
    }

    void Update()
    {
        // Handle logic for player health clamping and death state.
        if (health <= 0) // Check if the player's health is zero or below.
        {
            HandlePlayerDeath();
        }

        // Clamp health to a maximum value of 100.
        if (health > 100)
        {
            health = 100;
        }
    }


    /// Handles what happens when the player dies.

    private void HandlePlayerDeath()
    {
        // Disable player movement upon death.
        player.GetComponent<FirstPersonController>().enabled = false;

        // Make the cursor visible and unlock it for UI interaction.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Hide the HUD and inventory and show the death screen.
        hud.SetActive(false);
        inventory.SetActive(false);
        deathScreen.SetActive(true);
    }
}
