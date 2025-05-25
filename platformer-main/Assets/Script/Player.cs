using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private WaterProjectile BulletPrefab;
    [SerializeField]
    private Transform BulletSpawn;

    private Vector2 originalVelocity;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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

        originalVelocity = velocity;
    }

    private void PlayerOnDeath()
    {
        Destroy(gameObject);
        audioManager.PlaySFX(audioManager.Death, 1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        ComputeGrounded();
        ComputeEnd();

        if (isEndLevel)
        {
            Timer.instance.GameWon();
            SceneManager.LoadScene("Menu");
        }

        groundCollider.enabled = isGround;
        airCollider.enabled = !isGround;

        moveDir = Input.GetAxis(horizontalAxisName);

        Vector2 currentVelocity = rb.linearVelocity;

        currentVelocity.x = moveDir * velocity.x;

        if (Input.GetButtonDown("Fire1"))
        {
            if (waterSystem.waterpower > 0)
            {
                animator.SetTrigger("isFired");
                audioManager.PlaySFX(audioManager.Waterattack,1);
                Instantiate(BulletPrefab, BulletSpawn.position, transform.rotation);
                waterSystem.ReduceWaterPower(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
            {
                if (waterSystem.waterpower >= 3 && healthSystem.ReturnHealth() < 5)
                {
                    healthSystem.AddHealth(1);
                    waterSystem.ReduceWaterPower(3);
                }
            }

        if (Input.GetKey(KeyCode.RightShift))
        {
            if (isGround)
            {
                velocity = sprintvelocity;
            }
        }
        else
        {
            velocity = originalVelocity;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level 3");
        }

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

                audioManager.PlaySFX(audioManager.Jumping, 0.15f);
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
