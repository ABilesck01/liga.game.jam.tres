using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNoteDestroyer : MonoBehaviour
{
    public RhythmController controller;

    private void Start()
    {
        controller = FindObjectOfType<RhythmController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Destroy(other.transform.parent.gameObject);
            controller?.missHit();
        }
    }
}
