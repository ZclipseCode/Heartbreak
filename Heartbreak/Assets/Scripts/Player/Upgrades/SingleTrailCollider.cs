using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTrailCollider : MonoBehaviour
{
    // each collider from a Northern Lights trail will have this

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameController.instance.StartCoroutine(GameController.instance.GetFreezeDebuff().Freeze(other.transform.parent.gameObject));
        }
    }
}
