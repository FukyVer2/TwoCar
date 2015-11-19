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


    public Text id;
    public Text count;
    public Text playerOne;
    public Text allCount;
    public Text rank;
    public Text score;
    public Text isValue;
    public Text fuck;
    


	// Use this for initialization
	void Start () {
        PlayGamesPlatform.Activate();
       
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

    public Action<string> cao = a=> { string c = a; };

    public void sang(string _quang)
    {
        Debug.Log(_quang + "1");
    }

    public void quang(string _quang)
    {
        Debug.Log(_quang + "2");
    }

    [ContextMenu("test action")]
    public void cao1()
    {
        int a = UnityEngine.Random.Range(0, 100);
        if (a%2 == 0)
            cao = quang;
        else
        {
            cao = sang;
        }
        cao("Blala");
        sang1(1, quang);
        sang1(1, a1 => { Debug.Log(a1 + "23"); });
    }

    public void sang1(int b, Action<string> _xyz )
    {
        //delay 20s 
        _xyz("sang");
    }

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
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.TopScores,
            10, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
            data =>
            {
                if (data.Valid)
                {
                    id.text = data.Id.ToString();
                }
                else
                {
                    id.text = "DEO GET Dc";
                }
            });
    }



    public void GetAllCount()
    {
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.TopScores,
           10, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
           data =>
           {
               if (data.Valid)
               {
                   if (data.ApproximateCount == 0)
                   {
                       allCount.text = "Deo Co all count" + data.ApproximateCount.ToString();
                   }
                   else
                   {
                       allCount.text = data.ApproximateCount.ToString();
                   }
               }
               else
               {
                   allCount.text = "DEO GET Dc all count";
               }
           });
    }

    public void GetPlayerScore()
    {
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.TopScores,
          10, LeaderboardCollection.Social, LeaderboardTimeSpan.AllTime,
          data =>
          {
              if (data.Valid)
              {
                  if (data.PlayerScore != null)
                  {
                      playerOne.text = "Player One";
                      string userID = data.PlayerScore.userID.ToString();
                      string valuePlayer = data.PlayerScore.value.ToString();
                      string leadId = data.PlayerScore.leaderboardID.ToString();
                      playerOne.text = userID + "-" + valuePlayer + "-" + leadId;
                  }
                  else
                  {
                      playerOne.text = "PlayerScore = null";
                  }
              }
              else
              {
                  playerOne.text = "DEO GET Dc Player Score";
              }
          });
    }
    public void GetCount()
    {
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.TopScores,
           10, LeaderboardCollection.Social, LeaderboardTimeSpan.AllTime,
           data =>
           {
               if (data.Valid)
               {
                   if (data.Scores != null)
                   {

                       if (data.Scores.Length == 0)
                       {
                           count.text = "Deo Co count" + data.Scores.Length.ToString();
                       }
                       else
                       {
                           count.text = data.Scores.Length.ToString();
                           playerOne.text = data.Scores[0].value.ToString();

                       }
                   }
                   else
                   {
                       count.text = "Count = Null " + data.Scores.ToString();
                   }
               }
               else
               {
                   count.text = "DEO GET Dc count";
               }
           });
    }

    public void GetRank()
    {
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.TopScores,
            10, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
            data =>
            {
                if (data.Valid)
                {
                    
                }
                else
                {
                    id.text = "DEO GET Dc";
                }
            });
    }

    public void GetStatus()
    {
        
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, LeaderboardStart.PlayerCentered,
            1, LeaderboardCollection.Social, LeaderboardTimeSpan.AllTime,
            data =>
            {
                if (data.Valid)
                {
                    isValue.text = data.Status.ToString();
                    fuck.text = data.Title;
                    rank.text = data.PlayerScore.userID;
                }
                else
                {
                    isValue.text = "DEO GET Dc";
                }
            });
    }

    public void GetScore()
    {
        PlayGamesPlatform.Instance.LoadScores(PlayGameServices.leaderboard_highscore, (
        (scores) => {
            if (scores != null)
            {
                if (scores.Length != 0)
                {
                    count.text = scores.Length.ToString();
                    string userID = scores[0].userID.ToString();
                    string valuePlayer = scores[0].value.ToString();
                    string leadId = scores[0].leaderboardID.ToString();
                    score.text = userID + "-" + valuePlayer + "-" + leadId;
                    
                }
                else
                {
                    score.text = "Scores = null " + scores.ToString();
                }
                //Debug.Log("scoreLength : " + scores.Length);
                //foreach (IScore sc in scores)
                //{
                //    if (sc.userID.Equals(PlayGamesPlatform.Instance.localUser.id))
                //    {
                //        Debug.Log("mi score : " + sc.value);
                //    }
                //}
            }
        }));
    }
    public void GetIsValue()
    { }
    public  void GetFuck()
    { }
}
