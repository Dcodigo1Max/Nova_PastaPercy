using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        
        if (enemy)
        {
            enemy.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
