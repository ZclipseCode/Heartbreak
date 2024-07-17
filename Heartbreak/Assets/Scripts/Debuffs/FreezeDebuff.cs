using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeDebuff : MonoBehaviour
{
    protected float freezeDuration = 2f;

    public IEnumerator Freeze(GameObject target)
    {
        BaseEnemyCombat baseEnemyCombat = target.GetComponent<BaseEnemyCombat>();
        baseEnemyCombat.StopAllCoroutines();

        SpriteRenderer spriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(0f, 100f, 255f);

        EnemyMovementAI enemyMovementAI = target.GetComponent<EnemyMovementAI>();
        enemyMovementAI.enabled = false;

        Animator animator = target.GetComponentInChildren<Animator>();
        float animatorSpeed = animator.speed;
        animator.speed = 0f;

        yield return new WaitForSeconds(freezeDuration);

        if (target != null)
        {
            baseEnemyCombat.SetAttacking(false);
            spriteRenderer.color = Color.white;
            enemyMovementAI.enabled = true;
            animator.speed = animatorSpeed;
        }
    }
}
