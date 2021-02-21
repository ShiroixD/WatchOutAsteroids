using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHealth : MonoBehaviour {

    [SerializeField] int _maxBlockHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (--_maxBlockHealth <= 0)
            {
                Destroy(this.gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
