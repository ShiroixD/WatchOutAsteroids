using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBridge : MonoBehaviour
{
    private Bridge _parentBridge;

	void Start ()
    {
        _parentBridge = GetComponentInParent<Bridge>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_parentBridge._isPlayerPassing)
        {
            _parentBridge._isPlayerPassing = true;
        }
    }
}
