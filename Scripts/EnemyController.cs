// Fardowsa Edited
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for patrolling.
    public float idleTime = 2f; // Time to wait at each waypoint.
    public float walkSpeed = 2f; // Walking speed when patrolling.
    public float chaseSpeed = 4f; // Speed while chasing the player.
    public float sightDistance = 10f; // Maximum distance the enemy can detect the player.
    public float fieldOfView = 45f; // Angle within which the enemy can detect the player.
    public AudioClip idleSound; // Sound to play while idling.
    public AudioClip walkingSound; // Sound to play while walking.
    public AudioClip chasingSound; // Sound to play when chasing the player.

    private int currentWaypointIndex = 0; // Index of the current waypoint the enemy is heading toward.
    private NavMeshAgent agent; // Reference to NavMeshAgent for movement.
    private Animator animator; // Reference to Animator for controlling animations.
    private float idleTimer = 0f; // Timer to track idle behavior.
    private Transform player; // Reference to the player's transform.
    private AudioSource audioSource; // For playing sound effects.

    // Enum representing the different states of the enemy.
    private enum EnemyState { Idle, Walk, Chase }
    private EnemyState currentState = EnemyState.Idle; // Current state of the enemy.

    private void Start()
    {
        // Get required components.
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();

        // Ensure waypoints are assigned.
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints not assigned to EnemyController.");
            return;
        }

        // Set the first destination and start player detection coroutine.
        SetDestinationToWaypoint();
        StartCoroutine(PlayerDetectionCoroutine());
    }

    private void Update()
    {
        // Switch logic depending on the enemy's current state.
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState();
                break;

            case EnemyState.Walk:
                HandleWalkState();
                break;

            case EnemyState.Chase:
                HandleChaseState();
                break;
        }
    }


    /// Handle behavior in Idle state: wait at the current waypoint for a certain amount of time.

    private void HandleIdleState()
    {
        idleTimer += Time.deltaTime; // Increment the idle timer.
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", false);
        PlaySound(idleSound);

        if (idleTimer >= idleTime) // If idle time has elapsed, move to the next waypoint.
        {
            NextWaypoint();
        }
    }


    /// Handle movement while walking between waypoints.

    private void HandleWalkState()
    {
        idleTimer = 0f; // Reset the idle timer upon starting to walk.
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsChasing", false);
        PlaySound(walkingSound);

        // Transition to Idle state when the enemy reaches its destination.
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = EnemyState.Idle;
        }
    }


    /// Handle behavior during the chase state.

    private void HandleChaseState()
    {
        agent.speed = chaseSpeed; // Set agent speed to chasing speed.
        agent.SetDestination(player.position); // Set the destination to the player's position.
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", true);
        PlaySound(chasingSound);

        // If the player is no longer within sight distance, return to walking behavior.
        if (Vector3.Distance(transform.position, player.position) > sightDistance)
        {
            currentState = EnemyState.Walk;
            agent.speed = walkSpeed;
        }
    }


    /// Coroutine for checking player detection at intervals.

    private IEnumerator PlayerDetectionCoroutine()
    {
        while (true)
        {
            CheckForPlayerDetection(); // Check if the player is within sight.
            yield return new WaitForSeconds(0.2f); // Check 5 times per second.
        }
    }

  
    /// Check if the player is within sight range and within field of view.

    private void CheckForPlayerDetection()
    {
        Vector3 playerDirection = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, playerDirection);

        if (angleToPlayer < fieldOfView) // Player is in the field of view angle.
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, sightDistance))
            {
                if (hit.collider.CompareTag("Player")) // Confirm detection.
                {
                    currentState = EnemyState.Chase; // Transition to chase state.
                    Debug.Log("Player detected!");
                }
            }
        }
    }


    /// Move to the next waypoint in a loop.

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop back to the first waypoint after the last one.
        SetDestinationToWaypoint();
    }


    /// Set the agent's destination to the current waypoint's position and switch to Walk state.

    private void SetDestinationToWaypoint()
    {
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentState = EnemyState.Walk;
            agent.speed = walkSpeed;
        }
    }


    /// Play sound if it's not already playing to prevent sound spamming.
 
    private void PlaySound(AudioClip soundClip)
    {
        if (!audioSource.isPlaying || audioSource.clip != soundClip)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

   
    /// Visualize the enemy's detection range or state in the editor.

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = currentState == EnemyState.Chase ? Color.red : Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}

