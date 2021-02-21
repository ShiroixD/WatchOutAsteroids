using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCamInfoTutorial : Tutorial 
{
    public override void CheckIfHappening()
    {
        TutorialController.instance.trash.SetActive(true);
        
        if (Input.inputString.Contains(key))
        {
            TutorialController.instance.CompletedTutorial();
        }
    }
}