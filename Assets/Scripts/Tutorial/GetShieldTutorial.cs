using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShieldTutorial : Tutorial 
{
    public override void CheckIfHappening()
    {
        TutorialController.instance.player.SetActive(true);
        if (TutorialController.instance.player.GetComponentInChildren<ShieldDefence>())
        {
            TutorialController.instance.CompletedTutorial();
        }
    }
}