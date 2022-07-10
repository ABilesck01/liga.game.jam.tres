using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboUI : MonoBehaviour
{
    public TextMeshProUGUI txtCombo;
    public Animator animator;

    public void showWindow()
    {
        animator.SetBool("showCombo", true);
    }

    public void closeWindow()
    {
        animator.SetBool("showCombo", false);
    }

    public void updateText(int comboCounter)
    {
        txtCombo.text = comboCounter.ToString();
    }
}
