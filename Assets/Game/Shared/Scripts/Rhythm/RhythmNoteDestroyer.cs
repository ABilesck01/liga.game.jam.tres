using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNoteDestroyer : MonoBehaviour
{
    public RhythmController controller;
    public string side = "";

    private void Start()
    {
        controller = FindObjectOfType<RhythmController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(other.transform.parent.CompareTag(side))
            {
                Destroy(other.transform.parent.gameObject);
                controller?.missHit();
            }
        }
    }
}
