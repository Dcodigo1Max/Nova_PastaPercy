using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool  checkEndOfPlatform = true;
    [SerializeField] private Transform checkEndOfPlatformTransform;
    [SerializeField] private LayerMask checkEndOfPlatformMask;
    [SerializeField] private bool checkWall = true;
    [SerializeField] private Transform checkWallTransform;
    [SerializeField] private LayerMask checkWallMask;
    [SerializeField] private float sensorRadius;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField, Range(0.1f, 5.0f)]
    private float groundCheckRadius = 2.0f;
    [SerializeField]
    private LayerMask groundCheckLayers;
    private bool isGround;
    private float direction = 1.0f;

    [SerializeField]
    private Vector2 velocity;
    private Rigidbody2D rb;

    private Animator animator;
    private Quaternion initialRotation;

    private float HealthPoints;
    private float MaxHealthPoints = 2;

    private float GetDirection()
    {
        return direction;
    }
    void Start()
    {

        HealthPoints = MaxHealthPoints;

        rb = GetComponent<Rigidbody2D>();

        //animator = GetComponent<Animator>();

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

        if (isGround)
        {
            if ((checkEndOfPlatform) && (checkEndOfPlatformTransform != null))
            {
                Collider2D collider = Physics2D.OverlapCircle(checkEndOfPlatformTransform.position,
                                                              sensorRadius,
                                                              checkEndOfPlatformMask);
                if (collider == null)
                {
                    direction = -direction;
                }
            }
            if ((checkWall) && (checkWallTransform != null))
            {
                Collider2D collider = Physics2D.OverlapCircle(checkWallTransform.position,
                                                              sensorRadius,
                                                              checkWallMask);
                if (collider != null)
                {
                    direction = -direction;
                }
            }

            rb.linearVelocity = new Vector2(direction * velocity.x, rb.linearVelocity.y);
        }
        else
        {
        }

        if ((GetDirection() < 0) && (transform.right.x > 0))
        {
            transform.rotation = initialRotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((GetDirection() > 0) && (transform.right.x < 0))
        {
            transform.rotation = initialRotation;
        }

        Vector2 currentVelocity = rb.linearVelocity;

        //animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
        //animator.SetFloat("VelocityY", currentVelocity.y);
        //animator.SetBool("isGrounded", isGround);
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

        if ((checkEndOfPlatform) && (checkEndOfPlatformTransform != null))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(checkEndOfPlatformTransform.position, sensorRadius);
        }
        if ((checkWall) && (checkWallTransform != null))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(checkWallTransform.position, sensorRadius);
        }
    }
}
