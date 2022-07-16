using System;
using UnityEngine;

public class DinoHealth : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;

    public bool isDead = false;

    public GameObject[] hearts;

    [Space]
    public DinoAnimation dinoAnimation;

    public static EventHandler onPlayerDeath;

    private void Start()
    {
        currentHealth = MaxHealth;
        AirStrikeController.onAirStrike += onAirStrike;
        //SetupHearts();
    }

    private void OnDisable()
    {
        AirStrikeController.onAirStrike -= onAirStrike;
    }

    private void onAirStrike(object sender, EventArgs e)
    {
        TakeDamage();
    }

    private void SetupHearts()
    {
        for (int i = 0; i < MaxHealth; i++)
        {
            if (i <= currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(true);
            }
        }
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
            onPlayerDeath?.Invoke(this, null);
            AudioManager.instance.StopAllAudios();
            AudioManager.instance.Play("death");
            AudioManager.instance.Play("heart");
            dinoAnimation.PlayTargetAnimation("die");
            Invoke(nameof(LoadDeathScreen), 3f);
        }
    }

    public void LoadDeathScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
    }
}
