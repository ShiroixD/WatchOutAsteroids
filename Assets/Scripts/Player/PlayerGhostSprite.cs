using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostSprite : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private float _timer = 0.1f;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = _player.transform.position;
        transform.localScale = _player.transform.localScale;
        _spriteRenderer.sprite = _player.GetComponent<SpriteRenderer>().sprite;
        _spriteRenderer.color = new Vector4(50, 50, 50, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
        _timer -= Time.deltaTime;
        if(_timer <=0)
        {
            Destroy(gameObject);
        }
	}
}
