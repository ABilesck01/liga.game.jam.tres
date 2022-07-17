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
            if(other.transform.parent.CompareTag("note_R"))
                controller.currentNote_R = other.transform.parent.gameObject;
            else if (other.transform.parent.CompareTag("note_L"))
                controller.currentNote_L = other.transform.parent.gameObject;

            controller.NeedleOnPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("noteEnd"))
        {
            controller.NeedleOnPosition = false;
        }
    }

}
