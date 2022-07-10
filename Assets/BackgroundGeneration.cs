using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGeneration : MonoBehaviour
{

    public List<GameObject> buildings = new List<GameObject>();
    public float distance;
    int lastBuildingIndex;
    public GameObject player;
    public Vector3 lastPosition;
    public int cont = 10;

    int bpm = 96;
    float interval = 0;
    // Start is called before the first frame update
    void Start()
    {
        interval = 100 * 60 / bpm;
        
        lastPosition = player.transform.position;
        for (int i = 0; i < 10; i++)
        {
            int index = Random.Range(0, buildings.Count - 1);
            while (index == lastBuildingIndex)
            {
                index = Random.Range(0, buildings.Count - 1);
            }

            lastBuildingIndex = index;

            Instantiate(buildings[index], (transform.position + (Vector3.forward * i * distance)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position != lastPosition)
        {
            int index = Random.Range(0, buildings.Count - 1);
            while (index == lastBuildingIndex)
            {
                index = Random.Range(0, buildings.Count - 1);
            }
            lastBuildingIndex = index;
            Instantiate(buildings[index], (transform.position + (Vector3.forward * cont * distance)), Quaternion.identity);
            cont++;
            lastPosition = player.transform.position;
        }
    }
}
