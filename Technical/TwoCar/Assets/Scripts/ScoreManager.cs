using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public int highScore;
    public int score;
    public int gold;
    public int diamon;
    public float timeAdd = 1f;
    public int totalGold;

    public Text playScore;
    public Text overScore;
    public Text overHightScore;

    public Text playGold;
    public Text shopGold;


    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        gold = PlayerPrefs.GetInt("Gold");
    }

    void Update()
    {
        if (GameManager.Instance.isPlay && !GameManager.Instance.isPause)
        {
            AddScore();            
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore()
    {
        if (timeAdd > 0)
        {
            timeAdd -= Time.deltaTime;
        }
        else
        {
            timeAdd = 1;
            score++;
            GameManager.Instance.ChangeBackground();
            overScore.text = "Distance: " + score + "m";
            playScore.text = "Distance: " + score + "m";
        }
    }

    public void BestScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        overHightScore.text = "Best: " + PlayerPrefs.GetInt("HighScore") + "m";
        PlayerPrefs.Save();
    }

    public void AddGold()
    {
        gold += 5;
        playGold.text = "" + gold;
        PlayerPrefs.SetInt("Gold", gold);
        //GameManager.Instance.ChangeBackground();
    }

    public void ShowGold()
    {
        playGold.text = "" + gold;
        shopGold.text = "" + gold;
    }
}
