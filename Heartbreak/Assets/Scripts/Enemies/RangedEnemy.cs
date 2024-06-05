using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileForce = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ReadyShot());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    void Shoot()
    {
        GameObject p = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        Rigidbody pRb = p.GetComponent<Rigidbody>();

        pRb.AddForce(shootPoint.forward * projectileForce, ForceMode.Impulse);
    }

    IEnumerator ReadyShot()
    {
        yield return new WaitForSeconds(shootDelay);

        Shoot();
        
        StartCoroutine(ReadyShot());
    }
}
