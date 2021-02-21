using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTutorial : Tutorial 
{
    [SerializeField] GameObject[] _asteroid;

    private List<GameObject> _arrayAst = new List<GameObject>();
    private List<bool> _arrayAstFlag = new List<bool>();
    private int _shieldDurability = 6;


    public override void CheckIfHappening()
    {
        GameObject.FindGameObjectWithTag("Ground").GetComponent<LifesScript>().setLifes(6);

        if (!_init)
        {
            _init=InitTutorial();
        }

        for (int i = _arrayAst.Count - 1; i >= 0; i--)
        {
            if (_arrayAst[i].Equals(null))
            {
                if(_shieldDurability!= TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().Durability)
                {
                    _shieldDurability = TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().Durability;
                }
                else
                {
                    _arrayAstFlag[i] = true;
                }
                _arrayAst.RemoveAt(i);
            }
            else if (_arrayAst[i].transform.position.y > 20 || _arrayAst[i].transform.position.x > 40 || _arrayAst[i].transform.position.x < -40)
            {
                Destroy(_arrayAst[i]);
                _arrayAst.RemoveAt(i);
                _arrayAstFlag[i] = true;
            }

        }

        if (TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().Durability <= 3)
        {
            TutorialController.instance.CompletedTutorial();
        }
        else if (TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().Durability > 3 && _arrayAst.Count <3)
        {
            Replay();
        }
    }

    protected override void Replay()
    {
        Vector2[] _spawnPosition =
    {
            new Vector2(TutorialController.instance.trash.transform.position.x, TutorialController.instance.player.transform.position.y + 6),
            new Vector2(TutorialController.instance.trash.transform.position.x+8, TutorialController.instance.player.transform.position.y + 6),
            new Vector2(TutorialController.instance.trash.transform.position.x+18, TutorialController.instance.player.transform.position.y + 6)
        };

        for (int i=0; i< _arrayAstFlag.Count; i++)
        {
            if (_arrayAstFlag[i] == true)
            {
                GameObject go = Instantiate(_asteroid[i], _spawnPosition[i], Quaternion.identity);
                _arrayAst.Add(go);
                _arrayAstFlag[i] = false;
                if (_arrayAst[_arrayAst.Count-1] != null)
                    _arrayAst[_arrayAst.Count-1].SendMessage("OnStart", new float[] { 1.0f, 0.1f });
            }
        }
    }

    protected override bool InitTutorial()
    {
        TutorialController.instance.trash.SetActive(false);
        TutorialController.instance.player.SetActive(true);
        TutorialController.instance.arrow.SetActive(false);
        TutorialController.instance.arrowC.SetActive(false);

        for (int i = 0; i<3; i++)
        {
            Vector2[] _spawnPosition =
    {
            new Vector2(TutorialController.instance.trash.transform.position.x, TutorialController.instance.player.transform.position.y + 6),
            new Vector2(TutorialController.instance.trash.transform.position.x+8, TutorialController.instance.player.transform.position.y + 6),
            new Vector2(TutorialController.instance.trash.transform.position.x+18, TutorialController.instance.player.transform.position.y + 6)
        };
            GameObject go = Instantiate(_asteroid[i], _spawnPosition[i], Quaternion.identity);
            _arrayAst.Add(go);
            _arrayAstFlag.Add(false);
            if(_arrayAst[i]!=null)
                _arrayAst[i].SendMessage("OnStart", new float[] { 1.0f, 0.1f });
        }
        


        return true;
    }
}