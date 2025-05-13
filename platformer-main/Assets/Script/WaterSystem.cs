using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [SerializeField]
    private float maxWaterPower = 100.0f;
    
    public float waterpower;
    public float wp => waterpower;
    public float wpNormalized => wp / maxWaterPower;

    void Awake()
    {
        waterpower = maxWaterPower;
    }

    void Update()
    {
        
    }

    public bool ReduceWaterPower()
    {
        if(waterpower <= 0) return false;
        waterpower--;
        return true;
    }
    public bool IncreaseWaterPower()
    {
        if(waterpower >= maxWaterPower) return false;
        waterpower++;
        return true;
    }
}
