using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    int damage = 1;

    void Start()
    {
        StartCoroutine(DestroyProjectile());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.LoseHealth(damage);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }

    public void SetDamage(int value) => damage = value;
}
