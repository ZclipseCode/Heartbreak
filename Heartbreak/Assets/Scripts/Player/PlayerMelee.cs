using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    // should probably inherit from a "PlayerAttack", which can track either melee or ranged
    [SerializeField] Transform meleePoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float meleeRange = 1f;
    [SerializeField] float meleeCooldown = 1f;
    [SerializeField] float damage = 10f;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] PlayerMovement playerMovement;
    bool readyToMelee = true;
    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (playerControls.Player.StandardAttack.ReadValue<float>() > 0 && readyToMelee && playerMovement.GetReadyToDodge())
        {
            readyToMelee = false;

            Melee();

            Invoke(nameof(ResetMelee), meleeCooldown);
        }
    }

    void Melee()
    {
        bool meleeHit = Physics.CheckSphere(meleePoint.position, meleeRange, enemyLayer);

        if (meleeHit)
        {
            Collider[] collisions = Physics.OverlapSphere(meleePoint.position, meleeRange, enemyLayer);

            foreach (Collider collision in collisions)
            {
                if (collision.CompareTag("Enemy"))
                {
                    EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
                    enemyHealth.LoseHealth(damage);

                    break;
                }
            }
        }

        playerAnimation.StartAnimation("isAttacking");
    }

    void ResetMelee()
    {
        readyToMelee = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }

    public bool GetReadyToMelee() => readyToMelee;

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }
}
