using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameObject[] healthItems;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSystem.onHealthChange += HealthSystem_onHealthChange;
       


        UpdateHealthDisplay();
    }

    private void HealthSystem_onHealthChange(int damage)
    {
        UpdateHealthDisplay();
    }

   
    void UpdateHealthDisplay()
    {
        int health = healthSystem.hp;

        for (int i = 0; i < health; i++)
        {
            healthItems[i].SetActive(true);
        }
        for (int i = health; i < healthItems.Length; i++)
        {
            healthItems[i].SetActive(false);
        }

    }



}


