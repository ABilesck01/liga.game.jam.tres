using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtPosition;
    [SerializeField] private TextMeshProUGUI txtUser;
    [SerializeField] private TextMeshProUGUI txtPoints;

    public void Instantiateitem(PlayerLeaderboardEntry leaderboardEntry)
    {
        txtPosition.text = (leaderboardEntry.Position + 1).ToString();
        txtUser.text = leaderboardEntry.DisplayName;
        txtPoints.text = leaderboardEntry.StatValue.ToString();
    }
}
