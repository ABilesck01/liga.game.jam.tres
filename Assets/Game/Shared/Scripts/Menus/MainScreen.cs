using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScreen : MonoBehaviour
{
    public static string SCENE_NAME = "menu";

    [SerializeField] private string song;
    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnLeaderboard;
    [SerializeField] private Button btnOptions;

    public static event EventHandler OnStartGame;

    private void Awake()
    {
        btnPlay.onClick.AddListener(BtnPlayGame);
        btnLeaderboard.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(LeaderboardController.SceneName, LoadSceneMode.Additive);
        });

        btnOptions.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(OptionsController.SceneName, LoadSceneMode.Additive);
        });
    }

    private void Start()
    {
        AudioManager.instance.Play(song);
    }

    public void BtnPlayGame()
    {
        //SceneManager.LoadScene(scene);
        SceneManager.UnloadSceneAsync(SCENE_NAME);
        OnStartGame?.Invoke(this, EventArgs.Empty);
    }
}
