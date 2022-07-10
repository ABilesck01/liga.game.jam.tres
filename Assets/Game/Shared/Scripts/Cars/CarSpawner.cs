using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carsToSpawn;

    public void SpawnCar()
    {
        int car = Random.Range(0, carsToSpawn.Length);
        Instantiate(carsToSpawn[car], transform.position, Quaternion.identity, transform);
    }
}
