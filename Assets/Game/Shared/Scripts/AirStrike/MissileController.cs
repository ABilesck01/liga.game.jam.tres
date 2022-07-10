using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject explosion;
    public float missileSpeed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, _transform.position, Quaternion.identity);
        AudioManager.instance.Play("missile");
        AirStrikeController.onAirStrike?.Invoke(this, null);
    }
}
