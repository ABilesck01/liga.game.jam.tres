using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInWorld : MonoBehaviour
{
    public int minFloorsAmount = 3;
    public int maxFloorsAmount = 10;

    [Space]
    public float floorHeight = 1.6f;
    [Space]
    public GameObject level0_prefab;
    public GameObject levelAbove_prefab;
    [Space]
    public GameObject destroyEffects;
    private int FloorsCount;
    private int currentFloorsCount;
    private List<GameObject> floors;

    private void Start()
    {
        floors = new List<GameObject>();

        CreateBuilding();
    }

    private void CreateBuilding()
    {
        Transform t = Instantiate(level0_prefab, transform).transform;
        t.localPosition = Vector3.zero;
        floors.Add(t.gameObject);
        
        if(levelAbove_prefab != null)
        {
            FloorsCount = Random.Range(minFloorsAmount, maxFloorsAmount);

            for (int i = 0; i < FloorsCount; i++)
            {
                Transform f = Instantiate(levelAbove_prefab, transform).transform;
                f.localPosition = Vector3.zero;
                f.localPosition += new Vector3(0, (i + 1) * floorHeight, 0);
                floors.Add(f.gameObject);
            }
            currentFloorsCount = FloorsCount;
        }
        else
        {
            FloorsCount = 1;
        }
    }

    public bool BreakBuilding()
    {
        GameObject removedFloor = floors[0];
        floors.Remove(removedFloor);
        Instantiate(destroyEffects, removedFloor.transform.position, Quaternion.identity);
        Destroy(removedFloor);

        int index = Random.Range(1, 3);
        AudioManager.instance.Play($"explosion{index}");

        for (int i = 0; i < floors.Count; i++)
        {
            if(floors[i] != null)
            {
                floors[i].transform.localPosition = Vector3.zero;
                floors[i].transform.localPosition += new Vector3(0, i * floorHeight, 0);
            }
        }

        if (currentFloorsCount <= 0)
        {
            Destroy(gameObject);

            return false;
        }

        currentFloorsCount--;
        return true;

    }
}
