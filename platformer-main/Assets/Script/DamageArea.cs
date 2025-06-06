using JetBrains.Annotations;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Faction _faction;
    [SerializeField] private int damage;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        HealthSystem hs = collider.GetComponent<HealthSystem>();
        if(hs != null)
        {
            if (FactionHelper.IsHostile(_faction, hs.faction))
            Debug.Log($"Hit {collider.name}");
            hs.DealDamage(damage);
            audioManager.PlaySFX(audioManager.Damage, 1);
        }
   }
}