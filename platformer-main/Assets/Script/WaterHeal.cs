using UnityEngine;

public class WaterHeal : MonoBehaviour
{
    [SerializeField] public int healthValue;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthSystem>().AddHealth(healthValue);
            
        }
    }
}
