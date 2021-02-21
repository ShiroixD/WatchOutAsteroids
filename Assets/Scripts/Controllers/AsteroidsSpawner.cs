using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour {

    #region Fields
    [SerializeField] GameObject Asteroid;
    [SerializeField] float _patternDelay = 2f;
    [SerializeField] float _spawnDelay;
    private List<GameObject> _asteroids;

    [Header("Special asteroids")]
    [SerializeField] GameObject OrangeAsteroid;
    [SerializeField] GameObject RedAsteroid;
    [SerializeField] float _specialAsteroidDelay;
    [SerializeField] float _specialAsteroidStartDelay;

    [Header("Dificulty Rates")]
    [SerializeField] float _spawnDelayDifficultyModifier = 0.06f;
    [SerializeField] float _patternDelayDifficultyModifier = 0.06f;
    [SerializeField] float _speedAsteroidModifier;
    [SerializeField] float _spawnDelayThreshold = 0.55f;
    [SerializeField] float _patternDelayThreshold = 0.75f;
    [SerializeField] float _difficulty = 1.0f;
    [SerializeField] float _timeSpanDifficultyIncrease;

    private float _leftMargin;
    private float _rightMargin;
    private float _upMargin;
    private float _startDifficultyLevel;

    private List<Vector2> _positions;

    [SerializeField] List<List<string>> _patterns;
    #endregion

    // Use this for initialization
    void Start () {
        _leftMargin = gameObject.transform.position.x;
        _upMargin = gameObject.transform.position.y - 3;
        _rightMargin = _leftMargin + GetComponent<BoxCollider2D>().size.x;
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);        
        
        InitPositions();
        StartCoroutine(SpawnAsteroids());
        InvokeRepeating("IncreaseDifficulty", 30f, 30f);
	}

    void InitPositions()
    {
        _positions = new List<Vector2>
        {
            new Vector2(_leftMargin, _upMargin),
            new Vector2((_leftMargin + _rightMargin) / 2f, _upMargin),
            new Vector2(_rightMargin, _upMargin)
        };

        _patterns = new List<List<string>>
        {
            new List<string> { "0 11", "2 -25"},
            new List<string> { "0 24", "2 -25"},
            new List<string> { "0 35", "1 15", "0 27", "2 -15"},
            new List<string> { "2 -3", "0 31", "2 -21.37", "0 15"},
            new List<string> { "1 15", "1 -15", "1 -10", "2 -15", "1 11", "0 10"},
            new List<string> { "2 -9", "1 -12, 0 12"}
        };

        _asteroids = new List<GameObject>
        {
            OrangeAsteroid,
            Asteroid,
            RedAsteroid
        };
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(_patternDelay);

        while(true)
        {
            var pattern = _patterns[UnityEngine.Random.Range(0, _patterns.Count)];
            foreach(var item in pattern)
            {
                var config = item.Split(' ');
                Vector2 spawnPosition = _positions[int.Parse(config[0])];
                float angle = float.Parse(config[1]);
                GameObject go = Instantiate(RandomAsteroidType(), spawnPosition, Quaternion.identity);
                go.SendMessage("OnStart", new float[] { angle, _difficulty });
                yield return new WaitForSeconds(_spawnDelay);
            }

            yield return new WaitForSeconds(_patternDelay);
        }
    }

    void IncreaseDifficulty()
    {
        _difficulty += _speedAsteroidModifier;
        if (_patternDelay >= _patternDelayThreshold)
        {
            _patternDelay -= _patternDelayDifficultyModifier;
        }
        if (_spawnDelay >= _spawnDelayThreshold)
        {
            _spawnDelay -= _spawnDelayDifficultyModifier;
        }
    }

    GameObject RandomAsteroidType()
    {
        return _asteroids[UnityEngine.Random.Range(0, _asteroids.Count)];
    }
}
