using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] FreezeDebuff freezeDebuff;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public FreezeDebuff GetFreezeDebuff() => freezeDebuff;
}
