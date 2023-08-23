using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button btnAdContinue;
    [SerializeField] private Button btnClose;

    public static event EventHandler OnAdSuccess;

    private void Awake()
    {
        btnAdContinue.onClick.AddListener(OnBtnAdContinueClick);
        btnClose.onClick.AddListener(OnBtnCloseClick);
    }

    private void OnBtnAdContinueClick()
    {
        SceneManager.UnloadSceneAsync("Death");
        OnAdSuccess?.Invoke(this, EventArgs.Empty);
    }

    private void OnBtnCloseClick()
    {
        SceneManager.UnloadSceneAsync("Death");
        SceneManager.LoadScene("Game");
    }

}


