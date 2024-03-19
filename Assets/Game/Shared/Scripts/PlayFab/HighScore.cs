using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private int currentScore;

    private void OnEnable()
    {
        RhythmController.OnCorrectHit += RhythmController_onCorrectHit;
        MainScreen.OnStartGame += MainScreen_OnStartGame;
        GameOverMenu.OnGameFinished += GameOverMenu_OnGameFinished;
    }


    private void OnDisable()
    {
        RhythmController.OnCorrectHit -= RhythmController_onCorrectHit;
        MainScreen.OnStartGame -= MainScreen_OnStartGame;
        GameOverMenu.OnGameFinished -= GameOverMenu_OnGameFinished;
    }

    private void MainScreen_OnStartGame(object sender, System.EventArgs e)
    {
        ResetScore();
    }

    private void RhythmController_onCorrectHit(object sender, System.EventArgs e)
    {
        IncrementScore();
    }
    private void GameOverMenu_OnGameFinished(object sender, System.EventArgs e)
    {
        SendScore();
    }

    private void ResetScore()
    {
        currentScore = 0;
    }

    private void IncrementScore()
    {
        currentScore++;
    }

    private void SendScore()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "global",
                    Value = currentScore
                },
                new StatisticUpdate
                {
                    StatisticName = "weeklyLeaderboard",
                    Value = currentScore
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSendHighscore, OnError);
    }

    private void OnSendHighscore(UpdatePlayerStatisticsResult result)
    {
        Debug.Log(result.Request);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ToString());
    }

}
