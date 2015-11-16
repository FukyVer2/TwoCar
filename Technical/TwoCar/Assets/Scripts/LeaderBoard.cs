using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard instance;
    private const string LEADERBOARD_SCORE = "CgkIopiSk-sYEAIQAQ";
    // Use this for initialization

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

    public void OpenLeaderBoardScore()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD_SCORE);
            Debug.Log("dang nhap play services");

        }
    }

    void ReportScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(100, LEADERBOARD_SCORE, b => {});
        }
    }
}
