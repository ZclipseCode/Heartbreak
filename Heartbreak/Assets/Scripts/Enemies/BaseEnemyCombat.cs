using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyCombat : MonoBehaviour
{
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected PlayerAnimation enemyAnimation;
    protected Transform player;
    protected bool attacking;
    protected bool isMelee;

    public abstract void Attack();

    public IEnumerator ReadyAttack()
    {
        if (!attacking)
        {
            attacking = true;

            if (!isMelee)
            {
                enemyAnimation.StartAnimation("isAttacking");
            }

            yield return new WaitForSeconds(attackDelay);

            if (!isMelee)
            {
                enemyAnimation.End("isAttacking");
                enemyAnimation.GetAnimator().Play("Idle");
            }
            else
            {
                enemyAnimation.StartAnimation("isAttacking");
            }

            Attack();

            attacking = false;

            StartCoroutine(ReadyAttack());
        }
    }

    public void SetPlayer(Transform value) => player = value;

    public void SetAttacking(bool value) => attacking = value;
}
