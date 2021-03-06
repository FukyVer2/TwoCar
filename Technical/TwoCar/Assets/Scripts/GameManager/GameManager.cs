﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject play; // Canvas playground
    public GameObject start;
    public GameObject over;
    public GameObject pause;
    public GameObject countdown;
    public GameObject playObj;
    public GameObject garage;
    public GameObject continuePlay;
    public GameObject buyGold;
    public GameObject notEnough;
    public GameObject audioGameObject;

    public int highScore;
    public Text textScore;
    public Text textBestScore;
    public Text playScore;
    public int buyBack;

    public float speed = 5;     // Value to spawn Enemy
    public float velo = 0.01f;
    public float minDelay = 0.6f;
    public float maxDelay = 0.8f;
    public int maxSpeed = 9;
    public int enemyCount = 0;

    public bool isPause = false;  // Pause game

    public SpriteRenderer background;
    public Color randColor;
    public Color previousBackgroundColor;
    public float t = 0; // time to change backgroudn color.

    public float dieDelay = 0;
    public bool isDie = false;
    public bool isPlay = false;
    public bool isContinue = true;
    public float continueDelay = 2;
    public Image continueImage;

    public Text timer;
    public float countDown = 3;
    public bool isCountDown = false;

    public bool music = true;
    public float volume = 1;
    public Sprite soundOn;
    public Sprite soundOff;
    public Image audioButton;


    private float _bkSpeed;
    private float _bkMinDelay;
    private float _bkMaxDelay;

    public ScoreManager scoreManager;
    public List<EnemySpawner> enemySpawner;

    public GameObject freeContinue;
    public GameObject earnDiamond;

    public Color startColor = Color.white; //new Color(11/255f,11/255f,11/255f,30/255f);
    public string unityAdsID;

	void Start ()
	{
	    highScore = PlayerPrefs.GetInt("Best Score");
        previousBackgroundColor = background.color;
	    StartScene();
        Reset();
	    for (int i = 0; i < enemySpawner.Count; i++)
	    {
	        enemySpawner[i].SetDelay(minDelay, maxDelay);
	    }
#if UNITY_ANDROID || UNITY_IOS
        ChartboostAndroid.Instance.RequestInterstitial(ChartboostSDK.CBLocation.Default);
        Advertisement.Initialize(unityAdsID);
#elif UNITY_WP8
        GoogleAdmobPlugin_WP8.Instance.RequestInterstitial();
#endif
    }

	// Update is called once per frame
	void Update ()
	{
	    if (!Advertisement.isShowing && Time.timeScale == 0)
	    {
	        Time.timeScale = 1;

	    }
	    ChangeBackgroundColor();
	    if (isDie)
	    {
            Die();
	    }
	    if (isCountDown)
	    {
            Resume();
	    }
	    if (isPlay && !isPause)
	    {
            scoreManager.AddScore();
            for (int i = 0; i < enemySpawner.Count; i++)
            {
                enemySpawner[i].Spawn(minDelay, maxDelay);
            }
        }
	}

    public void Reset()
    {
        buyBack = 1;
        background.color = Color.white; // new Color(117f/255f, 170f/255f, 160f/255f, 1f);
        previousBackgroundColor = Color.white;//new Color(117f / 255f, 170f / 255f, 160f / 255f, 1f);
        randColor = Color.white;//new Color(117f / 255f, 170f / 255f, 160f / 255f, 1f);
        velo = 0.02f;
        speed = 4f;
        minDelay = 0.7f;
        maxDelay = 0.9f;
        continueDelay = 2f;
        ScoreManager.Instance.ShowGold();
		enemyCount = 0;
    }

    public void GameOver()
    {
#if UNITY_ANDROID
        MyApplycation.Instance.googleAnalytics.LogEvent("GameOver", ScoreManager.Instance.score.ToString(), "", (int)Time.fixedTime);
        Lead.instance.ReportScore(PlayerPrefs.GetInt("HighScore"));
        Lead.instance.GetRank();
        if (Random.Range(0, 3) == 2)
        {
            Invoke("ShowAdmob", 0.2f);
        }
#elif UNITY_WP8
                GoogleAdmobPlugin_WP8.Instance.ShowInterstitial();
#endif
        if (Advertisement.IsReady())
        {
            earnDiamond.SetActive(true);
        }
        else
        {
            earnDiamond.SetActive(false);
        }
        isPlay = false;
        //AudioManager.Instance.StopBackground();
        ScoreManager.Instance.BestScore();
        over.SetActive(true);
        play.SetActive(false);
        start.SetActive(false);
        playObj.SetActive(false);
        pause.SetActive(false);
        garage.SetActive(false);
        buyGold.SetActive(false);
        garage.SetActive(false);

    }

    public void ShowAdmob()
    {
#if UNITY_ANDROID || UNITY_IOS
        ChartboostAndroid.Instance.ShowInterstitial(ChartboostSDK.CBLocation.Default);
#endif
    }

    public void StartScene()
    {
        isPlay = false;
        Unpause();
        PoolObject.DespawnAll("Enemy");
        PoolObject.DespawnAll("Effect");
        //.Instance.StopBackground();
        //AudioManager.Instance.Background();
        start.SetActive(true);
        play.SetActive(false);
        over.SetActive(false);
        playObj.SetActive(false);
        pause.SetActive(false);
        buyGold.SetActive(false);
        garage.SetActive(false);

    }

    public void PlayScene()
    {
#if UNITY_ANDROID || UNITY_IOS
        MyApplycation.Instance.googleAnalytics.LogEvent("GamePlay", "PlayGame", "", (int)Time.fixedTime);
#elif UNITY_WP8
        //GoogleAdmobPlugin_WP8.Instance.ShowBanner();
#endif
        ScoreManager.Instance.ShowGold();
        isPlay = true;
        ScoreManager.Instance.ResetScore();
        playScore.text = "Distance: " + ScoreManager.Instance.score + "m";
        Reset();
        PoolObject.DespawnAll("Enemy");
        PoolObject.DespawnAll("Effect");
        //AudioManager.Instance.StopBackground();
        //AudioManager.Instance.Background();
        play.SetActive(true);
        over.SetActive(false);
        start.SetActive(false);
        pause.SetActive(false);
        playObj.SetActive(true);
        buyGold.SetActive(false);
        garage.SetActive(false);
    }

    public void CarShop()
    {
#if UNITY_ANDROID || UNITY_IOS
        MyApplycation.Instance.googleAnalytics.LogEvent("Shop", "Shop", "", (int)Time.fixedTime);
#endif
        ScoreManager.Instance.ShowGold();
        garage.SetActive(true);
    }



    public void CloseShop()
    {
        garage.SetActive(false);
    }

    public void GoldShop()
    {
        ScoreManager.Instance.ShowGold();
        buyGold.SetActive(true);
        garage.SetActive(false);
    }

    public void CloseGoldShop()
    {
        buyGold.SetActive(false);
        garage.SetActive(true);
    }

    public void ChangeBackground()
    {
            previousBackgroundColor = background.color;
            RandomBackgroundColor();
    }


    public void BackUp()
    {
        _bkSpeed = speed;
        _bkMinDelay = minDelay;
        _bkMaxDelay = maxDelay;
    }

    public void Pause()
    {
        BackUp();
        countDown = 3;
        speed = 0;
        minDelay = 0;
        maxDelay = 0;
        isPause = true;
    }

    public void Unpause()
    {
        countDown = 0;
        speed = _bkSpeed;
        minDelay = _bkMinDelay;
        maxDelay = _bkMaxDelay;
        isPause = false;
    }

    public void Replay()
    {
        MyApplycation.Instance.googleAnalytics.LogEvent("GamePlay", "ReplayGame", "", (int)Time.fixedTime);
        pause.SetActive(false);
        Unpause();
        PlayScene();
    }

    public void RandomBackgroundColor()
    {
        float red = (float)Random.Range(40, 210) / 255;
        float green = (float)Random.Range(40, 210) / 255;
        float blue = (float)Random.Range(40, 210) / 255;
        randColor = new Color(red, green, blue, 1f);
        Debug.Log(randColor);
    }

    void ChangeBackgroundColor()
    {
        if (background.color != randColor)
        {
            background.color = Color.Lerp(previousBackgroundColor, randColor, t);
            t += Time.deltaTime/2;
        }
        else
        {
            previousBackgroundColor = background.color;
            t = 0;
        }
    }

    public void Die()
    {
        if (!isPause)
        {
            Pause();
            dieDelay = 1.5f;
        }
        if (dieDelay > 0)
        {
            dieDelay -= Time.deltaTime;
        }
        else
        {
            if (Advertisement.IsReady() && buyBack < 5)
            {
                freeContinue.SetActive(true);
                Debug.Log("checked");
            }
            else
            {
                freeContinue.SetActive(false);
                Debug.Log("checked");
            }
            continuePlay.SetActive(true);
            ContinuePlay();
        }
    }

    public void PauseScene()
    {

        if (!isPause)
        {
            Pause();
            pause.SetActive(true);
        }
        else
        {
            Unpause();
            pause.SetActive(false);
        }
    }

    public void Resume()
    {
        if (countDown > 0)
        {
            countdown.SetActive(true);
            continuePlay.SetActive(false);
            pause.SetActive(false);
            countDown -= Time.deltaTime;
            int t = Mathf.CeilToInt(countDown);
            timer.text = "" + t;
        }
        else
        {
            isCountDown = false;
            countdown.SetActive(false);
            PauseScene();
        }
    }

    public void StartCd()
    {
        Debug.Log(_bkSpeed);
        Debug.Log(_bkMaxDelay);
        Debug.Log(_bkMinDelay);
        isCountDown = true;
        countDown = 3;
    }

    public void SoundControl()
    {
        if (music)
        {
            volume = 0;
            audioButton.sprite = soundOff;
            audioGameObject.SetActive(false);
            music = false;
            AudioManager.Instance.isSoundGamePlay = false;
        }
        else
        {
            volume = 1;
            audioButton.sprite = soundOn;
            audioGameObject.SetActive(true);
            music = true;
            AudioManager.Instance.isSoundGamePlay = true;
        }
    }

    public void ChangeSpeed()
    {
        enemyCount++;
        if (enemyCount % 100 == 0 && enemyCount != 0)
        {
            if (velo > 0)
            {
                velo *= -1/3f;
            }
            else
            {
                velo *= -3f;
            }
        }
        if (speed < maxSpeed/* || velo < 0*/)
        {
            speed += velo;
            minDelay -= velo/10;
            maxDelay -= velo/10;
        }
    }

    public void ContinuePlay()
    {
        if (isContinue && continueDelay > 0)
        {
            continueDelay -= Time.deltaTime;
            continueImage.fillAmount += Time.deltaTime/2;
        }
        else if (!isContinue)
        {
            isDie = false;
            StartCd();
            continueDelay = 2f;
            continueImage.fillAmount = 0;
            isContinue = true;
        }
        else
        {
            isDie = false;
            continueDelay = 2f;
            continuePlay.SetActive(false);
            continueImage.fillAmount = 0;
            Unpause();
            PoolObject.DespawnAll("Enemy");
            PoolObject.DespawnAll("Effect");
            GameOver();
        }
    }

    public void ContinueButton()
    {
        if (ScoreManager.Instance.diamond >= buyBack)
        {
            ScoreManager.Instance.ShowGold();
            ScoreManager.Instance.AddDiamon(-buyBack);
            buyBack +=2;
            ScoreManager.Instance.ShowGold();
            isContinue = false;
            ClearObstacles();
        }
    }

    public void ClearObstacles()
    {
        PoolObject.DespawnAll("Enemy");
    }

    public void ShowNotEnough()
    {
        notEnough.SetActive(true);
    }

    public void HideNotEnough()
    {
        notEnough.SetActive(false);
    }

    public void Rate()
    {
#if UNITY_ANDROID || UNITY_IOS
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Fuky.TapRacing");
#endif
    }

    public void FreeContinue()
    {
#if UNITY_ANDROID || UNITY_IOS
        Advertisement.Show("rewardedVideo");
        ScoreManager.Instance.AddDiamon(buyBack);
        Time.timeScale = 0;
        ContinueButton();
        ClearObstacles();
#endif
    }

    public void ShowReward()
    {
#if UNITY_ANDROID || UNITY_IOS
        Advertisement.Show("rewardedVideo");
        ScoreManager.Instance.AddDiamon(5);
#endif
    }
}
