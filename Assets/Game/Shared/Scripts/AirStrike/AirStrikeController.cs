using System;
using UnityEngine;
using UnityEngine.UI;

public class AirStrikeController : MonoBehaviour
{
    public int ErrorsToAirStrike = 3;
    public Image targetImg;
    private int currentErrors = 0;
    public float percetage = 0;

    public GameObject missile;
    public Transform player;

    public static EventHandler onAirStrike;


    private void Start()
    {
        RhythmController.onMissHit += RhythmController_onMissHit;
        RhythmController.onCorrectHit += RhythmController_onCorrectHit;
    }

    private void RhythmController_onCorrectHit(object sender, System.EventArgs e)
    {
        Color tempColor = targetImg.color;
        currentErrors = 0;
        tempColor.a = 0;
        targetImg.color = tempColor;
    }

    private void RhythmController_onMissHit(object sender, System.EventArgs e)
    {
        currentErrors++;
        percetage = (float)currentErrors / ErrorsToAirStrike;
        Color tempColor = targetImg.color;
        tempColor.a = percetage;
        targetImg.color = tempColor;
        if(percetage >= 1)
        {
            Instantiate(missile, player.position, Quaternion.Euler(new Vector3(180, 0, 0)));
            currentErrors = 0;
            tempColor.a = 0;
            targetImg.color = tempColor;
        }

    }
}
