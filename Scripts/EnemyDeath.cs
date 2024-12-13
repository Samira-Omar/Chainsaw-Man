// Samira Edited
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public GameObject player; 
    private float damageAmount; 
    public float fixedDamage = 25f; 
    public float minRandomDamage; 
    public float maxRandomDamage; 

    public bool useRandomDamage; 
    public bool useFixedDamage; 

    public AudioClip[] damageSounds; 
    private AudioSource audioSource; 

    void Start()
    {
        // randomly chosen initial damage value if using random damage.
        damageAmount = Random.Range(minRandomDamage, maxRandomDamage);
        audioSource = player.GetComponent<AudioSource>(); // Reference the player's audio source.
    }

    /// <summary>
    /// Triggered when an object enters the enemy's collider.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Only process damage if the player is hit.
        {
            if (useRandomDamage)
            {
                player.GetComponent<PlayerHealth>().health -= damageAmount; // Apply random damage to player.
                PlayRandomDamageSound();
            }

            if (useFixedDamage)
            {
                player.GetComponent<PlayerHealth>().health -= fixedDamage; // Apply fixed damage to player.
                PlayRandomDamageSound();
            }
        }
    }

    /// <summary>
    /// Plays a random damage sound when the player takes damage.
    /// </summary>
    private void PlayRandomDamageSound()
    {
        if (damageSounds.Length > 0) // Ensure there are sounds to play.
        {
            audioSource.clip = damageSounds[Random.Range(0, damageSounds.Length)];
            audioSource.Play();
        }
    }
}


