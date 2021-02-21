using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 25f;

    [Header("Jump")]
    [SerializeField] float _fallJump = 5f;
    [SerializeField] float _lowJump = 2f;
    [SerializeField] float _jumpVelocity = 2f;

    public bool CanMove {
        get {
            return _canMove;
        }
        set {
            _canMove = value;
        }
    }

    public bool _isGrounded = true;
    private GameObject _shield;
    private Rigidbody2D _rigidbody2D;
    private bool _canMove = true;
    private float _moveHorizontal;
    public Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (CanMove)
        {
            float translation = _moveHorizontal * Time.fixedDeltaTime * _movementSpeed;
            if (_moveHorizontal != 0)
            {
                transform.localScale = new Vector3(-Mathf.Sign(_moveHorizontal), 1, 1);
            }
            transform.Translate(translation, 0f, 0f);
            _animator.SetFloat("Speed", Mathf.Abs(_moveHorizontal));
        }
    }

    private void Jump()
    {
        if (IsStartingJump())
        {
            _rigidbody2D.velocity = Vector2.up * _jumpVelocity;
        }
        if (IsFalling())
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * _fallJump * Time.fixedDeltaTime;
        }
        else if (IsFloating())
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * _lowJump * Time.fixedDeltaTime;
        }
    }

    private bool IsStartingJump()
    {
        return IsJumping() && _isGrounded && CanMove;
    }

    private bool IsFalling()
    {
        return _rigidbody2D.velocity.y < 0 && CanMove;
    }

    private bool IsFloating()
    {
        return _rigidbody2D.velocity.y > 0 && CanMove;
    }

    private bool IsJumping()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool HasShield()
    {
        return _shield != null ? true : false;
    }

    public void EquipShield(GameObject shield)
    {
        if (!HasShield())
        {
            _shield = Instantiate(shield, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, this.transform) as GameObject;
            _shield.transform.localPosition = new Vector3(0.5f, -0.35f);
        }
    }

    public void DestroyShield()
    {
        Destroy(_shield);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Bridge"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Bridge"))
        {
            _isGrounded = false;
        }
    }
}
