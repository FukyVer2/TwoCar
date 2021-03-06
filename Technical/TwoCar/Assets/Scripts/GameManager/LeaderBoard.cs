﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard instance;
    private const string LEADERBOARD_SCORE = PlayGameServices.leaderboard_highscore;
    // Use this for initialization
    //public Text valid;
    public Text rank;
    public Text value;
    public Text id;
    public Text count;
    public Image background;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start ()
    {
        PlayGamesPlatform.Activate();
        SignIn();

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnLevelWasLoaded()
    {
        
    }

    public void ConnectOrDisConnectOnGooglePlay()
    {
        Social.localUser.Authenticate(b => { });
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
                
        }
        else
        {
            Social.localUser.Authenticate(b => { });
            Debug.Log("dang nhap play services");

        }
    }

    public void SignIn()
    {
        Social.localUser.Authenticate(b => { });
    }

    public void OpenLeaderBoardScore()
    {
        if (Social.localUser.authenticated)
        {
            
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD_SCORE);
            Debug.Log("dang nhap play services");

        }
    }

    public void GetPlayerRank()
    {
        PlayGamesPlatform.Instance.LoadScores(LEADERBOARD_SCORE, scores =>
        {
            if (scores != null)
            {
                background.color = Color.blue;
                foreach (IScore sc in scores)
                {
                    if (sc.userID.Equals(PlayGamesPlatform.Instance.localUser.id))
                    {
                        value.text = "" + sc.value;
                    }
                }
            }
            else
            {
                background.color = Color.red;
            }
        } );
    }

    public void ReportScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, LEADERBOARD_SCORE, b => {});
        }
    }

    public void ShowRank()
    {
        GetPlayerRank();
    }
}
