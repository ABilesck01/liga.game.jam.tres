using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerController : MonoBehaviour
{
    [SerializeField] private string androidAdUnit;
    [SerializeField] private BannerPosition bannerPosition;

    private static string adUnit;

    private void Start()
    {
        adUnit = androidAdUnit;

        Advertisement.Banner.SetPosition(bannerPosition);

        //LoadBanner();
    }

    public static void LoadBanner()
    {
        BannerLoadOptions bannerOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoad,
            errorCallback = OnBannerLoadError,
        };

        Advertisement.Banner.Load(adUnit, bannerOptions);
    }

    private static void OnBannerLoadError(string message)
    {
        Debug.LogError(message);
    }

    private static void OnBannerLoad()
    {
        ShowBanner();
    }

    private static void ShowBanner()
    {
        BannerOptions bannerOptions = new BannerOptions
        {
            showCallback = OnShowBanner,
            clickCallback = OnClickBanner,
            hideCallback = OnHideBanner
        };

        Advertisement.Banner.Show(adUnit, bannerOptions);
    }

    private static void OnHideBanner()
    {
        Advertisement.Banner.Hide();
    }

    private static void OnClickBanner()
    {
    }

    public static void HideBanner()
    {
        Debug.Log("hide banner");
        Advertisement.Banner.Hide(false);
    }

    private static void OnShowBanner()
    {
    }
}
