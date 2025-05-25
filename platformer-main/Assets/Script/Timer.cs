using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    private bool GameOver = false;

    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(remainingTime <= 0.01f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }


    }


    public void GameWon()
    {
        GameOver = true;

        
        float bestTime = GetBestTime();

        PlayerPrefs.SetFloat("remainingTime", bestTime);
        PlayerPrefs.Save();

    }





    public float GetBestTime()
    {
        return remainingTime;
    }


}
