using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyCombat : MonoBehaviour
{
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected int damage = 1;
    protected Transform player;
    protected bool attacking;

    public abstract void Attack();

    public IEnumerator ReadyAttack()
    {
        if (!attacking)
        {
            attacking = true;

            yield return new WaitForSeconds(attackDelay);

            Attack();

            attacking = false;

            StartCoroutine(ReadyAttack());
        }
    }

    public void SetPlayer(Transform value) => player = value;

    public void SetAttacking(bool value) => attacking = value;
}
