using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnit;

    private string adUnit;

    public event EventHandler OnSkipReward;
    public event EventHandler OnGetReward;

    public void LoadAd()
    {
        Advertisement.Load(adUnit, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (!placementId.Equals(adUnit)) return;

        Debug.Log("add loaded", this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError(message, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(adUnit, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (!placementId.Equals(adUnit)) return;

        if(!showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            OnSkipReward?.Invoke(this, EventArgs.Empty);
            return;
        }

        Debug.Log("Interstitial complete", this);
        OnGetReward?.Invoke(this, EventArgs.Empty);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError(message, this);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Interstitial start", this);
    }

    private void Awake()
    {
        adUnit = androidAdUnit;
    }

    private void Start()
    {
        LoadAd();
    }

}
