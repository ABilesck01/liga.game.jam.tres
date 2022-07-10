using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunk types")]
    public GameObject singleLineChunk;
    public GameObject crossRoadChunk;
    [Header("Chunk flags")]
    public ChunkController currentChunk;
    public ChunkController nextChunk;
    public ChunkController prevChunk;


    public bool canSpawnCrossingRoad = false;

    public void SpawnChunk()
    {
        if(canSpawnCrossingRoad)
        {
            GameObject road = crossRoadChunk;

            //int type = Random.Range(0, 2);
            //if (type == 0)
            //    road = singleLineChunk;
            //else
            //    road = crossRoadChunk;

            GameObject newChunk = Instantiate(road, currentChunk.spawnPoint.position, Quaternion.identity, transform);
            nextChunk = newChunk.GetComponent<ChunkController>();
            nextChunk.chunkManager = this;
            canSpawnCrossingRoad = false;
        }
        else
        {
            GameObject road = singleLineChunk;
            GameObject newChunk = Instantiate(road, currentChunk.spawnPoint.position, Quaternion.identity, transform);
            nextChunk = newChunk.GetComponent<ChunkController>();
            nextChunk.chunkManager = this;
            canSpawnCrossingRoad = true;
        }

        
    }

    public void PlayerHasPassedChunk()
    {
        //if (prevChunk != null)
        //{
        //    Destroy(prevChunk.gameObject);
        //}
        prevChunk = currentChunk;
        
        currentChunk?.carSpawner?.SpawnCar();
        
        currentChunk = nextChunk;
        SpawnChunk();

    }
}
