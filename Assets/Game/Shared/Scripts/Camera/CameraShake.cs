using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private Transform _transform;
    private bool _isPlaying = false;

    private static float intensity;

    public static float Intensity
    {
        get => intensity;
        set
        {
            intensity = Mathf.Clamp01(value);

        } 
    }

    private void Start()
    {
        _transform = transform;
        instance = this;
    }

    public void Shake(float duration, float mag)
    {
        if (_isPlaying) return;

        StartCoroutine(ShakeCamera(duration, mag));
    }

    private IEnumerator ShakeCamera(float duration, float mag)
    {
        Vector3 originalPosition = _transform.localPosition;

        float timeElapsed = 0;

        _isPlaying = true;

        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * mag * ComboManager.comboMultiplier * intensity;
            float y = Random.Range(-1f, 1f) * mag * ComboManager.comboMultiplier * intensity;

            _transform.localPosition = new Vector3(x, y, originalPosition.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _transform.localPosition = originalPosition;
        _isPlaying = false;

    }
}
