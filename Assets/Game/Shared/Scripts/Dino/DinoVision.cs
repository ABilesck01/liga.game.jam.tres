using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoVision : MonoBehaviour
{
    public DinoBehaviour DinoBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("building"))
        {
            DinoBehaviour.hasBuilding = true;
            DinoBehaviour.BuildingInWorld = other.GetComponent<BuildingInWorld>();
        }
        else if(other.CompareTag("entity"))
        {
            DinoBehaviour.hasEntity = true;
            DinoBehaviour.carControllerInWorld = other.GetComponent<CarController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("building"))
        {
            DinoBehaviour.hasBuilding = false;
        }
        else if (other.CompareTag("entity"))
        {
            DinoBehaviour.hasEntity = false;
        }
    }
}
