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
        damageAmount = Random.Range(minRandomDamage, maxRandomDamage);
        audioSource = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useRandomDamage)
            {
                player.GetComponent<PlayerHealth>().health -= damageAmount;
                PlayRandomDamageSound();
            }

            if (useFixedDamage)
            {
                player.GetComponent<PlayerHealth>().health -= fixedDamage;
                PlayRandomDamageSound();
            }
        }
    }

    private void PlayRandomDamageSound()
    {
        if (damageSounds.Length > 0)
        {
            audioSource.clip = damageSounds[Random.Range(0, damageSounds.Length)];
            audioSource.Play();
        }
    }
}
