using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemyCombat
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileForce = 15f;

    public override void Attack()
    {
        GameObject p = Instantiate(projectile, attackPoint.position, Quaternion.identity);
        p.GetComponent<EnemyProjectile>().SetDamage(damage);

        Rigidbody pRb = p.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - pRb.position).normalized;

        pRb.AddForce(direction * projectileForce, ForceMode.Impulse);
    }
}
