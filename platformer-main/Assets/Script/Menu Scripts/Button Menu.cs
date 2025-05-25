using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
