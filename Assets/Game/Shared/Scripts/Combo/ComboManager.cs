using System;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int comboCounter;
    public float multiplierIncreaseAmount;
    public int comboAmountToFever = 10;
    [Space(30)]
    public ComboUI ComboUI;
    [Space]
    public DinoBehaviour dinoBehaviour;
    public RhythmController rhythmController;

    public bool isOnFever;

    public static float comboMultiplier = 1;

    public static EventHandler onFeverEnter;
    public static EventHandler onFeverExit;

    public void Start()
    {
        RhythmController.onCorrectHit += AddCombo;
        RhythmController.onMissHit += ResetCombo;

        dinoBehaviour = FindObjectOfType<DinoBehaviour>();
        rhythmController = FindObjectOfType<RhythmController>();
    }

    private void OnDisable()
    {
        RhythmController.onCorrectHit -= AddCombo;
        RhythmController.onMissHit -= ResetCombo;
    }

    public void AddCombo(object sender, EventArgs e)
    {
        comboCounter++;
        ComboUI.updateText(comboCounter);
        if (comboCounter % comboAmountToFever == 0)
        {
            ComboUI.showWindow();
            if (!isOnFever)
            {
                onFeverEnter?.Invoke(this, null);
                isOnFever = true;
            }
            comboMultiplier += multiplierIncreaseAmount;
        }
    }

    public void ResetCombo(object sender, EventArgs e)
    {
        onFeverExit?.Invoke(this, null);
        isOnFever = false;
        ComboUI.closeWindow();
        comboCounter = 0;                 
        comboMultiplier = 1;
    }


}
