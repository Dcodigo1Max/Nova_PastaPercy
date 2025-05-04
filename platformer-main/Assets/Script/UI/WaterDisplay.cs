using UnityEngine;
using UnityEngine.UI;

public class HeallthDisplayWater : MonoBehaviour
{
    [SerializeField] private WaterSystem waterSystem;
    [SerializeField] private GameObject[] waterItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterSystem.onAmmoChange += WaterSystem_onAmmoChange;
        waterSystem.onAmmoGain += WaterSystem_onAmmoGain;

        UpdateWaterDisplay();
    }

    
    private void WaterSystem_onAmmoChange(int lose)
    {
        UpdateWaterDisplay();
    }
    

    private void WaterSystem_onAmmoGain(int gain)
    {
        UpdateWaterDisplay();
    }

    void UpdateWaterDisplay()
    {
        int health = waterSystem.mana;

        for (int i = 0; i < health; i++)
        {
            waterItems[i].SetActive(true);
        }
        for (int i = health; i < waterItems.Length; i++)
        {
            waterItems[i].SetActive(false);
        }
    }

}
