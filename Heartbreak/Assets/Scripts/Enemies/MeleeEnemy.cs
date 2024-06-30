using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemyCombat
{
    [SerializeField] float attackRange = 2f;

    public override void Attack()
    {
        Vector3 direction = (player.position - attackPoint.position).normalized;
        attackPoint.position = transform.position + direction;

        bool meleeHit = Physics.CheckSphere(attackPoint.position, attackRange, playerLayer);
        print("swing");

        if (meleeHit)
        {
            Collider[] collisions = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

            foreach (Collider collision in collisions)
            {
                if (collision.CompareTag("Player"))
                {
                    print("Player hit!");

                    break;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
