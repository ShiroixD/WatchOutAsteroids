using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : Singleton<PointsController>
{
    [SerializeField] float _comboTimeLimit = 4;       // maximum time combo
    [SerializeField] TextMeshProUGUI _pointsText;
    [SerializeField] TextMeshProUGUI _comboText;
    [SerializeField] Image _timerMask;

    private static int _playerPoints = 0;
    private float _lastHitTime;
    private int _hitCounter = 1;

    public static int PlayerPoints {
        get {
            return _playerPoints;
        }

        private set {
            _playerPoints = value;
        }
    }

    void Start()
    {
        _pointsText.text = PlayerPoints.ToString();
        _comboText.text = "";
        PlayerPoints = 0;
    }

    void Update ()
    {
        if (_hitCounter > 1)
        {
            float timeDifference = Time.time - _lastHitTime;
            var fill = timeDifference / _comboTimeLimit;
            _timerMask.fillAmount = fill;

            if (timeDifference < _comboTimeLimit)
            {
                _comboText.text = "x" + _hitCounter;
            }
            else
            {
                _hitCounter = 1;
                _comboText.text = "";
                _timerMask.fillAmount = 0f;
            }
        }

        _pointsText.text = PlayerPoints.ToString();

    }

    public void CalculatePoints(int points)
    {
        _lastHitTime = Time.time;
        if (_hitCounter <= 1)
        {
            PlayerPoints += points * _hitCounter;
            _hitCounter++;
        }
        else
        {
            PlayerPoints += _hitCounter * points;
            _hitCounter++;
        }
    }
}
