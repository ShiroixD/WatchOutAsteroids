using System.Collections.Generic;
using UnityEngine;

public class AsteroidNavigationController : MonoBehaviour
{
    [SerializeField] GameObject arrow;

    private float offsetX;
    
    private Dictionary<Asteroid, GameObject> _asteroids = new Dictionary<Asteroid, GameObject>();

    public float OffsetY { get; set; }
    public float OffsetX {
        get {
            return offsetX;
        }

        set {
            offsetX = value;
        }
    }

    private void Start()
    {
        Camera camera = Camera.main;
        OffsetY = camera.orthographicSize;
        OffsetX = camera.aspect * OffsetY;
    }

    public void DrawArrowRight(Asteroid asteroid)
    {
        if (_asteroids.ContainsKey(asteroid))
        {
            return;
        }

        GameObject tmp = (GameObject)Instantiate(
          arrow,
          new Vector2(transform.position.x + OffsetX, 0f),
          Quaternion.Euler(0f, 0f, -90f)
          );

        _asteroids.Add(asteroid, tmp);
    }

    public void DrawArrowLeft(Asteroid asteroid)
    {
        if (_asteroids.ContainsKey(asteroid))
        {
            return;
        }
        GameObject tmp = (GameObject)Instantiate(
          arrow,
          new Vector2(transform.position.x - OffsetX, asteroid.gameObject.transform.position.y),
          Quaternion.Euler(0f, 0f, 90f)
          );
        _asteroids.Add(asteroid, tmp);
    }

    public void DeleteArrow(Asteroid asteroid)
    {
        if (!_asteroids.ContainsKey(asteroid))
        {
            return;
        }
        Destroy(_asteroids[asteroid]);

        _asteroids.Remove(asteroid);
    }

    void LateUpdate()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        foreach (var item in _asteroids)
        {

            float asteroidYPos = item.Key.transform.position.y;
            float off;
            if (transform.position.x < item.Key.transform.position.x)
            {
                off = OffsetX;
            }
            else
            {
                off = -OffsetX;
            }
            item.Value.transform.position = new Vector2(transform.position.x + off, asteroidYPos);
        }
    }
}
