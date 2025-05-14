using UnityEngine;

public class Enemy_Capsule : MonoBehaviour
{
    [SerializeField] public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthSystem>().DealDamage(damage);
        }
    }

}
