using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Avalanche : MonoBehaviour
{
    PlayerMelee playermelee;
    public float multiplier;
    float range;
    public Transform MeleePoint;


    // Start is called before the first frame update
    void Start()
    {
        playermelee = GetComponent<PlayerMelee>();
        range = playermelee.GetMeleeRange();
        playermelee.SetMeleeRange(range * multiplier);
        MeleePoint.position = new Vector3(MeleePoint.position.x + (range * multiplier), MeleePoint.position.y, MeleePoint.position.z);
    }

    // Update is called once per frame

    void Update()
    {

    }
}
