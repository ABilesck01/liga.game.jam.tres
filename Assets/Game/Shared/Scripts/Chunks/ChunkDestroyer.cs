using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("chunk"))
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
