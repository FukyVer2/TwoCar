using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Lead : MonoBehaviour
{
    public static Lead instance;

    public Text id;
    public Text count;
    public Text playerOne;
    public Text allCount;
    public Text rank;
    public Text score;
    public Text isValue;
    public Text fuck;

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

    // Use this for initialization
    void Start () {
        PlayGamesPlatform.Activate();
        SignIn();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SignIn()
    {
        Social.localUser.Authenticate(b =>
        {
            
        });
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
            Social.localUser.Authenticate(b =>
            {
                
            });

        }
    }

    //public void OpenLeaderBoardScore()
    //{
    //    if (Social.localUser.authenticated)
    //    {

    //        PlayGamesPlatform.Instance.ShowLeaderboardUI(PlayGameServices.leaderboard_highscore);

    //    }
    //}

    //public Action<string> cao = a=> { string c = a; };

    //public void sang(string _quang)
    //{
    //    Debug.Log(_quang + "1");
    //}

    //public void quang(string _quang)
    //{
    //    Debug.Log(_quang + "2");
    //}

    //[ContextMenu("test action")]
    //public void cao1()
    //{
    //    int a = UnityEngine.Random.Range(0, 100);
    //    if (a%2 == 0)
    //        cao = quang;
    //    else
    //    {
    //        cao = sang;
    //    }
    //    cao("Blala");
    //    sang1(1, quang);
    //    sang1(1, a1 => { Debug.Log(a1 + "23"); });
    //}

    //public void sang1(int b, Action<string> _xyz )
    //{
    //    //delay 20s 
    //    _xyz("sang");
    //}

    public void ReportScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, PlayGameServices.leaderboard_highscore, b => { });
        }
    }

    public void Logout()
    {
        PlayGamesPlatform.Instance.SignOut();

    }

    public void ShowLeaderBoard()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(PlayGameServices.leaderboard_highscore);
        }
    }

    public void GetId()
    {
    }



    public void GetAllCount()
    {

    }

    public void GetPlayerScore()
    {

    }
    public void GetCount()
    {

    }

    public void GetRank()
    {
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.TopScores,
            10, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
            data =>
            {
                if (data.Valid)
                {
                    rank.text = "Rank: " + data.PlayerScore.rank;
                }
                else
                {
                    rank.text = "Rank: ***";
                }
            });
    }

    public void GetStatus()
    {
        
       
    }

    public void GetScore()
    {
        
    }
    public void GetIsValue()
    { }
    public  void GetFuck()
    { }
}
