﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    public int highScore;
    public int score = 0;
    public Text textScore;
    public Text textBestScore;
    public Text playScore;

    public float speed = 5;     // Value to spawn Enemy
    public float velo = 0.01f;
    public float minDelay = 0.6f;
    public float maxDelay = 0.8f;
    public int enemyCount = 0;

    public bool isPause = false;  // Pause game

    public Image background;
    public Color randColor;
    public Color previousBackgroundColor;
    public float t = 0; // time to change backgroudn color.

    public float dieDelay = 0;
    public bool isDie = false;

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

	void Start ()
	{
	    highScore = PlayerPrefs.GetInt("Best Score");
        previousBackgroundColor = background.color;
        Debug.Log(randColor);
	    StartScene();
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
	}

    public void GameOver()
    {
        AudioManager.Instance.StopBackground();
        BestScore();
        over.SetActive(true);
        play.SetActive(false);
        start.SetActive(false);
        playObj.SetActive(false);
        pause.SetActive(false);
        garage.SetActive(false);

    }

    public void StartScene()
    {
        Unpause();
        PoolObject.DespawnAll("Enemy");
        PoolObject.DespawnAll("Effect");
        AudioManager.Instance.StopBackground();
        AudioManager.Instance.Background();
        start.SetActive(true);
        play.SetActive(false);
        over.SetActive(false);
        playObj.SetActive(false);
        pause.SetActive(false);
        garage.SetActive(false);
    }

    public void PlayScene()
    {
        score = 0;
        playScore.text = "Score: " + score;
        Reset();
        //ClearListGameObject();
        PoolObject.DespawnAll("Enemy");
        PoolObject.DespawnAll("Effect");
        AudioManager.Instance.StopBackground();
        AudioManager.Instance.Background();
        play.SetActive(true);
        over.SetActive(false);
        start.SetActive(false);
        pause.SetActive(false);
        playObj.SetActive(true);
        garage.SetActive(false);

    }

    public void CarShop()
    {
        garage.SetActive(true);
        start.SetActive(false);
        play.SetActive(false);
        over.SetActive(false);
        playObj.SetActive(false);
        pause.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        if (score%50 == 0 && score !=0)
        {
            previousBackgroundColor = background.color;
            Debug.Log("change color");
            RandomBackgroundColor();
        }
        textScore.text = "Score: " + score;
        playScore.text = "Score: " + score;
    }

    public void BestScore()
    {
        if (score > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", score);
        }
        textBestScore.text = "Best: " + PlayerPrefs.GetInt("Best Score");
        PlayerPrefs.Save();
    }

    public int GetScore()
    {
        return score;
    }

    public void Reset()
    {
        background.color = Color.white;
        previousBackgroundColor = Color.white;
        randColor = Color.white;
        velo = 0.01f;
        speed = 5;
        minDelay = 0.6f;
        maxDelay = 0.8f;
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
            t += Time.deltaTime/2;
            Debug.Log(background.color);
        }
        else
        {
            previousBackgroundColor = background.color;
            t = 0;
        }
    }

    public void Die()
    {
        if (dieDelay > 0)
        {
            Pause();
            dieDelay -= Time.deltaTime;
        }
        else
        {
            isDie = false;
            Unpause();
            //ClearListGameObject();
            PoolObject.DespawnAll("Enemy");
            PoolObject.DespawnAll("Effect");
            GameOver();
        }
    }

    public void PauseScene()
    {
        if (!isPause)
        {
            Pause();
            pause.SetActive(isPause);
        }
        else
        {
            Unpause();
            pause.SetActive(isPause);
        }
    }

    public void Resume()
    {
        if (countDown > 0)
        {
            countdown.SetActive(true);
            pause.SetActive(false);
            countDown -= Time.deltaTime;
            int t = Mathf.CeilToInt(countDown);
            timer.text = ""+t;
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
        isCountDown = true;
    }

    public void SoundControl()
    {
        if (music)
        {
            volume = 0;
            AudioManager.Instance.Background();
            audioButton.GetComponent<Image>().sprite = soundOff;
            music = false;
        }
        else
        {
            volume = 1;
            AudioManager.Instance.Background();
            audioButton.GetComponent<Image>().sprite = soundOn;
            music = true;
        }
    }
}
