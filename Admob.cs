using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Admob : MonoBehaviour
{
    [SerializeField] private const string _bannerId = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private const string _interstitialId = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private const string _rewardedId = "ca-app-pub-3940256099942544/5224354917";

    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestInterstitial();
        RequestRewarded();
    }

    // BANNER

    private void RequestBanner()
    {
        _bannerView = new BannerView(_bannerId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        _bannerView.LoadAd(request);
    }

    // INTERSTITIAL

    private void RequestInterstitial()
    {
        _interstitialAd = new InterstitialAd(_interstitialId);

        _interstitialAd.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(request);
    }

    public void DisplayInterstitialAd()
    {
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        _interstitialAd.Destroy();

        RequestInterstitial();
    }

    // REWARDED

    private void RequestRewarded()
    {
        _rewardedAd = new RewardedAd(_rewardedId);

        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        _rewardedAd.LoadAd(request);
    }

    public void DisplayRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        Debug.Log("REWARD!!!");
    }

    public void HandleRewardedAdClosed(object sender,EventArgs args)
    {
        RequestRewarded();
    }

    private void OnDestroy()
    {
        _rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        _rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }
}
