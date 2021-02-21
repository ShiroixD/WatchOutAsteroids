using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Regeneration : MonoBehaviour {

    [SerializeField] float _repairTime;
    [SerializeField] Slider _repairTimeSlider;

    private GameObject _sliderParent;
    private float _startTimeOfRepair = 0;
    private bool _canRepair = false;
    private GameObject _objectToFollow;
    private ShieldDefence _shieldDefence;
    private bool _canPlaySound = true;
    private int _lastSecond;


    private void Start()
    {
        _sliderParent = _repairTimeSlider.transform.parent.gameObject;
        _repairTimeSlider.maxValue = _repairTime;
        _lastSecond = DateTime.Now.Second;
    }

    private void Update()
    {
        if (_objectToFollow != null)
        {
            _sliderParent.transform.position = _objectToFollow.transform.position + new Vector3(-4f, 2f, 0);
            PlayRepairSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        _shieldDefence = shield != null ? shield.GetComponent<ShieldDefence>() : null;
        if (collision.gameObject.CompareTag("Player") && _shieldDefence != null && _shieldDefence.IsShieldDamaged())
        {
            _canRepair = true;
            _startTimeOfRepair = Time.time;
            _sliderParent.gameObject.SetActive(true);
            _objectToFollow = collision.gameObject;
            _repairTimeSlider.value = 0;
            _shieldDefence.ToggleRepairAnimation(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float deltaTime = Time.time - _startTimeOfRepair;
            _repairTimeSlider.value = deltaTime;
            if (_canRepair && deltaTime > _repairTime)
            {
                if (_shieldDefence != null)
                {
                    _shieldDefence.RepairShield();
                    _shieldDefence.ToggleRepairAnimation(false);
                }
                _canRepair = false;
                _objectToFollow = null;
                _sliderParent.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canRepair = false;
            _sliderParent.SetActive(false);
            _objectToFollow = null;
            _startTimeOfRepair = 0;
            if (_shieldDefence != null)
            {
                _shieldDefence.ToggleRepairAnimation(false);
            }
        }
    }

    private void PlayRepairSound()
    {
        if (LastSecondChanged())
        {
            _lastSecond = DateTime.Now.Second;
            _canPlaySound = true;
        }
        if (_canPlaySound)
        {
            SoundManager.instance.PlaySound("HammerToShield");
            _canPlaySound = false;
        }
    }

    private bool LastSecondChanged()
    {
        return _lastSecond != DateTime.Now.Second;
    }
}
