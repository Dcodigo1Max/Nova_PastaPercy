using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void OnHealthChange(int damage);
    public delegate void OnHealthAlter(int health);
    public delegate void OnDeath();
    public delegate void OnInvulnerable(bool on);


    [SerializeField] private Faction _faction;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invulnerableDuration = 2.0f;

    public Faction faction => _faction;

    public int hp => health;

    public int hpNormalised => hp / maxHealth;

    public event OnHealthChange onHealthChange;
    public event OnHealthAlter onHealthAlter;
    public event OnDeath onDeath;
    public event OnInvulnerable onInvulnerable;


    private int health;
    private float invulnerableTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(invulnerableTimer > 0)
        {
            invulnerableTimer -= Time.deltaTime;
            if(invulnerableTimer <=0 )
            {
                onInvulnerable?.Invoke(false);
            }
        }
    }

    // Update is called once per frame
    public bool DealDamage(int damage)
    {
        if (health <= 0) return false;

        if (invulnerableTimer > 0) return false;

        health = health - damage;

        onHealthChange?.Invoke(damage);

        if(health <= 0)
        {
            onDeath?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            invulnerableTimer = invulnerableDuration;
            onInvulnerable?.Invoke(true);
        }
        
        return true;
    }

    public bool HealDamage(int heal)
    {
        if (health <= 0) return false;
       
        

        onHealthChange?.Invoke(heal);

        if (health > 0)
        {
            health = health + heal;
            onHealthChange?.Invoke(heal);

        }
        if(health >= 3)
        {
            health = maxHealth;
            onHealthChange?.Invoke(heal);
        }

        return true;
    }




}
