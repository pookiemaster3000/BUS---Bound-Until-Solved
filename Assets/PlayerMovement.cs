using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float dashSpeed = 5f;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;

    [Header("Ground Check Settings")]
    public float playerHeight;

    [SerializeField] private LayerMask WhatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Ground Check
        grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            playerHeight * 0.5f + 0.2f,
            WhatIsGround
        );

        MyInput();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        // Get movement input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput
                      + orientation.right * horizontalInput;

        // Check if player is holding Shift + moving forward
        bool isDashing = Input.GetKey(dashKey) && verticalInput > 0;

        // Choose movement speed
        float currentSpeed = isDashing ? dashSpeed : moveSpeed;

        // Move on ground
        if (grounded)
        {
            rb.AddForce(
                moveDirection.normalized * currentSpeed * 10f,
                ForceMode.Force
            );
        }
        // Move in air
        else
        {
            rb.AddForce(
                moveDirection.normalized * currentSpeed * 10f * airMultiplier,
                ForceMode.Force
            );
        }

        SpeedControl(currentSpeed);
    }

    private void SpeedControl(float currentSpeed)
    {
        // Get horizontal velocity
        Vector3 flatVel = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        // Limit velocity based on current movement speed
        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;

            rb.linearVelocity = new Vector3(
                limitedVel.x,
                rb.linearVelocity.y,
                limitedVel.z
            );
        }
    }

    private void Jump()
    {
        // Reset vertical velocity
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        rb.AddForce(
            transform.up * jumpForce,
            ForceMode.Impulse
        );
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}