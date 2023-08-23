using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button btnAdContinue;
    [SerializeField] private Button btnClose;
    [Space]
    [SerializeField] private RewardController rewardController;

    public static event EventHandler OnAdSuccess;

    private void Awake()
    {
        btnAdContinue.onClick.AddListener(OnBtnAdContinueClick);
        btnClose.onClick.AddListener(OnBtnCloseClick);
        rewardController.OnGetReward += RewardController_OnGetReward;
        rewardController.OnSkipReward += RewardController_OnSkipReward;
    }

    private void OnDisable()
    {
        rewardController.OnGetReward -= RewardController_OnGetReward;
        rewardController.OnSkipReward -= RewardController_OnSkipReward;
    }

    private void RewardController_OnGetReward(object sender, EventArgs e)
    {
        SceneManager.UnloadSceneAsync("Death");
        OnAdSuccess?.Invoke(this, EventArgs.Empty);
    }

    private void RewardController_OnSkipReward(object sender, EventArgs e)
    {
        DinoHealth.hasUsedAd = true;
        OnBtnCloseClick();
    }

    private void OnBtnAdContinueClick()
    {
        rewardController.ShowAd();
    }

    private void OnBtnCloseClick()
    {
        SceneManager.UnloadSceneAsync("Death");
        SceneManager.LoadScene("Game");
    }

}


