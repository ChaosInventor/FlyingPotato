using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

    public float AdsPerGame;

    public string AndroidAppId;
    public string iPhoneAppId;

    private RewardBasedVideoAd VideoAd;

    private GuiManager GUIM;

    // Use this for initialization
    void Awake ()
    {

        GUIM = GameObject.Find("GUI manager").GetComponent<GuiManager>();

        VideoAd = RewardBasedVideoAd.Instance;

        LoadAd();

        VideoAd.OnAdRewarded += Reward;
        VideoAd.OnAdFailedToLoad += FailToLoadAd;
        VideoAd.OnAdOpening += OpenAd;
        VideoAd.OnAdClosed += AdClose;

    }

    public void LoadAd()
    {

#if UNITY_ANDROID
            string adUnitId = AndroidAppId;
#elif UNITY_IPHONE
            string adUnitId = iPhoneAppId;
#else
        string adUnitId = "Invlaid platform for ads";
#endif

        AdRequest Request = new AdRequest.Builder().Build();

        VideoAd.LoadAd(Request, adUnitId);

    }

    public void StartVideoAd()
    {

        if (VideoAd.IsLoaded())
        {
            VideoAd.Show();
        }else
        {
            LoadAd();
        }

    }

    private void Reward(object sender, Reward args)
    {

        PlayerControler Player = GameObject.Find("Player").GetComponent<PlayerControler>();
        Player.Revive();
        Player.StartImmune();

        AudioManager Audio = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        Audio.StopPlaying();
        AdsPerGame -= 1f;

    }

    private void FailToLoadAd(object sender, AdFailedToLoadEventArgs args)
    {
        
        GUIM.Revive.GetComponentInChildren<UnityEngine.UI.Text>().text = "Failed to load ad: " + args;

        Debug.Log(args);

    }

    private void OpenAd(object sender, EventArgs args)
    {

        AudioManager Audio = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        Audio.Mute();

    }

    private void AdClose(object sender, EventArgs args)
    {

        AudioManager Audio = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        Audio.UnMute();

        LoadAd();

    }

}
