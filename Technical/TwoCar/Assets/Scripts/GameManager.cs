using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject play;
    public GameObject start;
    public GameObject over;
    public GameObject pause;
    public GameObject playObj;
    public List<GameObject> listGameObjects;
    public int highScore;
    public int score = 0;
    public Text textScore;
    public Text textBestScore;
    public Text playScore;
    public float speed;
    public float minDelay;
    public float maxDelay;
    public bool isPause = false;
    public AudioClip backgroundSound;

    private float _bkSpeed;
    private float _bkMinDelay;
    private float _bkMaxDelay;

	void Start ()
	{
	    highScore = PlayerPrefs.GetInt("Best Score");
        StartScene();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void GameOver()
    {
        AudioManager.Instance.StopBackground();
        BestScore();
        over.SetActive(true);
        play.SetActive(false);
        start.SetActive(false);
        playObj.SetActive(false);
    }

    public void StartScene()
    {
        AudioManager.Instance.StopBackground();
        AudioManager.Instance.Background();
        start.SetActive(true);
        play.SetActive(false);
        over.SetActive(false);
        playObj.SetActive(false);
    }

    public void PlayScene()
    {
        score = 0;
        playScore.text = "Score: " + score;
        ResetSpeed();
        ClearListGameObject();
        AudioManager.Instance.StopBackground();
        AudioManager.Instance.Background();
        play.SetActive(true);
        over.SetActive(false);
        start.SetActive(false);
        playObj.SetActive(true);
    }

    public void AddGameObject(GameObject obj)
    {
        if (!listGameObjects.Contains(obj))
            listGameObjects.Add(obj);
    }

    public void RemoveGameObject(GameObject obj)
    {
        if (listGameObjects.Contains(obj))
        {
            listGameObjects.Remove(obj);
        }
    }

    public void ClearListGameObject()
    {
        foreach (GameObject obj in listGameObjects)
        {
            Destroy(obj);
        }
        listGameObjects.Clear();
    }

    public void AddScore()
    {
        score++;
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

    public void ResetSpeed()
    {
        speed = 5;
        minDelay = 0.6f;
        maxDelay = 1f;
    }

    public void RemoveObj(GameObject obj)
    {
        listGameObjects.Remove(obj);
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
        pause.SetActive(true);
        speed = 0;
        minDelay = 0;
        maxDelay = 0;
        isPause = true;
    }

    public void Unpause()
    {
        pause.SetActive(false);
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
}
