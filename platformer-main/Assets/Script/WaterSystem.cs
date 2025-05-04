using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using static HealthSystem;

public class WaterSystem : MonoBehaviour
{


    public delegate void OnAmmoChange(int lose);
    public delegate void OnAmmoGain(int gain);

    [SerializeField] private int maxAmmo = 5;
    [SerializeField] private int lose;
    [SerializeField] private int gain;

    public int mana => ammo;

    public int manaNormalised => mana / maxAmmo;

    public event OnAmmoChange onAmmoChange;
    public event OnAmmoGain onAmmoGain;
   
        
    private int ammo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ammo = maxAmmo;

    }

    // Update is called once per frame
    void Update()
    {
     

    }

    public bool DealDamage(int lose)
    {
        if (ammo <= 0) return false;



        onAmmoChange?.Invoke(lose);

        if (ammo >= 5 && Input.GetKeyDown("w"))
        {
            ammo = ammo - lose;
        }
       

        return true;
    }

    public bool GainAmmo(int gain)
    {
        if (ammo <= 0.0f) return false;
        
        onAmmoGain?.Invoke(gain);

        if (ammo <= 0.0f)
        {
            ammo = ammo + gain;
            onAmmoGain?.Invoke(gain);

        }
        if (ammo >= 5.0f)
        {
            ammo = maxAmmo;
            onAmmoGain?.Invoke(gain);
        }

        return true;
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        collider.GetComponent<BoxCollider2D>();
        if (Input.GetKeyDown("w") && collider.tag == "Water")
        {
            ammo = ammo - 1;

        }

    }
     */ 


}
