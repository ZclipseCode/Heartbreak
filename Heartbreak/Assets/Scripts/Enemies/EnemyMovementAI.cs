using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float walkPointRange = 1f;
    [SerializeField] float sightRange = 1f;
    NavMeshAgent agent;
    Transform player;
    Vector3 walkPoint;
    bool walkPointSet;
    bool playerInSightRange;
    bool canMoveWhileAttacking;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        if (!playerInSightRange)
        {
            Patrol();
        }
        else if (playerInSightRange && canMoveWhileAttacking)
        {
            agent.SetDestination(player.position);
        }
        else if (playerInSightRange && !canMoveWhileAttacking)
        {
            walkPointSet = false;
            agent.SetDestination(transform.position);
        }
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    public void SetCanMoveWhileAttacking(bool value) => canMoveWhileAttacking = value;

    public Transform GetPlayer() => player;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
