using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Vector2 velocity;
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField, Range(0.1f, 5.0f)]
    protected float groundCheckRadius = 2.0f;
    [SerializeField]
    protected LayerMask groundCheckLayers;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected HealthSystem healthSystem;
    protected Quaternion initialRotation;
    protected bool isGround;
    protected bool invulnerableEnable = false;
    protected float invulnerableTimer;

    protected abstract float GetDirection();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onInvulnerable += HealthSystem_onInvulnerable;

        initialRotation = transform.rotation;
    }

    private void HealthSystem_onInvulnerable(bool on)
    {
        invulnerableEnable = on;
        if (invulnerableEnable)
        {
            invulnerableTimer = 0.1f;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if ((GetDirection() < 0) && (transform.right.x > 0))
        {
            transform.rotation = initialRotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((GetDirection() > 0) && (transform.right.x < 0))
        {
            transform.rotation = initialRotation;
        }

        Vector2 currentVelocity = rb.linearVelocity;

        animator.SetFloat("AbsVelocity", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("VelocityY", currentVelocity.y);
        animator.SetBool("isGrounded", isGround);

        if (invulnerableEnable)
        {
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableTimer < 0)
            {
                invulnerableTimer = 0.1f;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    protected void ComputeGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayers);

        if(collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    
    protected virtual void OnDrawGizmosSelected()
    {
        if(groundCheck == null) return;

        ComputeGrounded();

        if (isGround) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
