using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] float _duration = 1.0f;
    [SerializeField] float _amplitude = .7f;

    private Vector3 _originalPos;
    public bool shouldShake;
    void LateUpdate()
    {
        if(shouldShake)
        {
            _originalPos = transform.position;

            float x = _originalPos.x + Random.Range(-1f, 1f) * _amplitude;
            float y = _originalPos.y + Random.Range(-1f, 1f) * _amplitude;

            transform.localPosition = new Vector3(x, y, _originalPos.z);
        }
    }
    public void ShakeScreen()
    {
        shouldShake = true;
    }

    public void StartShaking()
    {
        shouldShake = true;
            Invoke("StopShaking", _duration);
    }

    public void StopShaking()
    {
        shouldShake = false;
        transform.localPosition = _originalPos;
    }
}
