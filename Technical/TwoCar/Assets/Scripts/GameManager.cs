using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int highScore;
    public Text textScore;
    public Text textBestScore;
    public Text playScore;
    public int buyBack;

    public float speed = 5;     // Value to spawn Enemy
    public float velo = 0.01f;
    public float minDelay = 0.6f;
    public float maxDelay = 0.8f;
    public int maxSpeed = 10;
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
    public GameObject audioButton;


    private float _bkSpeed;
    private float _bkMinDelay;
    private float _bkMaxDelay;

    public ScoreManager scoreManager;
    public List<EnemySpawner> enemySpawner;

	void Start ()
	{
	    highScore = PlayerPrefs.GetInt("Best Score");
        previousBackgroundColor = background.color;
	    StartScene();
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
	{
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
	        foreach (var spawner in enemySpawner)
	        {
	            spawner.Spawn();
	        }
	    }
	}

    public void Reset()
    {
        buyBack = 1;
        background.color = new Color(117f/255f, 170f/255f, 160f/255f, 1f);
        previousBackgroundColor = new Color(117f / 255f, 170f / 255f, 160f / 255f, 1f);
        randColor = new Color(117f / 255f, 170f / 255f, 160f / 255f, 1f);
        velo = 0.02f;
        speed = 4f;
        minDelay = 0.65f;
        maxDelay = 0.85f;
        continueDelay = 2f;
        ScoreManager.Instance.ShowGold();
    }

    public void GameOver()
    {
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
        pause.SetActive(false);
        Unpause();
        PlayScene();
    }

    public void RandomBackgroundColor()
    {
        float red =(float)Random.Range(40, 210) / 255;
        float green = (float)Random.Range(40, 210) / 255;
        float blue = (float)Random.Range(40, 210) / 255;
        randColor = new Color(red,green,blue,1f);
        Debug.Log(randColor);
    }

    void ChangeBackgroundColor()
    {
        if (background.color != randColor)
        {
            background.color = Color.Lerp(previousBackgroundColor, randColor, t);
            Debug.Log("debug");
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

    //public void SoundControl()
    //{
    //    if (music)
    //    {
    //        volume = 0;
    //        AudioManager.Instance.Background();
    //        audioButton.GetComponent<Image>().sprite = soundOff;
    //        music = false;
    //    }
    //    else
    //    {
    //        volume = 1;
    //        AudioManager.Instance.Background();
    //        audioButton.GetComponent<Image>().sprite = soundOn;
    //        music = true;
    //    }
    //}

    public void ChangeSpeed()
    {
        enemyCount++;
        if (enemyCount % 130 == 0 && enemyCount != 0)
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
        if(speed<maxSpeed || velo < 0)
        speed += velo;
        minDelay -= velo / 9;
        maxDelay -= velo / 9;
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
        }
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
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.fuky.twocar");
    }
}
