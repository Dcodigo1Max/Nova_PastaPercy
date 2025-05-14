using System.Collections;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private string horizontalAxisName = "Horizontal";
    [SerializeField, Header("Jump Parameters")]
    private float jumpMaxDuration = 0.1f;
    [SerializeField]
    private float jumpGravityScale = 1.0f;
    [SerializeField]
    private Collider2D groundCollider;
    [SerializeField]
    private Collider2D airCollider;
    private float jumpTimer;
    private float originalGravity;
    private float moveDir;

    protected WaterSystem waterSystem;

    [SerializeField]
    private BulletShot BulletPrefab;
    [SerializeField]
    private Transform BulletSpawn;

    protected override float GetDirection()
    {
        return moveDir;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        waterSystem = GetComponent<WaterSystem>();

        originalGravity = rb.gravityScale;

        healthSystem.onDeath += PlayerOnDeath;
    }

    private void PlayerOnDeath()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        ComputeGrounded();

        groundCollider.enabled = isGround;
        airCollider.enabled = !isGround;

        moveDir = Input.GetAxis(horizontalAxisName);

        Vector2 currentVelocity = rb.linearVelocity;

        currentVelocity.x = moveDir * velocity.x;

        if(Input.GetButtonDown("Fire1"))
        {
            if(waterSystem.waterpower > 0)
                Instantiate(BulletPrefab,BulletSpawn.position,transform.rotation);
                waterSystem.ReduceWaterPower(1);
        }
        if(Input.GetButtonDown("Fire2"))
        {
            if(waterSystem.waterpower > 3 && healthSystem.ReturnHealth() < 5)
            {
                healthSystem.IncreaseHealth();
                waterSystem.ReduceWaterPower(3);
                
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(isGround)
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
                rb.gravityScale = Mathf.Lerp(jumpGravityScale, originalGravity, jumpTimer / jumpMaxDuration);
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

        base.Update();
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
