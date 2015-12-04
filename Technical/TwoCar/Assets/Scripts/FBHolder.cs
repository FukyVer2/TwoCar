
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FBHolder : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IOS
    void Start () {
	}	


	void Awake()
	{
		FB.Init (SetInit, onHideUnity);
	}

	private void SetInit()
	{
		Debug.Log("FB Init done");
		if (FB.IsLoggedIn) //logged in OK
		{
            Debug.Log("FB is Logged in");
		}
		else
		{
		}
	}

	private void onHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	//public void FBLogin()
	public void FBLogin()
	{
		FB.Login ("email,publish_actions", AuthCallBack);
	}

	void AuthCallBack(FBResult result)
	{
		if (FB.IsLoggedIn)
		{
            Debug.Log("FB login worked");
            FBfeed();
        }
		else
		{
            Debug.Log("FB login Failed");
		}
	}

    public void ShareWithFriends()
    {

	    //check login or not yet:
	    if (FB.IsLoggedIn)
	    {
                Debug.Log("logged in OK -> goto share to FB");
			    FBfeed();
	            //StartCoroutine(TakeScreenshot1());
	    }
	    else //not login yet, so go to login and share
	    {
                Debug.Log("not login yet, so go to login and share");
			    FBLogin();
	    }
    }

	void FBfeed()
	{
		FB.Feed(
			linkCaption: "I'm playing this awesome game",
			//picture: "http://robovina.com/uploads/news/2015_03/autobase1.png",
			linkName: "LET'S RACE",
            link: "https://play.google.com/store/apps/details?id=com.Fuky.Cars"
            //link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "Guest")
            //link: "https://play.google.com/store/apps/details?id=den.pin",
            //mediaSource: "https://www.youtube.com/watch?v=dAXW1R_aoz4"

            );
	}
    //private IEnumerator TakeScreenshot1()
    //{
    //    yield return new WaitForEndOfFrame();
    //    int width = Screen.width;
    //    int height = Screen.height;
    //    Texture2D scren = new Texture2D(width, height, TextureFormat.RGB24, false);

    //    // Read screen contents into the texture
    //    scren.ReadPixels(new Rect(0, 0, width, height), 0, 0);

    //    scren.Apply();
    //    byte[] screenshot = scren.EncodeToPNG();
    //    //lấy chuỗi byte của hình để post lên facebook

    //    var wwwForm = new WWWForm();
    //    wwwForm.AddBinaryData("image", screenshot, "barcrawling.png");

    //    wwwForm.AddField("name", "hehe");
    //    FB.API("me/photos", Facebook.HttpMethod.POST, PostPicCallback, wwwForm);//post
    //}
    //void PostPicCallback(FBResult result)
    //{
    //    if (result.Error != null)
    //    {
    //        Debug.LogWarning("FacebookManager-publishActionCallback: error: " + result.Error);
    //    }
    //    else
    //    {
    //        Debug.Log("FacebookManager-publishActionCallback: success: " + result.Text);
    //    }
    //}
#endif
}

