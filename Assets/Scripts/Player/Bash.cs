using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bash : MonoBehaviour {
	public string BashButton = "Fire2";
	public float ReachRadius = 4f;
	public float ThrowSpeed = 2;
	public float MaxBashTime = 1f;
	public float SlowMotionScale = 0;
	public float ArrowRotationSpeed = 1.5f;
	public Transform Arrow;
	private RaycastHit2D[] _objects;
	private Vector2 _throwDirection;
	private bool _canBash;
	private GameObject _interactionObject;
  private Rope _rope;
  private Asteroid _asteroidComponent;

	void Start ()
	{
        _rope = GetComponent<Rope>();
		DeactivateArrow();
	}
	
	void Update ()
	{
		if(IsRightMouseButtonClicked())
		{
			_objects = Physics2D.CircleCastAll(transform.position, ReachRadius, Vector2.zero);
			foreach (RaycastHit2D @object in _objects.Reverse())
			{
				GameObject prop = @object.collider.gameObject;
				if(IsAsteroid(prop))
				{
                    _asteroidComponent = prop.GetComponent<Asteroid>();
					StopTime();
                    TurnOffRope();
					StartCoroutine("Counter");
					ActivateArrow(prop);
					_interactionObject = prop;
					ActivateBash();
                    _asteroidComponent.UpdateAsteroidLayer("BashedAsteroids");
					break;
				}
			}
			if(_canBash) RotateArrow();
		}
		else if(!IsRightMouseButtonReleased() && _canBash)
		{
			StartTime();
			DeactivateArrow();
			Throw();
 			DeactivateBash();
        }
	}

	private IEnumerator Counter()
	{
		float pauseTime = Time.realtimeSinceStartup + MaxBashTime;
		while(Time.realtimeSinceStartup < pauseTime)
		{
			yield return null;
		}
		if(IsTimeStopped())
		{
			StartTime();
			DeactivateBash();
			DeactivateArrow();
            _asteroidComponent.UpdateAsteroidLayer("Asteroids");
        }
	}

	private void Throw()
	{
		_throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		_throwDirection = _throwDirection.normalized;
		if (_interactionObject != null)
        {
            _asteroidComponent.IsThrown = true;
            _asteroidComponent.ThrowDirection = _throwDirection;
            _asteroidComponent.ThrowSpeed = ThrowSpeed;
        }
	}

	private bool IsAsteroid(GameObject other)
	{
		return other.CompareTag("Asteroid");
	}

	private bool IsRightMouseButtonClicked()
	{
		return Input.GetButton(BashButton);
	}

	private bool IsRightMouseButtonReleased()
	{
		return Input.GetButtonUp(BashButton);
	}

	private void StopTime()
	{
		Time.timeScale = SlowMotionScale;
	}

	private void StartTime()
	{
		Time.timeScale = 1;
	}

	private bool IsTimeStopped()
	{
		return Time.timeScale == SlowMotionScale;
	}

	private void ActivateBash()
	{
		_canBash = true;
    }

	private void DeactivateBash()
	{
		_canBash = false;
    }

	private void DeactivateArrow()
	{
		Arrow.gameObject.SetActive(false);
	}

	private void ActivateArrow(GameObject other)
	{
		Arrow.position = other.transform.position;
		Arrow.gameObject.SetActive(true);
	}

	private void RotateArrow()
	{
		float rot_z = CalculateRotationDegree(CalculateVectorDifferenceBetweenMouseAndArrow());
		RotateArrowAroundZAxis(rot_z);
	}

	private Vector2 CalculateVectorDifferenceBetweenMouseAndArrow()
	{
		Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		return diff;
	}

	private float CalculateRotationDegree(Vector2 diff)
	{
		return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
	}

	private void RotateArrowAroundZAxis(float rot_z)
	{
		Arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
	}

  private void TurnOffRope()
  {
      _rope.HitPoint = new RaycastHit2D();
      _rope.Line.enabled = false;
  }
}