using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour {
	private Rigidbody2D _rigidbody2D;
	private WallClimb _wallClimb;
	public Transform WallCheckPoint;
	public LayerMask WallLayerMask;
	public float Radius = 1f;
	public float Speed = -2.0f;
	private bool _isTouchingWall = false;

	void Start () 
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_wallClimb = GetComponent<WallClimb>();
	}
	
	void Update () 
	{
		_isTouchingWall = Physics2D.OverlapCircle(WallCheckPoint.position, Radius, WallLayerMask);
	}

	void FixedUpdate()
	{
		if(_isTouchingWall && !_wallClimb.Climb && !Input.GetKey(KeyCode.S))
		{
			SlideDownWall();
		}
	}

	private void SlideDownWall()
	{
		_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Speed);
	}
}
