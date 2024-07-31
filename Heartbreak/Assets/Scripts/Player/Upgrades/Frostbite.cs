using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostbite : Upgrade
{
    FreezeDebuff freezeDebuff;

    private void Awake()
    {
        upgradeType = UpgradeType.attack;

        freezeDebuff = gameObject.GetComponent<FreezeDebuff>();

        if (freezeDebuff == null)
        {
            freezeDebuff = gameObject.AddComponent<FreezeDebuff>();
        }
    }

    public override void PerformEffects(GameObject target)
    {
        StartCoroutine(freezeDebuff.Freeze(target));
    }
}
