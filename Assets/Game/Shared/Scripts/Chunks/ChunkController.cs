using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public Transform spawnPoint;
    public ChunkManager chunkManager;
    public CarSpawner carSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            chunkManager.PlayerHasPassedChunk();
        }
        else if(other.CompareTag("chunk"))
        {
            Destroy(gameObject);
        }
    }
}
