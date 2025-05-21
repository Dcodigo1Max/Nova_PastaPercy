using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool checkEndOfPlatform = true;
    [SerializeField]
    private Transform checkEndOfPlatformTransform;
    [SerializeField]
    private LayerMask checkEndOfPlatformMask;
    [SerializeField]
    private bool checkWall = true;
    [SerializeField]
    private Transform checkWallTransform;
    [SerializeField]
    private LayerMask checkWallMask;
    [SerializeField]
    private float sensorRadius;

    private float direction = 1.0f;

    protected override float GetDirection()
    {
        return direction;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

            healthSystem.onDeath += EnemyOnDeath;
    }
    void EnemyOnDeath()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        ComputeGrounded();

        if (isGround)
        {
            if ((checkEndOfPlatform) && (checkEndOfPlatformTransform != null))
            {
                Collider2D collider = Physics2D.OverlapCircle(checkEndOfPlatformTransform.position, sensorRadius, checkEndOfPlatformMask);
                if (collider == null)
                {
                    direction = -direction;
                }
            }
            if ((checkWall) && (checkWallTransform != null))
            {
                Collider2D collider = Physics2D.OverlapCircle(checkWallTransform.position, sensorRadius, checkWallMask);

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

        base.Update();
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        if ((checkEndOfPlatform) && (checkEndOfPlatformTransform != null))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(checkEndOfPlatformTransform.position, sensorRadius);
        }
        if((checkWall) && (checkWallTransform!= null))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(checkWallTransform.position, sensorRadius);
        }
    }
}

