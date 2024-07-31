using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] GameObject northernLightsTrailPrefab;

    List<Upgrade> attackUpgrades = new List<Upgrade>();
    List<Upgrade> timedUpgrades = new List<Upgrade>();
    List<Upgrade> instantUpgrades = new List<Upgrade>();
    List<Upgrade> dodgeUpgrades = new List<Upgrade>();

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade.GetUpgradeType() == UpgradeType.attack)
        {
            attackUpgrades.Add(upgrade);
        }
        else if (upgrade.GetUpgradeType() == UpgradeType.timed)
        {
            timedUpgrades.Add(upgrade);
        }
        else if (upgrade.GetUpgradeType() == UpgradeType.instant)
        {
            instantUpgrades.Add(upgrade);
        }
        else if (upgrade.GetUpgradeType() == UpgradeType.dodge)
        {
            dodgeUpgrades.Add(upgrade);
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

    public void PerformDodgeUpgrades()
    {
        foreach(Upgrade upgrade in dodgeUpgrades)
        {
            upgrade.PerformEffects();
        }
    }

    public GameObject GetNorthernLightsTrailPrefab() => northernLightsTrailPrefab;

    // testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            NorthernLights northernLights = gameObject.AddComponent<NorthernLights>();
            AddUpgrade(northernLights);

            print("Gained Upgrade!");
        }
    }
}
