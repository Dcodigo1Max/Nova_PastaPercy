using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI timeScoreText;


    //It is used to start UpdateHighScore 
    private void Start()
    {
        UpdateTimeScore();
    }


    // Update is called once per frame
    void UpdateTimeScore()
    {

        float timeScore = PlayerPrefs.GetFloat("remainingTime", 0f);
        string totalTimeString = " Your High Score is " + timeScore;

        timeScoreText.text = totalTimeString;

    }
}
