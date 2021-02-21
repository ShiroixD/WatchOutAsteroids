using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarTutorial : Tutorial
{

    public override void CheckIfHappening()
    {
        TutorialController.instance.shieldBar.SetActive(true);
        if (Input.inputString.Contains(key))
        {
           TutorialController.instance.CompletedTutorial();
        }
    }

}