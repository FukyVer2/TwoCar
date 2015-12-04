using UnityEngine;
using System.Collections;

public class AdsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void btShowAds()
    {
        GoogleAdmobPlugin_WP8.Instance.ShowBanner();
    }


    public void btHideAds()
    {
        GoogleAdmobPlugin_WP8.Instance.HideBanner();
    }

    public void btShowFullAds()
    {
        GoogleAdmobPlugin_WP8.Instance.ShowInterstitial();
    }


    public void btRequestAds()
    {
        GoogleAdmobPlugin_WP8.Instance.RequestInterstitial();
    }
}
