using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ScoreScene()
    {
        SceneManager.LoadScene("Score Scene");
    }
}
