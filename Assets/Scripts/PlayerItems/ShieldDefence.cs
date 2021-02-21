using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDefence : MonoBehaviour
{

    [SerializeField] int _lifesPerHealth = 2;
    [SerializeField] int _startHeart = 3;

    [SerializeField] private Sprite[] _shieldSprite;

    private Image[] _shieldHealthImage;


    private GameObject _hammerRepairing;

    private int _durability;
    public int Durability {
        get {
            return _durability;
        }

        set {
            _durability = value;
        }
    }

    private int _maxStamina;
    public int MaxStamina {
        get {
            return _maxStamina;
        }

        set {
            _maxStamina = value;
        }
    }

    private void Start()
    {
        _shieldHealthImage = GameObject.FindGameObjectWithTag("ShieldBar").GetComponentsInChildren<Image>();
        Durability = MaxStamina = _startHeart * _lifesPerHealth;
        _hammerRepairing = transform.Find("HammerRepairing").gameObject;
        ChangeLife();
    }

    public bool IsShieldDamaged()
    {
        return Durability < MaxStamina;
    }

    public void RepairShield()
    {
        Durability = MaxStamina;
        ChangeLife();
    }

    public void TakeDamage(int damage)
    {
        Durability -= damage;
        ChangeLife();

        if (Durability <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void ChangeLife()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in _shieldHealthImage)
        {
            if (empty)
            {
                image.sprite = _shieldSprite[0];
            }
            else
            {
                i++;
                if (Durability >= i * _lifesPerHealth)
                {
                    image.sprite = _shieldSprite[_shieldSprite.Length - 1];
                }
                else
                {
                    int currentHealthHeart = (int)(_lifesPerHealth - (_lifesPerHealth * i - Durability));
                    int healthPerImage = _lifesPerHealth / (_shieldSprite.Length - 1);
                    int imageIndex = currentHealthHeart / healthPerImage;
                    image.sprite = _shieldSprite[imageIndex];
                    empty = true;
                }
            }
        }
    }


    public void ToggleRepairAnimation(bool state)
    {
        _hammerRepairing.SetActive(state);
    }
}
