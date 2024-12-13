// Fardowsa Edited
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float idleTime = 2f;
    public float walkSpeed = 2f; // Walking speed.
    public float chaseSpeed = 4f; // Chasing speed.
    public float sightDistance = 10f;
    public float fieldOfView = 45f; // Field of view angle for detection.
    public AudioClip idleSound;
    public AudioClip walkingSound;
    public AudioClip chasingSound;

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private float idleTimer = 0f;
    private Transform player;
    private AudioSource audioSource;

    private enum EnemyState { Idle, Walk, Chase }
    private EnemyState currentState = EnemyState.Idle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();

        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints not assigned to EnemyController.");
            return;
        }

        SetDestinationToWaypoint();
        StartCoroutine(PlayerDetectionCoroutine());
    }

    private void Update()
    {
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

    private void HandleIdleState()
    {
        idleTimer += Time.deltaTime;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", false);
        PlaySound(idleSound);

        if (idleTimer >= idleTime)
        {
            NextWaypoint();
        }
    }

    private void HandleWalkState()
    {
        idleTimer = 0f;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsChasing", false);
        PlaySound(walkingSound);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = EnemyState.Idle;
        }
    }

    private void HandleChaseState()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", true);
        PlaySound(chasingSound);

        if (Vector3.Distance(transform.position, player.position) > sightDistance)
        {
            currentState = EnemyState.Walk;
            agent.speed = walkSpeed;
        }
    }

    private IEnumerator PlayerDetectionCoroutine()
    {
        while (true)
        {
            CheckForPlayerDetection();
            yield return new WaitForSeconds(0.2f); // Check for detection 5 times per second.
        }
    }

    private void CheckForPlayerDetection()
    {
        Vector3 playerDirection = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, playerDirection);

        if (angleToPlayer < fieldOfView)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, sightDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    currentState = EnemyState.Chase;
                    Debug.Log("Player detected!");
                }
            }
        }
    }

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetDestinationToWaypoint();
    }

    private void SetDestinationToWaypoint()
    {
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentState = EnemyState.Walk;
            agent.speed = walkSpeed;
        }
    }

    private void PlaySound(AudioClip soundClip)
    {
        if (!audioSource.isPlaying || audioSource.clip != soundClip)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = currentState == EnemyState.Chase ? Color.red : Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}

