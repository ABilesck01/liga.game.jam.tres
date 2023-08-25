using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    public static string SceneName = "Leaderboard";

    [SerializeField] private LeaderboardItem leaderboardItem;
    [SerializeField] private Transform container;
    [Space]
    [SerializeField] private Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            SceneManager.UnloadSceneAsync(SceneName);
        });

        GetLeaderboard();
    }

    private void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "global",
            StartPosition = 0
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
    }

    private void OnGetLeaderboard(GetLeaderboardResult result)
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
