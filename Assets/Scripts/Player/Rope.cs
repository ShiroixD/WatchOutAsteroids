using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    [Header("Line")]
    [SerializeField] float _ropeSpeed;

    private float _currentRopeLength = 0.01f;
    private float _maxRopeLength = 0f;

    private bool _isRopeAttached = false;
    public bool IsRopeAttached 
    {
        get {
            return _isRopeAttached;
        }
        set {
            _isRopeAttached = value;
        }
    }

    private string _fireButton = "Fire2";
    private Vector2 _mousePosition;

    public RaycastHit2D _hitPoint;
    public RaycastHit2D HitPoint 
    {
        get {
            return _hitPoint;
        }
        set {
            _hitPoint = value;
        }
    }
    private Rigidbody2D _rigidBody;
    public Rigidbody2D Rigidbody 
    {
        get {
            return _rigidBody;
        }
        set {
            _rigidBody = value;
        }
    }

    private LineRenderer line;
    public LineRenderer Line {
        get {
            return line;
        }

        set {
            line = value;
        }
    }

    void Start () {
        Line = GetComponent<LineRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Line.startWidth = 0.2f;
        Line.endWidth = 0.2f;
        Line.material.color = Color.red;
        Line.startColor = Color.red;
        Line.endColor = Color.red;
        _ropeSpeed = 0.1f;
    }
	
	void Update () {
        Shoot();
        DrawLine();        
	}

    private void Shoot()
    {
        if (Input.GetButtonDown(_fireButton))
        {
            int mask = 1 << LayerMask.NameToLayer("SpecialAsteroids");
            var start = transform.position;
            _mousePosition = Input.mousePosition;
            var camera = Camera.main.transform;
            var end = Camera.main.ScreenToWorldPoint(_mousePosition) - start;
            _maxRopeLength = Vector2.Distance(start, end);
            HitPoint = Physics2D.Raycast(start, end, _maxRopeLength, mask);
            if(HitPoint.collider != null)
            {
                Line.enabled = true;
                HitPoint.collider.GetComponent<Asteroid>().IsBashed = true;
            }

        }
    }

    private void DrawLine()
    {
        _currentRopeLength += _ropeSpeed;
        if (_currentRopeLength <= _maxRopeLength)
        {
            Line.SetPosition(0, transform.position);
            if (HitPoint.collider != null)
            {
                Vector2 tmp = Vector2.Lerp(transform.position, HitPoint.transform.position, _currentRopeLength);
                Line.SetPosition(1, tmp);
            }
        }
        else
        {
            IsRopeAttached = true;
            _currentRopeLength = 0;
            _maxRopeLength = 0;
        }        
    }

    private void StartTime()
    {
        Time.timeScale = 1;
    }

    private void Slowmotion()
    {
        Time.timeScale = 0.1f;
    }
}
