using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void StartGame()
    {
        SceneManager.LoadScene("Level 3");
    }
       

  
}
