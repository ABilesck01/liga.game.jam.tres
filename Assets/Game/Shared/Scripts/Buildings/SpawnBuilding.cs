using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{
    public GameObject[] buildings;

    public static int lastBuildingIndex;

    void Start()
    {
        int myBuilding = -1;

        do
        {
            myBuilding = Random.Range(0, buildings.Length);
        }
        while(myBuilding == lastBuildingIndex);

        Instantiate(buildings[myBuilding], transform.position, Quaternion.identity);
        lastBuildingIndex = myBuilding;
    }

}
