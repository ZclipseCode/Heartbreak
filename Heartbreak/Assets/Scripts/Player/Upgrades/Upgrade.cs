using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    protected UpgradeType upgradeType;

    public virtual void PerformEffects() { }

    public virtual void PerformEffects(GameObject target) { }

    public UpgradeType GetUpgradeType() => upgradeType;
}
