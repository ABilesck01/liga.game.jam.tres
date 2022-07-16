using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScreen : MonoBehaviour
{
    public TextMeshProUGUI txtHighscore;
    public TextMeshProUGUI txtLastGamePoints;
    public TextMeshProUGUI txtHash;
    public bool ShowLastPoints;
    public static EventHandler onStartGame;
    public string scene;
    public string song;
    
    private void Start()
    {
        AudioManager.instance.Play(song);

        if (PlayerPrefs.HasKey("highscore"))
        {
            Debug.Log("has highscore");
            txtHighscore.text = $"Highscore: {PlayerPrefs.GetInt("highscore")}";
        }
        if (PlayerPrefs.HasKey("lastGameHits"))
        {
            if(ShowLastPoints)
            {
                txtLastGamePoints.text = $"This game: {PlayerPrefs.GetInt("lastGameHits")}";
                generateHash();
                resolveHash();
            }
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            btnPlayGame();
        }
    }

    private void generateHash()
    {
        int increment = 2934;
        int points = PlayerPrefs.GetInt("lastGameHits") * increment;
        Debug.Log(points);
        string myHex = points.ToString("X");  // Gives you hexadecimal
        txtHash.text = myHex;
    }

    private void resolveHash()
    {
        int increment = 2934;
        int points = Convert.ToInt32(txtHash.text, 16);
        Debug.Log(points / increment);
    }

    public void btnPlayGame()
    {
        SceneManager.LoadScene(scene);
    }
}
