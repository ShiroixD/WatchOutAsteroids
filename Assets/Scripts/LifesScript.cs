using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifesScript : MonoBehaviour
{
    private int _lifes;    
    [SerializeField] private int _lifesPerHealth;
    [SerializeField] private int _startHeart = 3;

    private Image[] _earthImage;
    [SerializeField] private Sprite[] _earthSprite;

    public void setLifes(int value)
    {
        _lifes = value;
        _lifes = Mathf.Clamp(_lifes, 0, _startHeart * _lifesPerHealth);
    }

    private void Start()
    {
        _earthImage = GameObject.FindGameObjectWithTag("EarthBar").GetComponentsInChildren<Image>();
        _lifes = _startHeart * _lifesPerHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
            TakeDamage(1);

            if (_lifes <= 0)
            {
                Destroy(gameObject);
            }
        }

        ChangeLife();

        if (_lifes <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void TakeDamage(int lifesDamage)
    {
        _lifes -= lifesDamage;
        _lifes = Mathf.Clamp(_lifes, 0, _startHeart * _lifesPerHealth);
    }

    private void ChangeLife()
    {
        bool empty = false;
        int i = 0;

        foreach(Image image in _earthImage)
        {
            if (empty)
            {
                image.sprite = _earthSprite[0];
            }
            else
            {
                i++;
                if(_lifes >= i * _lifesPerHealth)
                {
                    image.sprite = _earthSprite[_earthSprite.Length - 1];
                }
                else
                {
                    int currentHealthHeart = (int)(_lifesPerHealth - (_lifesPerHealth * i - _lifes));
                    int healthPerImage = _lifesPerHealth / (_earthSprite.Length - 1);
                    int imageIndex = currentHealthHeart / healthPerImage;
                    image.sprite = _earthSprite[imageIndex];
                    empty = true;
                }
            }
        }
    }
}
