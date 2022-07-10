using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScreen : MonoBehaviour
{
    public TextMeshProUGUI txtHighscore;
    public static bool resetScene;
    public static EventHandler onStartGame;
    public string scene;
    public string song;
    
    private void Start()
    {
        AudioManager.instance.Play(song);

        if (PlayerPrefs.HasKey("highscore"))
        {
            txtHighscore.text = $"Highscore: {PlayerPrefs.GetInt("highscore")}";
        }
    }

    public void btnPlayGame()
    {
        SceneManager.LoadScene(scene);
    }
}
