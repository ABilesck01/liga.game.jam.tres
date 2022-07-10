using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoAnimation : MonoBehaviour
{
    public Animator animator;

    public void PlayTargetAnimation(string targetAnim)
    {
        animator.CrossFade(targetAnim, 0.1f);
    }

    public void setIdleState(bool isAttacking)
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
