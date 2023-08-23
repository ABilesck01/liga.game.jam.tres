using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoAnimation : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        MainScreen.OnStartGame += MainScreen_OnStartGame;
        GameOverMenu.OnAdSuccess += MainScreen_OnStartGame;
    }

    private void OnDisable()
    {
        MainScreen.OnStartGame -= MainScreen_OnStartGame;
        GameOverMenu.OnAdSuccess -= MainScreen_OnStartGame;
    }

    private void MainScreen_OnStartGame(object sender, System.EventArgs e)
    {
        PlayTargetAnimation("intro");
    }

    public void PlayTargetAnimation(string targetAnim)
    {
        animator.CrossFade(targetAnim, 0.1f);
    }

    public void SetIdleState(bool isAttacking)
    { 
        if (isAttacking)
        {
            animator.SetFloat("idle_state", 1);
        }
        else
        {
            animator.SetFloat("idle_state", 0);
        }
    }

    public void stompSound()
    {
        AudioManager.instance.Play("stomp");
    }
}
