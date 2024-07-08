using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] Image healthIcon;
    [SerializeField] Sprite[] healthStages;

    private void Awake()
    {
        PlayerHealth.healthChange += UpdateHealth;
    }

    public void UpdateHealth(int health)
    {
        healthIcon.sprite = healthStages[health];
    }

    private void OnDestroy()
    {
        PlayerHealth.healthChange -= UpdateHealth;
    }
}
