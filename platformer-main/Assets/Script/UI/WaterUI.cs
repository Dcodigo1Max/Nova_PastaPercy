using UnityEngine;

public class WaterUI : MonoBehaviour
{
    [SerializeField] private WaterSystem waterSystem;
    [SerializeField] private GameObject[] waterItems;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterSystem.onWaterChange += WaterSystem_onWaterChange;



        UpdateWaterDisplay();
    }

    private void WaterSystem_onWaterChange(int gainWater)
    {
        UpdateWaterDisplay();
    }


    void UpdateWaterDisplay()
    {
        int waterpower = waterSystem.wp;

        for (int i = 0; i < waterpower; i++)
        {
            waterItems[i].SetActive(true);
        }
        for (int i = waterpower; i < waterItems.Length; i++)
        {
            waterItems[i].SetActive(false);
        }

    }
  
}
