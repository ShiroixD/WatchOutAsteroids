using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAsteroid : MonoBehaviour {

    public Rope _rope;
    [SerializeField] float _pullSpeed = .5f;
    [SerializeField] float _slowmotionTime = 1.5f;
    
    void Start()
    {
        _rope = GetComponent<Rope>();
    }
    
    void Update()
    {
        if(IsSmallAsteroid())
        {
            PullAsteroidToPlayer();
        }
        else if(IsLargeAsteroid())
        {
            PullPlayerToAsteroid();
        }
        else if(_rope.HitPoint.collider == null)
        {
            _rope.Line.enabled = false;
        }
    }

    private IEnumerator Counter(Action action, float time)
    {
        float pauseTime = Time.realtimeSinceStartup + time;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return null;
        }
        action.Invoke();
    }

    private void PullPlayerToAsteroid()
    {
        if (_rope.HitPoint.collider != null)
        {

            if(_rope.IsRopeAttached)
            {
                Pull();
                Slowmotion();
                StartCoroutine(Counter(() => 
                {
                    StartTime();
                    _rope.HitPoint = new RaycastHit2D();
                    _rope.Rigidbody.gravityScale = 1;
                    _rope.Line.enabled = false;
                }, 
                    _slowmotionTime));
            }
        }
        else
        {
            _rope.HitPoint = new RaycastHit2D();
            _rope.Line.enabled = false;
        }
    }

    private void PullAsteroidToPlayer()
    {
        if(_rope.HitPoint.collider != null)
        {
            if(_rope.IsRopeAttached)
            {
                var asteroidRB = _rope.HitPoint.collider.gameObject.GetComponent<Rigidbody2D>();
                if(asteroidRB!=null)
                asteroidRB.MovePosition(Vector2.Lerp(asteroidRB.position, transform.position, 0.1f));

            }
        }
        else
        {
            _rope.HitPoint = new RaycastHit2D();
            _rope.Line.enabled = false;
        }
    }

    private void Pull()
    {
        _rope.Rigidbody.gravityScale = 0;
        _rope.Rigidbody.transform.position = Vector2.MoveTowards(transform.position, _rope.HitPoint.collider.transform.position, _pullSpeed);
    }


    private void StartTime()
    {
        Time.timeScale = 1;
    }    

    private void Slowmotion()
    {
        Time.timeScale = 0.1f;
    }

    private bool IsLargeAsteroid()
    {
        if (_rope.HitPoint.collider != null)
            return _rope.HitPoint.collider.gameObject.name.Contains("LargeAsteroid");

        return false;
    }

    private bool IsSmallAsteroid()
    {
        if (_rope.HitPoint.collider != null)
        {
            return _rope.HitPoint.collider.gameObject.name.Contains("SmallAsteroid");
        }
        return false;
    }
}
