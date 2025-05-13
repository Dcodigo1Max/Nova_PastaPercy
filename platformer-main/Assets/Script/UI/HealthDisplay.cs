using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameObject[] healthItems;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSystem.onHealthChange += HealthSystem_onHealthChange;
        healthSystem.onHealthAlter += HealthSystem_OnHealthAlter;


        UpdateHealthDisplay();
    }

    private void HealthSystem_onHealthChange(int damage)
    {
        UpdateHealthDisplay();
    }

    private void HealthSystem_OnHealthAlter(int health)
    {
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        Debug.Log("Health modified");
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
