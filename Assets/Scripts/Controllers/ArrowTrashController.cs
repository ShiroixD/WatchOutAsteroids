using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowTrashController : MonoBehaviour
{
    [SerializeField] private float _deltaArrowMove;
    [SerializeField] private float _shieldLevel;
    [SerializeField] private float _movingSpeed;

    [Header("Prefabs")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _arrow;

    private ShieldDefence _shield;
    private bool _isNotVisible;
    private float _arrowMove;
    private float _startY;
    private Text _adviceText;
    void Start()
    {
        _arrow = GameObject.FindGameObjectWithTag("ArrowTrash");
        _arrowMove = 0.1f;
        _arrow.transform.position = transform.position + new Vector3(0, 7.5f, 0);
        _startY = _arrow.gameObject.transform.localPosition.y;
        _adviceText = _arrow.GetComponentInChildren<Text>();
    }

    
    void Update()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Shield");
        if (temp != null) _shield = temp.GetComponent<ShieldDefence>();
        else _shield = null;
        DisplayHintArrow();
        MoveHintArrow();
    }

    private void DisplayHintArrow()
    {
        if (IfShieldFull())
        {
            _arrow.gameObject.SetActive(false);
            _isNotVisible = true;
        }
        else if (IfShieldDamage())
        {
            _adviceText.text = "Repair shield";
            _arrow.gameObject.SetActive(true);
            _isNotVisible = false;
        }
        else
        {
            _adviceText.text = "Equip shield";
            _arrow.gameObject.SetActive(true);
            _isNotVisible = false;
        }

    }

    private void MoveHintArrow()
    {
        if (!_isNotVisible)
        {
            float x = _arrow.gameObject.transform.localPosition.x;
            float y = _arrow.gameObject.transform.localPosition.y;

            _arrow.gameObject.transform.localPosition = new Vector3(x, y + (_arrowMove * Time.deltaTime * _movingSpeed), 0);

            if (IfMaxAmplitude())
            {
                _startY = _arrow.gameObject.transform.localPosition.y;
                _arrowMove = _arrowMove * -1;
            }
        }
    }

    private bool IfMaxAmplitude()
    {
        return ((_startY - _arrow.gameObject.transform.localPosition.y) >= _deltaArrowMove) || ((_startY - _arrow.gameObject.transform.localPosition.y) <= -_deltaArrowMove);
    }

    private bool IfShieldFull()
    {
        return (_shield != null && _shield.Durability >= _shield.MaxStamina * (_shieldLevel / 100));
    }

    private bool IfShieldDamage()
    {
        return (_shield != null && _shield.Durability < _shield.MaxStamina * (_shieldLevel / 100) && _shield.Durability > 0);
    }
}