using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public delegate void HealthChangeDelegate(int value);
    public static HealthChangeDelegate healthChange;

    [SerializeField] int maxHealth = 4;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        healthChange(currentHealth);
    }

    public void GainHealth(int value)
    {
        if (currentHealth + value > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += value;
        }

        healthChange(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoseHealth(1);
        }
    }

    public void LoseHealth(int value)
    {
        if (currentHealth - value <= 0)
        {
            currentHealth = 0;
            print("Game Over");
        }
        else
        {
            currentHealth -= value;
        }

        healthChange(currentHealth);
    }
}
