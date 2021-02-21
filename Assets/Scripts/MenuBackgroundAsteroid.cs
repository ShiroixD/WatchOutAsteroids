using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundAsteroid : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] GameObject _explosion;

    private float _x;
    private float _y;

    private float _dX;
    private float _angle;

    private Rigidbody2D _rigidbody;

    private const float Pi180 = 0.01745329f;

    void OnStart(float[] args)
    {
        _angle = args[0];
        _rigidbody = GetComponent<Rigidbody2D>();
        RandAngle();
        _x = _dX;
        _y = -_speed * args[1];
        _rigidbody.velocity = new Vector2(_x, _y);

    }

    private void RandAngle()
    {
        float angleInDegrees = _angle * Pi180;
        _dX = _speed * Mathf.Tan(angleInDegrees);
    }

    private void DestroyMyself()
    {
        Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            SoundManager.instance.PlaySound("MeteorToMeteor");
            DestroyMyself();
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.PlaySound("MeteorToMeteor");
            DestroyMyself();
        }
        else
        {
            Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        }
    }
}