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
    private Quaternion initialRotation;
    private bool isGround;
    private float jumpTimer;
    private float originalGravity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ComputeGrounded();

        float moveDir = Input.GetAxis(horizontalAxisName);
        Vector2 currentVelocity = rb.linearVelocity;
        currentVelocity.x = moveDir * velocity.x;

        if(Input.GetButton("Run") == true)
            currentVelocity.x *= 2;
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                currentVelocity.y = velocity.y;
                jumpTimer = 0.0f;
                rb.gravityScale = jumpGravityScale;
            }
        }
        else if (jumpTimer < jumpMaxDuration)
        {
            jumpTimer = jumpTimer + Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                rb.gravityScale = Mathf.Lerp(jumpGravityScale, originalGravity,
                jumpTimer / jumpMaxDuration);
            }
            else
            {
                jumpTimer = jumpMaxDuration;
                rb.gravityScale = originalGravity;
            }
        }
        else
        {
            rb.gravityScale = originalGravity;
        }

        rb.linearVelocity = currentVelocity;

        if ((moveDir < 0) && (transform.right.x > 0))
        {
            transform.rotation = initialRotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((moveDir > 0) && (transform.right.x < 0))
        {
            transform.rotation = initialRotation;
        }
    }

    void ComputeGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, 
        groundCheckRadius, groundCheckLayers);

        if ( collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        ComputeGrounded();

        if (isGround)  Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
