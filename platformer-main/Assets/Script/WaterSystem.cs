using System.Collections;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [SerializeField]
    private float maxWaterPower = 100.0f;

    public float waterpower;
    public float wp => waterpower;
    public float wpNormalized => wp / maxWaterPower;

    [SerializeField]
    private Transform waterCheck;
    [SerializeField, Range(0.1f, 5.0f)]
    private float waterCheckRadius = 2.0f;
    [SerializeField]
    private LayerMask waterCheckLayers;
    private bool inWater;

    private float cooldownStepTimer;

    void Start()
    {
        cooldownStepTimer = 1.0f;
    }

    void Awake()
    {
        waterpower = maxWaterPower;
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
                    cooldownStepTimer = 1.0f;
                }
            }
        }
    }

    public virtual bool ReduceWaterPower(float reduction)
    {
        if(waterpower <= 0) return false;
        waterpower -= reduction;
        return true;
    }
    public bool IncreaseWaterPower()
    {
        if(waterpower >= maxWaterPower) return false;
        waterpower++;
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
