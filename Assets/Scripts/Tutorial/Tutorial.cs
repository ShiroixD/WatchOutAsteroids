using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour 
{

    public int Order;

    [TextArea(3,10)]
    public string Explanation;

    public string key = "f";
    protected bool _init = false; 

    void Awake()
    {
        TutorialController.instance.tutorials.Add(this);
    }

    public virtual void CheckIfHappening() { }

    protected virtual void Replay() { }

    protected virtual bool InitTutorial() { return true; }


}