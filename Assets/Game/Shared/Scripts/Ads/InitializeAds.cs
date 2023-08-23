using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidId;
    [Space]
    [SerializeField] private bool isTestMode = true;

    private string gameId;

    private void Awake()
    {
        InitAds();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError(message);
    }

    private void InitAds()
    {
        gameId = androidId;

        Advertisement.Initialize(gameId, isTestMode, this);
    }
}
