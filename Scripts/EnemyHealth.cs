// Fardowsa Edited

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy.
    private int currentHealth; // Current health of the enemy.
    public GameObject deathEffect; // Optional: Particle effect for death.
    public AudioClip deathSound; // Optional: Sound for death.
    private AudioSource audioSource;
    private Animator animator;
    private bool isDead = false; // To prevent multiple death triggers.

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health to the maximum value.
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Function to apply damage to the enemy.
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Prevent further damage if already dead.

        currentHealth -= damageAmount; // Reduce the health.

        // Optionally trigger a "hit" animation if it exists.
        animator?.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die(); // Handle death when health is depleted.
        }
    }

    // Function to handle the enemy's death.
    private void Die()
    {
        if (isDead) return; // Prevent multiple death triggers.
        isDead = true;

        // Play death animation if it exists.
        if (animator != null)
        {
            animator.SetBool("Death", true);
        }

        // Play death sound if assigned.
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Spawn death effect if assigned.
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Disable enemy's functionality (e.g., movement, attacks).
        DisableEnemy();

        // Destroy the enemy GameObject after a delay (e.g., after death animation).
        Destroy(gameObject, 2f);
    }

    // Function to disable the enemy's functionality upon death.
    private void DisableEnemy()
    {
        // Disable any movement scripts or nav mesh agents.
        var navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.enabled = false;
        }

        // Disable collider to prevent interactions.
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Stop other components like attacking or AI behaviors (if applicable).
    }
}
