using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void GainHealth(float value)
    {
        if (currentHealth + value > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += value;
        }
    }

    public void LoseHealth(float value)
    {
        if (currentHealth - value <= 0)
        {
            currentHealth = 0;
            Destroy(transform.parent.gameObject);
        }
        else
        {
            currentHealth -= value;
        }
    }
}
