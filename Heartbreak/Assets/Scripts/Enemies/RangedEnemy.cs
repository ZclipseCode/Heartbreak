using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileForce = 1f;
    [SerializeField] bool canMoveWhileAttacking;
    EnemyMovementAI enemyMovementAI;

    private void Start()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        enemyMovementAI.SetCanMoveWhileAttacking(canMoveWhileAttacking);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ReadyShot(other.transform));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
}
