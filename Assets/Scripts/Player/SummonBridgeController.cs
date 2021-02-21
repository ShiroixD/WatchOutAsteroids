using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonBridgeController : MonoBehaviour
{
    [SerializeField] private GameObject _bridge;
    [SerializeField] private GameObject _bridgeShadow;
    [SerializeField] private float _loadSkillTime = 7.0f; // time to load one use of skill 
    [SerializeField] private TextMeshProUGUI _useBridgeText;
    [SerializeField] private TextMeshProUGUI _maxUseBridgeText;
    [SerializeField] private TextMeshProUGUI _cantUseBridgeText;
    [SerializeField] private Image _timerMask;

    private int _maxUse = 3;
    private float _lastUse;
    private static int _currentUse;
    private bool _bridgeShowedShadow = false;
    private GameObject _lastBridgeShadow;

    public int CurrentUse
    {
        get
        {
            return _currentUse;
        }

        set
        {
            _currentUse = value;
        }
    }

    void Start ()
    {
        CurrentUse = 0;
        _useBridgeText.text = CurrentUse.ToString();
        _maxUseBridgeText.text = _maxUse.ToString();
        _cantUseBridgeText.enabled = false;
        _lastUse = Time.time;
    }
	
	void Update ()
    {
        float timeDifference = Time.time - _lastUse;

        if (CurrentUse < _maxUse)
            CheckUsage(timeDifference);

        if (_bridgeShowedShadow)
        {
            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
            _lastBridgeShadow.transform.position = playerPos;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SummonBridge(timeDifference);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _bridgeShowedShadow = false;
            Destroy(_lastBridgeShadow);
        }

    }

    private void CheckUsage(float timeDiff)
    {
        if (_currentUse == 0)
        {
            _cantUseBridgeText.enabled = true;
        } else
        {
            _cantUseBridgeText.enabled = false;
        }

        var fill = timeDiff / _loadSkillTime;
        _timerMask.fillAmount = fill;

        if (timeDiff >= _loadSkillTime)
        {
            CurrentUse += 1;
            _lastUse = Time.time;
            _timerMask.fillAmount = 0f;
        }

        _useBridgeText.text = CurrentUse.ToString();
    }

    public void SummonBridge(float timeDiff)
    {
        if (!_bridgeShowedShadow)
        {
            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
            _lastBridgeShadow = Instantiate(_bridgeShadow, playerPos, Quaternion.identity);
            _bridgeShowedShadow = true;
        } else if (CurrentUse > 0 && timeDiff > 0.5f)
        {
            Destroy(_lastBridgeShadow);
            _bridgeShowedShadow = false;
            --CurrentUse;
            Vector3 playerPos = new Vector3(transform.position.x,transform.position.y + 3.0f, transform.position.z);
            Instantiate(_bridge, playerPos, Quaternion.identity);
            _lastUse = Time.time;
        }
    }
}
