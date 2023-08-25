using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoHealth : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;

    public bool isDead = false;

    public GameObject[] hearts;

    [Space]
    public DinoAnimation dinoAnimation;


    public static bool hasUsedAd = false;

    public static EventHandler OnPlayerDeath;

    private void Start()
    {
        currentHealth = MaxHealth;
        AirStrikeController.onAirStrike += onAirStrike;
        //SetupHearts();
    }

    private void OnEnable()
    {
        GameOverMenu.OnAdSuccess += GameOverMenu_OnAdSuccess;
    }

    private void OnDisable()
    {
        AirStrikeController.onAirStrike -= onAirStrike;
        GameOverMenu.OnAdSuccess -= GameOverMenu_OnAdSuccess;
    }

    private void onAirStrike(object sender, EventArgs e)
    {
        TakeDamage();
    }

    private void SetupHearts()
    {
        for (int i = 0; i < MaxHealth; i++)
        {
            hearts[i].SetActive(i <= currentHealth - 1);
        }
    }

    private void GameOverMenu_OnAdSuccess(object sender, EventArgs e)
    {
        hasUsedAd = true;
        currentHealth = 1;
        isDead = false;
        SetupHearts();
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentHealth--;
        if(currentHealth < 0)
            currentHealth = 0;
        hearts[currentHealth].SetActive(false);
        dinoAnimation.PlayTargetAnimation("damage");
        AudioManager.instance.Play("damage");
        if (currentHealth <= 0)
        {
            isDead = true;
            OnPlayerDeath?.Invoke(this, null);
            //AudioManager.instance.StopAllAudios();
            //AudioManager.instance.Play("death");
            //AudioManager.instance.Play("heart");
            dinoAnimation.PlayTargetAnimation("die");
            Invoke(nameof(LoadDeathScreen), 2.5f);
        }
    }

    public void LoadDeathScreen()
    {
        if (!hasUsedAd)
            SceneManager.LoadScene("Death", LoadSceneMode.Additive);
        else
        {
            SceneManager.LoadScene("Game");
            hasUsedAd = false;
            GameOverMenu.GameFinish();
        }
    }
}
