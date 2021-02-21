using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInfoTutorial : Tutorial 
{

    public override void CheckIfHappening()
    {
        if(!_init)
        {
            _init = InitTutorial();
        }
       

        if (Input.inputString.Contains(key))
        {
            TutorialController.instance.CompletedTutorial();
        }
    }

    protected override bool InitTutorial()
    {
        TutorialController.instance.arrowC.SetActive(true);
        return true;
    }
}