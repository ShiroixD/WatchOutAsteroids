using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _createTime;
    private Animator anim;

    void Start ()
    {
        _createTime = Time.time;
    }
	
	void Update ()
    {
        if (Time.time - _createTime > 2.0f)
            Destroy(gameObject);

    }
}
