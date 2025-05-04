using UnityEngine;

public class HealArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Faction _faction;
    [SerializeField] private int heal;
    [SerializeField] private int gain;



    private void OnTriggerEnter2D(Collider2D collider)
    {
        HealthSystem hs = collider.GetComponent<HealthSystem>();
        WaterSystem ws = collider.GetComponent<WaterSystem>();
        if (hs != null)
        {
            if (FactionHelper.IsFriend(_faction, hs.faction))
            Debug.Log($"Hit {collider.name}");
            hs.HealDamage(heal);
            ws.GainAmmo(gain);

        }

    }
}
