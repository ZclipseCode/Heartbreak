using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemyCombat
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileForce = 1f;

    public override void Attack()
    {
        GameObject p = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        Rigidbody pRb = p.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - pRb.position).normalized;

        pRb.AddForce(direction * projectileForce, ForceMode.Impulse);
    }

    public override IEnumerator ReadyAttack()
    {
        if (!attacking)
        {
            attacking = true;

            yield return new WaitForSeconds(shootDelay);

            Attack();

            attacking = false;

            StartCoroutine(ReadyAttack());
        }
    }
}
