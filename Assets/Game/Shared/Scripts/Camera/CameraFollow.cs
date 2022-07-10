using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float Smoothness;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        Vector3 smoothPos = Vector3.Lerp(_transform.position, target.position, Smoothness * Time.deltaTime);
        _transform.position = smoothPos;
    }
}
