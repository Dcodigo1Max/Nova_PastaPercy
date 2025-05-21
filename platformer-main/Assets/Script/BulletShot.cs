using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField]
    private float speed = 400f;
    [SerializeField]
    private int damage;
    private Faction faction = Faction.Player;
    private HealthSystem healthSystem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HealthSystem collidedobject = other.gameObject.GetComponent<HealthSystem>();
        if (collidedobject != null)
        {
            if (FactionHelper.IsHostile(this.faction, collidedobject.faction))
            {
                Debug.Log("Dealt damage");
                collidedobject.DealDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
