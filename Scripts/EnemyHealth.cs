// Fardowsa Edited
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy.
    private int currentHealth; // Current health value of the enemy.
    public GameObject deathEffect; // Optional: Particle effect when the enemy dies.
    public AudioClip deathSound; // Optional: Death sound effect.
    private AudioSource audioSource; // For playing death sounds.
    private Animator animator; // For death animations.
    private bool isDead = false; // Track if the enemy is already dead to prevent repeated death logic.

    private void Start()
    {
        currentHealth = maxHealth; // Set initial health to maximum at the start.
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Applies damage to the enemy and triggers death if health reaches 0.
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Ignore damage if already dead.

        currentHealth -= damageAmount; // Reduce health by the damage amount.

        animator?.SetTrigger("Hit"); // Trigger a hit animation if available.

        if (currentHealth <= 0) Die(); // If health drops to or below 0, handle death.
    }

    /// <summary>
    /// Handles enemy death effects, animations, and cleanup.
    /// </summary>
    private void Die()
    {
        if (isDead) return;
        isDead = true;

        animator?.SetBool("Death", true);
        if (deathSound) audioSource.PlayOneShot(deathSound);
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);

        DisableEnemy();
        Destroy(gameObject, 2f);
    }

    /// <summary>
    /// Disables the enemy's functionality upon death.
    /// </summary>
    private void DisableEnemy()
    {
        var navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent) navAgent.enabled = false;

        var collider = GetComponent<Collider>();
        if (collider) collider.enabled = false;
    }
}

