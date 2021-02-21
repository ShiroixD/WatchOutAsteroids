using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : Tutorial
{
    public override void CheckIfHappening()
    {
        if (!_init)
            _init = InitTutorial();

        if (Input.inputString.Contains(key))
        {
            TutorialController.instance.player.SetActive(true);
            TutorialController.instance.CompletedTutorial();
        }
    }

    protected override bool InitTutorial()
    {
        TutorialController.instance.player.SetActive(false);
        TutorialController.instance.arrow.SetActive(false);
        TutorialController.instance.arrowC.SetActive(false);
        TutorialController.instance.trashText.SetActive(false);
        TutorialController.instance.trash.SetActive(false);
        TutorialController.instance.earthBar.SetActive(false);
        TutorialController.instance.shieldBar.SetActive(false);
        TutorialController.instance.comboText.SetActive(false);
        TutorialController.instance.pointsText.SetActive(false);
        return true;
    }
}