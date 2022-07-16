using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNeedle : MonoBehaviour
{
    public RhythmController controller;
    public Transform GFX;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("noteBegin"))
        {
            controller.NeedleOnPosition = true;
            controller.currentNote = other.transform.parent.gameObject;
            GFX.localScale *= 1.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("noteEnd"))
        {
            controller.NeedleOnPosition = false;
            GFX.localScale /= 1.1f;
        }
    }

}
