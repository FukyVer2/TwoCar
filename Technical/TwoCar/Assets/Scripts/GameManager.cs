using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject play; // Canvas playground
    public GameObject start;
    public GameObject over;
    public GameObject pause;
    public GameObject playObj;
    public List<GameObject> listGameObjects; // List of enemy on scene
    public int highScore;
    public int score = 0;
    public Text textScore;
    public Text textBestScore;
    public Text playScore;
    public float speed;     // Value to spawn Enemy
    public float minDelay;
    public float maxDelay;
    public bool isPause = false;  // Pause game
    public AudioClip backgroundSound; //Audio
    public Image background;
    public Color randColor;
    public Color previousBackgroundColor;
    public float t = 0;

    private float _bkSpeed;
    private float _bkMinDelay;
    private float _bkMaxDelay;

	void Start ()
	{
	    highScore = PlayerPrefs.GetInt("Best Score");
        previousBackgroundColor = background.color;
	    //randColor = previousBackgroundColor;
        Debug.Log(randColor);
	    StartScene();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ChangeBackgroundColor();
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
        if (score%10 == 0 && score !=0)
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
        //pause.SetActive(true);
        speed = 0;
        minDelay = 0;
        maxDelay = 0;
        isPause = true;
    }

    public void Unpause()
    {
        //pause.SetActive(false);
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
        float red =(float)Random.Range(80, 180) / 255;
        float green = (float)Random.Range(80, 180) / 255;
        float blue = (float)Random.Range(80, 180) / 255;
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
}
