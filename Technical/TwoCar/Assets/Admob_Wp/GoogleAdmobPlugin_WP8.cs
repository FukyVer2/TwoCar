using UnityEngine;
using System.Collections;
using System;

public class GoogleAdmobPlugin_WP8 : MonoSingleton<GoogleAdmobPlugin_WP8> {

    public event Action WP_RequestInterstitial;
    public event Action WP_ShowInterstitial;
    public event Action WP_ShowBanner;
    public event Action WP_HideBanner;

    public void ShowBanner()
    {
        if (WP_ShowBanner != null)
        {
            WP_ShowBanner();
        }
    }

    public void HideBanner()
    {
        if (WP_HideBanner != null)
        {
            WP_HideBanner();
        }
    }
    public void RequestInterstitial()
    {
        if (WP_RequestInterstitial != null)
        {
            WP_RequestInterstitial();
        }
    }

    public void ShowInterstitial()
    {
        if(WP_ShowInterstitial != null)
        {
            WP_ShowInterstitial();
        }
    }
}
