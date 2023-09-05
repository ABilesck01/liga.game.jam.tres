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
    public static event EventHandler OnGameFinished;

    private void Awake()
    {
        btnAdContinue.onClick.AddListener(OnBtnAdContinueClick);
        btnClose.onClick.AddListener(OnBtnCloseClick);
        rewardController.OnGetReward += RewardController_OnGetReward;
        rewardController.OnSkipReward += RewardController_OnSkipReward;
    }

    private void Start()
    {
        AudioManager.instance.Play("death", true);
    }

    private void OnDisable()
    {
        rewardController.OnGetReward -= RewardController_OnGetReward;
        rewardController.OnSkipReward -= RewardController_OnSkipReward;
    }

    private void RewardController_OnGetReward(object sender, EventArgs e)
    {
        OnAdSuccess?.Invoke(this, EventArgs.Empty);
        SceneManager.UnloadSceneAsync("Death");
    }

    private void RewardController_OnSkipReward(object sender, EventArgs e)
    {
        Debug.Log("ad skiped");
        DinoHealth.hasUsedAd = true;
        OnBtnCloseClick();
    }

    private void OnBtnAdContinueClick()
    {
        AudioManager.instance.StopAllAudios();
        rewardController.ShowAd();
    }

    private void OnBtnCloseClick()
    {
        GameFinish();
        SceneManager.UnloadSceneAsync("Death");
        SceneManager.LoadScene("Game");
    }

    public static void GameFinish()
    {
        Debug.Log("game finished");
        OnGameFinished?.Invoke(null, EventArgs.Empty);
    }

}


