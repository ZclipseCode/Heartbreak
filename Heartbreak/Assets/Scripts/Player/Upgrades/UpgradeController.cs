using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    List<Upgrade> attackUpgrades = new List<Upgrade>();
    List<Upgrade> timedUpgrades = new List<Upgrade>();
    List<Upgrade> instantUpgrades = new List<Upgrade>();

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade.GetUpgradeType() == UpgradeType.attack)
        {
            attackUpgrades.Add(upgrade);
        }
    }

    public void PerformAttackUpgrades(GameObject target)
    {
        foreach (Upgrade upgrade in attackUpgrades)
        {
            upgrade.PerformEffects(target);
        }
    }

    public void PerformTimedUpgrades()
    {
        foreach (Upgrade upgrade in timedUpgrades)
        {
            upgrade.PerformEffects();
        }
    }

    public void PerformInstantUpgrades()
    {
        foreach (Upgrade upgrade in instantUpgrades)
        {
            upgrade.PerformEffects();
        }
    }

    // testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Frostbite frostbite = gameObject.AddComponent<Frostbite>();
            AddUpgrade(frostbite);

            print("Gained Frostbite!");
        }
    }
}
