using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileForce = 1f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] bool canMoveWhileAttacking;
    EnemyMovementAI enemyMovementAI;
    Transform player;
    bool playerInAttackRange;
    bool attacking;

    private void Start()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        enemyMovementAI.SetCanMoveWhileAttacking(canMoveWhileAttacking);
        player = enemyMovementAI.GetPlayer();
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (playerInAttackRange && !attacking)
        {
            attacking = true;
            Shoot(player);
        }
        else if (!playerInAttackRange)
        {
            attacking = false;
            StopAllCoroutines();
        }
    }

    void Shoot(Transform target)
    {
        GameObject p = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        Rigidbody pRb = p.GetComponent<Rigidbody>();
        Vector3 direction = (target.position - pRb.position).normalized;

        pRb.AddForce(direction * projectileForce, ForceMode.Impulse);
    }

    IEnumerator ReadyShot(Transform target)
    {
        yield return new WaitForSeconds(shootDelay);

        Shoot(target);
        
        StartCoroutine(ReadyShot(target));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
