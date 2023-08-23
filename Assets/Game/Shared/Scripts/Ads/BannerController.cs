using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerController : MonoBehaviour
{
    [SerializeField] private string androidAdUnit;
    [SerializeField] private BannerPosition bannerPosition;

    private string adUnit;

    private void Start()
    {
        adUnit = androidAdUnit;

        Advertisement.Banner.SetPosition(bannerPosition);

        LoadBanner();
    }

    private void LoadBanner()
    {
        BannerLoadOptions bannerOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoad,
            errorCallback = OnBannerLoadError,
        };

        Advertisement.Banner.Load(adUnit, bannerOptions);
    }

    private void OnBannerLoadError(string message)
    {
        Debug.LogError(message);
    }

    private void OnBannerLoad()
    {
        ShowBanner();
    }

    private void ShowBanner()
    {
        BannerOptions bannerOptions = new BannerOptions
        {
            showCallback = OnShowBanner,
            clickCallback = OnClickBanner,
            hideCallback = OnHideBanner
        };

        Advertisement.Banner.Show(adUnit, bannerOptions);
    }

    private void OnHideBanner()
    {
        Advertisement.Banner.Hide();
    }

    private void OnClickBanner()
    {
    }

    private void OnShowBanner()
    {
    }
}
