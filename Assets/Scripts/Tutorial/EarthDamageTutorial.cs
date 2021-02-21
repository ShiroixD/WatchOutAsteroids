using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDamageTutorial : Tutorial
{
    [SerializeField] GameObject _asteroid;

    private GameObject _ast;
    public override void CheckIfHappening()
    {
        if (!_init)
        {
            TutorialController.instance.player.SetActive(false);
            _init = InitTutorial();
        }

        if (Input.inputString.Contains(key) && _init)
        {
            if (_ast != null)
                Destroy(_ast);
            TutorialController.instance.player.SetActive(false);
            TutorialController.instance.CompletedTutorial();
        }
    }

    protected override bool InitTutorial()
    {
        Vector2 spawnPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 4);
        _ast = Instantiate(_asteroid, spawnPosition, Quaternion.identity);
        _ast.SendMessage("OnStart", new float[] { 1.0f, 0.5f });
        return true;
    }
}