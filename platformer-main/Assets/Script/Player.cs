using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private string horizontalAxisName = "Horizontal";
    [SerializeField]
    private Transform groundCheck;
    [SerializeField, Range(0.1f, 5.0f)]
    private float groundCheckRadius = 2.0f;
    [SerializeField]
    private LayerMask groundCheckLayers;
    [SerializeField, Header("Jump Parameters")]
    private float jumpMaxDuration = 0.1f;
    [SerializeField]
    private float jumpGravityScale = 1.0f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGround;
    private float jumpTimer;
    private float originalGravity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDir = Input.GetAxis(horizontalAxisName);
        Vector2 currentVelocity = rb.linearVelocity;
        currentVelocity.x = moveDir * velocity.x;

        rb.linearVelocity = currentVelocity;
    }
}
