using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    [SerializeField] float _dashSpeed;
    [SerializeField] float _startDashTime;
    [SerializeField] float _dashCooldown;

    private DashDirection _direction;
    private float _dashTime;
    private float _cooldownTimer;
    private Rigidbody2D _rigidbody2D;
    public GameObject _playerGhost;
    public Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _dashTime = _startDashTime;
    }

    void FixedUpdate()
    {
        Dash();
        CooldownButtons();
    }

    private void Dash()
    {
        if (_direction == DashDirection.None && _cooldownTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _direction = DashDirection.Left;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _direction = DashDirection.Right;
            }
        }
        else
        {
            if (_dashTime <= 0)
            {
                ResetProperties();
            }
            else
            {
                _dashTime -= Time.deltaTime;
                ChangeVelocity();
                _animator.SetFloat("DashTime", _dashTime);
            }
        }
    }

    private void ResetProperties()
    {
        _direction = DashDirection.None;
        _dashTime = _startDashTime;
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void ChangeVelocity()
    {
        if (_direction == DashDirection.Left)
        {
            _rigidbody2D.velocity = Vector2.left * _dashSpeed;
            transform.localScale = new Vector3(Vector2.right.x, 1, 1);
        }
        else if (_direction == DashDirection.Right)
        {
            _rigidbody2D.velocity = Vector2.right * _dashSpeed;
            transform.localScale = new Vector3(Vector2.left.x, 1, 1);
        }
        Instantiate(_playerGhost, transform.position, transform.rotation);
    }

    private void CooldownButtons()
    {
        if ( _cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}

public enum DashDirection
{
    None,
    Left,
    Right
}