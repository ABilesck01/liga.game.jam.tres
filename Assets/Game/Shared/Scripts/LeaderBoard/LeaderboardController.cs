using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [System.Serializable]
    public struct LeaderboardView
    {
        public string leaderboardName;
        public Transform container;
    }

    public static string SceneName = "Leaderboard";

    [SerializeField] private List<LeaderboardView> views = new List<LeaderboardView>();

    [SerializeField] private LeaderboardItem leaderboardItem;
    [Space]
    [SerializeField] private Button btnClose;
    [Space]
    [SerializeField] private InterstitialController interstitialController;

    private void Start()
    {
        if (Random.value < 0.33f)
            interstitialController.ShowAd();

        btnClose.onClick.AddListener(() =>
        {
            SceneManager.UnloadSceneAsync(SceneName);
        });

        foreach (var view in views)
        {
            GetLeaderboard(view);
        }

        //GetLeaderboard();
    }

    private void GetLeaderboard(LeaderboardView view)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = view.leaderboardName,
            StartPosition = 0
        };

        PlayFabClientAPI.GetLeaderboard(request, result => OnGetLeaderboard(result, view.container), OnError);
    }

    private void OnGetLeaderboard(GetLeaderboardResult result, Transform container)
    {
        foreach (var item in result.Leaderboard)
        {
            var listItem = Instantiate(leaderboardItem, container);
            listItem.Instantiateitem(item);
        }
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }

    
}
