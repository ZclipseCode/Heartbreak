using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeDebuff : MonoBehaviour
{
    protected float freezeDuration = 2f;

    public IEnumerator Freeze(GameObject target)
    {
        EnemyMovementAI enemyMovementAI = target.GetComponent<EnemyMovementAI>();

        if (!enemyMovementAI.GetIsFrozen())
        {
            enemyMovementAI.SetIsFrozen(true);

            enemyMovementAI.enabled = false;

            BaseEnemyCombat baseEnemyCombat = target.GetComponent<BaseEnemyCombat>();
            baseEnemyCombat.StopAllCoroutines();

            SpriteRenderer spriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = new Color(0f, 100f, 255f);

            Animator animator = target.GetComponentInChildren<Animator>();
            float animatorSpeed = animator.speed;
            animator.speed = 0f;

            NavMeshAgent navMeshAgent = target.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            yield return new WaitForSeconds(freezeDuration);

            if (target != null)
            {
                enemyMovementAI.enabled = true;
                baseEnemyCombat.SetAttacking(false);
                spriteRenderer.color = Color.white;
                animator.speed = animatorSpeed;
                navMeshAgent.enabled = true;

                enemyMovementAI.SetIsFrozen(false);
            }
        }
    }
}
