using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public int highScore;
    public int score;
    public int gold;
    public int diamond;
    public float timeAdd = 1f;
    public int totalGold;
    public List<Text> listPriceGold;
    public List<Text> listValueDia;
    public List<Button> listBuyButtons;
    public GameObject notification;

    public Text playScore;
    public Text overScore;
    public Text overHightScore;

    public Text playGold;
    public Text shopGold;
    public Text playDiamon;
    public Text shopDiamon;
    public Text countDownDia;
    public Text buyGoldText;
    public Text buyDiaText;

    public int[] priceGold;
    public int[] valueDia;

    public int preIndex;

    void Start()
    {
        priceGold = new[] {500, 5000, 55000, 600000};
        valueDia = new[] {1, 9, 49, 99};
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 3);
        }
        highScore = PlayerPrefs.GetInt("HighScore");
        gold = PlayerPrefs.GetInt("Gold");
        diamond = PlayerPrefs.GetInt("Diamond");
        InitGoldShop();
    }

    //void Update()
    //{
    //    if (GameManager.Instance.isPlay && !GameManager.Instance.isPause)
    //    {
    //        AddScore();            
    //    }
    //}

    void InitGoldShop()
    {
        for (int i = 0; i < listPriceGold.Count; i++)
        {
            listPriceGold[i].text = "" + valueDia[i];
            listValueDia[i].text = "" + priceGold[i];
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
            if (score%50 == 0 && score != 0)
            {
                GameManager.Instance.ChangeBackground();
            }
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
        gold += 3;
        playGold.text = "" + gold;
        PlayerPrefs.SetInt("Gold", gold);
        //GameManager.Instance.ChangeBackground();
    }

    public void ShowGold()
    {   
        playGold.text = "" + gold;
        shopGold.text = "" + gold;
        buyGoldText.text = "" + gold;

        playDiamon.text = "" + diamond;
        shopDiamon.text = "" + diamond;
        buyDiaText.text = "" + diamond;
        countDownDia.text = "x" + GameManager.Instance.buyBack;
    }

    public void AddDiamon(int value)
    {
        diamond+=value;
        ShowGold();
        PlayerPrefs.SetInt("Diamond", diamond);
    }

    public void BuyDiamond(Button clickedButton)
    {
        preIndex = listBuyButtons.IndexOf(clickedButton);
        if(diamond>=valueDia[preIndex])
        {
            notification.SetActive(true);
        }
        else
        {
            GameManager.Instance.ShowNotEnough();
        }
    }

    public void CloseNotify()
    {
        notification.SetActive(false);
    }

    public void Confirm()
    {
        gold += priceGold[preIndex];
        diamond -= valueDia[preIndex];
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Diamond", diamond);
        ShowGold();
        CloseNotify();
    }
}
