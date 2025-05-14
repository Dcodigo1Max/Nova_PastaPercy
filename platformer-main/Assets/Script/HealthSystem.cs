using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void OnHealthChange(float damage);
    public delegate void OnDeath();
    public delegate void OnInvulnerable(bool on);

    [SerializeField] 
    private Faction _faction;
    [SerializeField] 
    private float maxHealth = 100.0f;
    [SerializeField] 
    private float invulnerabilityDuration = 2.0f;

    public Faction faction => _faction;
    private float health;
    private float invulnerableTimer;

    public float hp => health;
    public float hpNormalized => hp / maxHealth;
    public event OnHealthChange onHealthChange;
    public event OnDeath onDeath;
    public event OnInvulnerable onInvulnerable;

    void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (invulnerableTimer > 0)
        {
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableTimer <= 0)
            {
                onInvulnerable?.Invoke(false);
            }
        }
    }

    public bool DealDamage(float damage)
    {
        if (health <= 0) return false;
        if ( invulnerableTimer > 0) return false;

        health = health - damage;
        onHealthChange?.Invoke(damage);

        if (health <= 0)
        {
            onDeath?.Invoke();
        }
        else
        {
            invulnerableTimer = invulnerabilityDuration;
            onInvulnerable?.Invoke(true);
        }
        return true;
    }
    public bool IncreaseHealth()
    {
        if(health >= maxHealth) return false;
        health++;
        return true;
    }
    public float ReturnHealth()
    {
        return health;
    }
}
