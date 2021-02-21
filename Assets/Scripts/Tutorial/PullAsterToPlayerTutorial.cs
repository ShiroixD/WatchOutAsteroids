using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAsterToPlayerTutorial : Tutorial {
    [SerializeField] GameObject _asteroid;

    GameObject _created;

    private float _distance;
    private bool _wasCreated = false;
    private bool _wasPulled = false;
    private bool _wasDone = false;
    public override void CheckIfHappening()
    {
        if (!_init)
        {
            _init = InitTutorial();
        }

        if (_created)
        {
            _distance = Vector2.Distance((Vector2)_created.transform.position, (Vector2)TutorialController.instance.player.transform.position);

            if (_distance < 2.5)
            {
                _wasPulled = true;
            }
        }

        if (_wasPulled)
        {
            TutorialController.instance.expText.text = "Well done! Press "+"f"+" or left mouse button to continue.";
            _wasDone = true;
        }

        if (_wasDone && (Input.inputString.Contains(key)|| Input.GetKey(KeyCode.Mouse0)))
        {
            Destroy(_created);
            TutorialController.instance.CompletedTutorial();
        } 
        if(!_created.GetComponent<Renderer>().isVisible)
        {
            Replay();
        }
    }
    protected override bool InitTutorial()
    {
        TutorialController.instance.player.SetActive(true);
        Vector2 spawnPosition = new Vector2(TutorialController.instance.player.transform.position.x + 10, TutorialController.instance.player.transform.position.y + 13);
        _created = Instantiate(_asteroid, spawnPosition, Quaternion.identity);
        _created.SendMessage("OnStart", new float[] { 0.0f, 0.0f });
        _wasCreated = true;
        return true;
    }
    protected override void Replay()
    {
        if (_created != null)
        {
            Destroy(_created);
        }
        TutorialController.instance.player.SetActive(true);
        Vector2 spawnPosition = new Vector2(TutorialController.instance.player.transform.position.x + 10, TutorialController.instance.player.transform.position.y + 13);
        _created = Instantiate(_asteroid, spawnPosition, Quaternion.identity);
        _created.SendMessage("OnStart", new float[] { 0.0f, 0.0f });
        _wasCreated = true;
    }
}
