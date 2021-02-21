using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShieldTutorial : Tutorial 
{
    public override void CheckIfHappening()
    {

        if(!_init)
        {
            _init = InitTutorial();
        }

        if (TutorialController.instance.player.GetComponentInChildren<ShieldDefence>().Durability == 6)
        {
            TutorialController.instance.CompletedTutorial();
        }
    }

    protected override bool InitTutorial()
    {
        TutorialController.instance.trash.SetActive(true);
        TutorialController.instance.arrowC.SetActive(true);
        return base.InitTutorial();
    }
}