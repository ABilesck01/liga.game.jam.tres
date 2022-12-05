using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float CarSpeed = 3;

    [Header("Effects")]
    public GameObject explosion;
    public GameObject smoke;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.Translate(-Vector3.forward * CarSpeed * Time.deltaTime);
    }

    public void ExplodeCar()
    {
        AudioManager.instance.Play($"explosion_car");
        Rigidbody rb = _transform.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.detectCollisions = false;
        Instantiate(explosion, _transform.position, Quaternion.identity);
        Instantiate(smoke, _transform.position, Quaternion.identity);
        _transform.localScale = new Vector3(_transform.localScale.x, 0.1f, _transform.transform.localScale.z);
        CarSpeed = 0;
    }
}
