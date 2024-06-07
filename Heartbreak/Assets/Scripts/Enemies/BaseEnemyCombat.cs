using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyCombat : MonoBehaviour
{
    // Ranged and melee enemies will inherit this!
    protected Transform player;
    protected bool attacking;

    public abstract void Attack();

    public abstract IEnumerator ReadyAttack();

    public void SetPlayer(Transform value) => player = value;

    public void SetAttacking(bool value) => attacking = value;
}
