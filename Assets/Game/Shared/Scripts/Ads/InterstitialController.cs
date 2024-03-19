using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnit;

    private string adUnit;

    public void LoadAd()
    {
        Advertisement.Load(adUnit, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
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

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial complete", this);
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
