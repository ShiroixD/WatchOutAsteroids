using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBarTutorial : Tutorial
{

    public override void CheckIfHappening()
    {
        TutorialController.instance.earthBar.SetActive(true);
        if (Input.inputString.Contains(key))
        {
            TutorialController.instance.player.SetActive(false);
            TutorialController.instance.CompletedTutorial();
        }
    }
}