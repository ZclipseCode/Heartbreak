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
    [SerializeField] float attackRange = 1f;
    [SerializeField] bool canMoveWhileAttacking;
    [SerializeField] bool facingRight;
    [SerializeField] PlayerAnimation enemyAnimation; // should probably just be Animation.cs or something similiar
    NavMeshAgent agent;
    Transform player;
    BaseEnemyCombat enemyCombat;
    Vector3 walkPoint;
    bool walkPointSet;
    bool playerInSightRange;
    bool playerInAttackRange;
    bool isFrozen;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCombat = GetComponent<BaseEnemyCombat>();
        enemyCombat.SetPlayer(player);
    }

    void Update()
    {
        enemyAnimation.Walk(agent.desiredVelocity.x, agent.desiredVelocity.y);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange)
        {
            Patrol();
        }
        else if ((playerInSightRange && !playerInAttackRange) || (playerInSightRange && playerInAttackRange && canMoveWhileAttacking))
        {
            agent.SetDestination(player.position);
        }
        else if (playerInSightRange && playerInAttackRange && !canMoveWhileAttacking)
        {
            walkPointSet = false;
            agent.SetDestination(transform.position);
        }

        if (playerInAttackRange)
        {
            enemyCombat.StartCoroutine(enemyCombat.ReadyAttack());
        }
        if (!playerInAttackRange)
        {
            enemyCombat.StopAllCoroutines();
            enemyCombat.SetAttacking(false);
        }

        Flip(agent.velocity.normalized.x);
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

    void Flip(float horizontal)
    {
        if (((horizontal < 0 && facingRight) || (horizontal > 0 && !facingRight)))
        {
            facingRight = !facingRight;
            Vector3 currentScale = transform.localScale;
            currentScale.x = -currentScale.x;
            transform.localScale = currentScale;
        }
    }

    public bool GetIsFrozen() => isFrozen;
    public void SetIsFrozen(bool value) => isFrozen = value;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
