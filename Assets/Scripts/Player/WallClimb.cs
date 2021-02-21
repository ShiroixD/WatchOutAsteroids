using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : MonoBehaviour {
	public float Radius = 1f;
	public float Speed = 8.0f;
	public bool Climb {
		get
		{
			return _climb;
		}
		private set
		{
			_climb = value;
		}
	}
	public Transform WallCheckPoint;
	public LayerMask WallLayerMask;
	private Rigidbody2D _rigidbody2D;
	private bool _isTouchingWall;
	private bool _climb;
    private bool _isClimbing;
    private Sprite _szprajt;
	private Sprite _basicSzprajt;
	private SpriteRenderer szprajt;
    public Animator _animator;

    void Start () 
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		szprajt = GetComponent<SpriteRenderer>();
		_szprajt = Resources.Load<Sprite>("caretaker_from_behind");
		_basicSzprajt = szprajt.sprite;
        _isClimbing = false;
    }
	
	void Update () 
	{
		_isTouchingWall = Physics2D.OverlapCircle(WallCheckPoint.position, Radius, WallLayerMask);
		_climb = Input.GetKey(KeyCode.W);
	}

	void FixedUpdate()
	{
		if(_isTouchingWall && _climb)
		{
            _isClimbing = true;
            ClimbUpWall();
			szprajt.sprite = _szprajt;
		}
		else
		{
			szprajt.sprite = _basicSzprajt;
            _isClimbing = false;
        }
        _animator.SetBool("IsClimbing", _isClimbing);
    }

	private void ClimbUpWall()
	{
		_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Speed);
	}
}
