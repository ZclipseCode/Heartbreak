using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] LayerMask ground;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float groundDrag = 5f;
    [SerializeField] float dodgeForce = 3f;
    [SerializeField] float dodgeCooldown = 0.25f;
    [SerializeField] float airMultiplier = 8f;
    bool readyToDodge = true;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    bool grounded;
    PlayerControls playerControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        PlayerInput();
        SpeedControl();
        DragControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void PlayerInput()
    {
        horizontalInput = playerControls.Player.Movement.ReadValue<Vector2>().x;
        verticalInput = playerControls.Player.Movement.ReadValue<Vector2>().y;

        if (playerControls.Player.Dodge.ReadValue<float>() > 0 && readyToDodge)
        {
            readyToDodge = false;

            Dodge();

            Invoke(nameof(ResetDodge), dodgeCooldown);
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed && readyToDodge)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void Dodge()
    {
        rb.AddForce(rb.velocity * dodgeForce, ForceMode.Impulse);
    }

    void ResetDodge()
    {
        readyToDodge = true;
    }

    void DragControl()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }
}