using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtPosition;
    [SerializeField] private TextMeshProUGUI txtUser;
    [SerializeField] private TextMeshProUGUI txtPoints;
    [SerializeField] private Image background;
    [Space]
    [SerializeField] private Sprite firstPositionBg;
    [SerializeField] private Sprite secondPositionBg;
    [SerializeField] private Sprite thirdPositionBg;

    public void Instantiateitem(PlayerLeaderboardEntry leaderboardEntry)
    {
        int position = leaderboardEntry.Position + 1;
        if (position == 1)
        {
            background.sprite = firstPositionBg;
        }
        else if (position == 2)
        {
            background.sprite = secondPositionBg;
        }
        else if (position == 3)
        {
            background.sprite = thirdPositionBg;
        }

        txtPosition.text = position.ToString();
        txtUser.text = leaderboardEntry.DisplayName;
        txtPoints.text = leaderboardEntry.StatValue.ToString();
    }
}
