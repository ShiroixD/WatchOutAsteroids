using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashTutorial : Tutorial
{
    [SerializeField] GameObject _asteroid;

    private List<GameObject> _arrayAst = new List<GameObject>();

    public override void CheckIfHappening()
    {
        if (!_init)
        {
            TutorialController.instance.player.SetActive(true);
            _init = InitTutorial();
        }

        for (int i = _arrayAst.Count - 1; i >= 0; i--)
        {
            if (_arrayAst[i].Equals(null))
                _arrayAst.RemoveAt(i);
            else if (_arrayAst[i].transform.position.y > 20 || _arrayAst[i].transform.position.x > 40 || _arrayAst[i].transform.position.x < -40)
            {
                Destroy(_arrayAst[i]);
                _arrayAst.RemoveAt(i);
            }

        }

        if (_arrayAst.Count == 0)
            TutorialController.instance.CompletedTutorial();
        else if (_arrayAst.Count == 1)
            Replay();

    }

    protected override void Replay()
    {
        for (int i = _arrayAst.Count - 1; i >= 0; i--)
            if (_arrayAst[i].Equals(null))
            {
                _arrayAst.RemoveAt(i);
            }
            else
            {
                Destroy(_arrayAst[i]);
                _arrayAst.RemoveAt(i);
            }

        _init = false;

    }

    protected override bool InitTutorial()
    {
        Vector2 spawnPosition = new Vector2(TutorialController.instance.player.transform.position.x, TutorialController.instance.player.transform.position.y+6);
        Vector2 spawnPosition2 = new Vector2(TutorialController.instance.player.transform.position.x+4, TutorialController.instance.player.transform.position.y+10);
        GameObject go = Instantiate(_asteroid, spawnPosition, Quaternion.identity);

        _arrayAst.Add(go);
        go = Instantiate(_asteroid, spawnPosition2, Quaternion.identity);
        _arrayAst.Add(go);

        _arrayAst[0].SendMessage("OnStart", new float[] { 1.0f, 0.1f });
        _arrayAst[1].SendMessage("OnStart", new float[] { 1.0f, 0.1f });

        return true;
    }
}