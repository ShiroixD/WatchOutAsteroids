using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovingTutorial : Tutorial
{
    public override void CheckIfHappening()
    {
       if (TutorialController.instance.player.transform.position.y > 10)
        {
            TutorialController.instance.CompletedTutorial();
        }
    }

}