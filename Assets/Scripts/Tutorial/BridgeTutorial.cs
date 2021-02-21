using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTutorial : Tutorial
{
    [SerializeField] GameObject _asteroid;
    private GameObject _go;
    private bool _badDestroyFlags;

    public override void CheckIfHappening()
    {
        if (!_init)
        {
            TutorialController.instance.player.SetActive(true);
            _init = InitTutorial();
            _badDestroyFlags = false;
            TutorialController.instance.player.GetComponent<SummonBridgeController>().CurrentUse = 3;
            TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().RepairShield();
        }

       Debug.Log(TutorialController.instance.player.GetComponent<SummonBridgeController>().CurrentUse);

        if(_init)
        {
            if (_go != null)
            {
                if (_go.transform.position.y > 20 || _go.transform.position.x > 30 || _go.transform.position.x < -30)
                {
                    _badDestroyFlags = true;
                    Destroy(_go);
                }
            }
            else if (_go == null && (TutorialController.instance.player.GetComponent<SummonBridgeController>().CurrentUse == 3 || TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().Durability<6))
            {
                _badDestroyFlags = true;
            }

            

            if (_go == null && _badDestroyFlags == false && TutorialController.instance.player.GetComponent<SummonBridgeController>().CurrentUse < 3)
            {
                TutorialController.instance.CompletedTutorial();
            }
            else if (_badDestroyFlags)
                _init = false;
        }
    }

    protected override bool InitTutorial()
    {
        Vector2 spawnPosition = new Vector2(TutorialController.instance.player.transform.position.x, TutorialController.instance.player.transform.position.y + 5);
        _go = Instantiate(_asteroid, spawnPosition, Quaternion.identity);
        _go.SendMessage("OnStart", new float[] { 1.0f, 0.1f });

        return true;
    }
}