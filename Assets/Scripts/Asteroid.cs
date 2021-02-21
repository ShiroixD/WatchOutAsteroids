using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] int _points = 10;
    [SerializeField] int _damage = 15;
    [SerializeField] GameObject _explosion;

    public int Points
    {
        get
        {
            return _points;
        }
    }

    private bool _isThrown = false;

    private float _x;
    private float _y;

    private float _dX;
    private float _angle;

    private Rigidbody2D _rigidbody;

    private const float Pi180 = 0.01745329f;

    private Camera _camera;
    private AsteroidNavigationController _navigationController;

    public bool IsThrown
    {
        get
        {
            return _isThrown;
        }

        set
        {
            _isThrown = value;
            if (!value)
            {
                gameObject.layer = _prevLayerMask;
            }
        }
    }

    private bool _isBashed;
    private LayerMask _prevLayerMask;
    public bool IsBashed
    {
        get
        {
            return _isBashed;
        }
        set
        {
            _isBashed = value;
            if (value)
            {
                _prevLayerMask = gameObject.layer;
                gameObject.layer = LayerMask.NameToLayer("BashedAsteroids");
            }
            else
            {
                gameObject.layer = _prevLayerMask;
            }
        }
    }


    public Vector2 ThrowDirection { get; set; }
    public float ThrowSpeed { get; set; }

    public void UpdateAsteroidLayer(string layer)
    {
        _prevLayerMask = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    private void Start()
    {
        _camera = Camera.main;
        _navigationController = _camera.GetComponent<AsteroidNavigationController>();
    }

    void OnStart(float[] args)
    {
        _angle = args[0];
        _rigidbody = GetComponent<Rigidbody2D>();
        RandAngle();
        _x = _dX;
        _y = -_speed * args[1];
        _rigidbody.velocity = new Vector2(_x, _y);

    }

    void FixedUpdate()
    {
        if (IsThrown)
        {
            ThrowMove();
        }
    }

    private void Update()
    {
        CheckOutOfScreen();
        CheckInScreen();
    }

    private void ThrowMove()
    {
        _rigidbody.velocity = ThrowDirection * ThrowSpeed;
    }

    void RandAngle()
    {
        float angleInDegrees = _angle * Pi180;
        _dX = _speed * Mathf.Tan(angleInDegrees);
    }



    void CheckOutOfScreen()
    {
        if (_camera.transform.position.x + _navigationController.OffsetX < transform.position.x)
        {
            _navigationController.DrawArrowRight(this);
        }
        else if (_camera.transform.position.x - _navigationController.OffsetX > transform.position.x)
        {
            _navigationController.DrawArrowLeft(this);
        }
    }

    void CheckInScreen()
    {
        if (_camera.transform.position.x - _navigationController.OffsetX < transform.position.x &&
            _camera.transform.position.x + _navigationController.OffsetX > transform.position.x)
        {
            _navigationController.DeleteArrow(this);
        }
    }

    public void DestroyMyself()
    {
        Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void DestroyMyself(int points)
    {
        Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        PointsController.instance.CalculatePoints(points);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound("ShieldToMeteor");
            ShieldDefence shieldDefence = collision.gameObject.GetComponentInChildren<ShieldDefence>();
            if (shieldDefence != null)
            {
                shieldDefence.TakeDamage(_damage);
                DestroyMyself(Points);
            }
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            ShieldDefence shieldDefence = collision.gameObject.GetComponent<ShieldDefence>();
            if (shieldDefence != null)
            {
                shieldDefence.TakeDamage(_damage);
                DestroyMyself(Points);
            }
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            SoundManager.instance.PlaySound("MeteorToMeteor");
            DestroyMyself(Points);
        }
        else if (collision.gameObject.CompareTag("Bridge"))
        {
            SoundManager.instance.PlaySound("ShieldToMeteor");
            PointsController.instance.CalculatePoints(Points);
            DestroyMyself(_points);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            var cameraShake = Camera.main.GetComponent<CameraShake>();
            cameraShake.StartShaking();
            SoundManager.instance.PlaySound("MeteorToMeteor");
            DestroyMyself();
        }
        else
        {
            Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        if (_camera)
        {
            _navigationController.DeleteArrow(this);
        }
    }
}