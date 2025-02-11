using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public enum enemyState { Idle, Patrol, Chase }
    public enemyState currentState = enemyState.Idle;

    private Animator animator;
    public Transform player;
    private NavMeshAgent agent;
    private CheckpointSystem checkpointSystem; // Reference to CheckpointSystem

    // Patrol movement variables
    public Transform[] checkpoints;
    private int currentCheckpoint = 0;

    // Attack and Chase variables
    public float detectionRange = 4f;
    public float attackRange = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        checkpointSystem = FindObjectOfType<CheckpointSystem>(); // Find the CheckpointSystem in the scene

        if (checkpoints.Length > 0)
        {
            currentState = enemyState.Patrol;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case enemyState.Idle:
                Idle();
                break;

            case enemyState.Patrol:
                Patrol();
                break;

            case enemyState.Chase:
                Chase();
                break;
        }
    }

    void Idle()
    {
        animator.SetTrigger("Idle");
        agent.isStopped = true;
    }

    void Patrol()
    {
        animator.SetTrigger("Walk");

        if (checkpoints.Length == 0)
        {
            return; // No checkpoints to patrol
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentCheckpoint = (currentCheckpoint + 1) % checkpoints.Length;
            agent.destination = checkpoints[currentCheckpoint].position;
        }

        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            currentState = enemyState.Chase;
        }
    }

    void Chase()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Run");

        agent.isStopped = false;
        agent.destination = player.position;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            PlayerCaught();
        }
        else if (distance > detectionRange)
        {
            currentState = enemyState.Patrol;
        }
    }

    void PlayerCaught()
    {
        Debug.Log("Player caught by enemy!");
        checkpointSystem.HandlePlayerCaught(); // Notify CheckpointSystem
    }
}
