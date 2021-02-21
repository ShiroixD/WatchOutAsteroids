using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private float _autoDestroyTime  = 4.0f;
    [SerializeField] private TextMesh _destroyTimeCounter;

    private float _lastCountTime;
    private int _currentCountTime;
    private const float _timePeriod = 1.0f;     // time in seconds between counting remaining life time of footbridge
    private Collider2D _bridgeCollider;
    public bool _isPlayerPassing = false;

    void Start ()
    {
        _bridgeCollider = GetComponent<Collider2D>();
        _currentCountTime = (int)_autoDestroyTime;
        _destroyTimeCounter.text = _currentCountTime.ToString();
        _lastCountTime = Time.time;
        
        StartCoroutine(DelayedDestroyBridge());
    }

    void Update()
    {
        if (_isPlayerPassing)
            _bridgeCollider.isTrigger = true;
        else
            _bridgeCollider.isTrigger = false;

        float timeDifference = Time.time - _lastCountTime;

        if (timeDifference >= _timePeriod)
        {
            _lastCountTime = Time.time;
            _currentCountTime--;         
        }

        _destroyTimeCounter.text = _currentCountTime.ToString();
    }

    private IEnumerator DelayedDestroyBridge()
    {
        yield return new WaitForSeconds(_autoDestroyTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            collision.gameObject.GetComponent<Asteroid>();
            PointsController.instance.CalculatePoints(collision.GetComponent<Asteroid>().Points);
            collision.GetComponent<Asteroid>().DestroyMyself();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerPassing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(this.gameObject);
        }
    }
}
