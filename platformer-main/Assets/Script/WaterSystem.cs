using System.Collections;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    public delegate void OnWaterChange(int gainWater);
    [SerializeField]
    private int maxWaterPower = 5;

    public int waterpower;
    public int wp => waterpower;
    public float wpNormalized => wp / maxWaterPower;

    public event OnWaterChange onWaterChange;

    [SerializeField]
    private Transform waterCheck;
    [SerializeField, Range(0.1f, 5.0f)]
    private float waterCheckRadius = 2.0f;
    [SerializeField]
    private LayerMask waterCheckLayers;
    private bool inWater;

    private float cooldownStepTimer;

    AudioManager audioManager;



    void Start()
    {
        cooldownStepTimer = 1.0f;
    }

    void Awake()
    {
        waterpower = maxWaterPower;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        ComputeWater();
        if(inWater)
        {
            if(cooldownStepTimer > 0.0f)
            {
                cooldownStepTimer -= Time.deltaTime;
                if(cooldownStepTimer<=0.0f)
                {
                    IncreaseWaterPower();
                    onWaterChange?.Invoke(1);
                    cooldownStepTimer = 1.0f;
                }
            }
        }
    }

    public virtual bool ReduceWaterPower(int reduction)
    {
        if(waterpower <= 0) return false;
        waterpower -= reduction;
        onWaterChange?.Invoke(-reduction);
        return true;
    }
    public bool IncreaseWaterPower()
    {
        if(waterpower >= maxWaterPower) return false;
        waterpower++;
        audioManager.PlaySFX(audioManager.Water, 0.5f);
        return true;

    }

    void ComputeWater()
    {
        Collider2D watercollider = Physics2D.OverlapCircle(waterCheck.position, 
        waterCheckRadius, waterCheckLayers);

        if ( watercollider != null)
        {
            inWater = true;
            
        }
        else
        {
            inWater = false;
        }
    }
}
