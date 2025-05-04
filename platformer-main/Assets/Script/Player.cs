using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Quaternion initialRotation;
    private bool isGround;
    private float jumpTimer;
    private float originalGravity;
    [SerializeField]
    private BulletShot BulletPrefab;
    [SerializeField]
    private Transform BulletSpawn;
    [SerializeField]
    private float HealthPoints;
    [SerializeField]
    private float MaxHealthPoints = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthPoints = MaxHealthPoints;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialRotation = transform.rotation;
    }
    public void TakeDamage(float damage)
    {
        HealthPoints -= damage;

        if(HealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ComputeGrounded();

        float moveDir = Input.GetAxis(horizontalAxisName);
        Vector2 currentVelocity = rb.linearVelocity;
        currentVelocity.x = moveDir * velocity.x;

        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(BulletPrefab,BulletSpawn.position,transform.rotation);
        }

        if(Input.GetButton("Run"))
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

        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("VelocityY", currentVelocity.y);
        animator.SetBool("isGrounded", isGround);
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
