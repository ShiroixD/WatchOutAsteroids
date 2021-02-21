using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : Singleton<TutorialController>
{

    [Header("Prefabs")]
    public GameObject player;
    public GameObject arrow;
    public GameObject arrowC;
    public GameObject trash;
    public GameObject earthBar;
    public GameObject shieldBar;
    public GameObject trashText;
    public GameObject pointsText;
    public GameObject comboText;


    public List<Tutorial> tutorials = new List<Tutorial>();
    public TextMeshProUGUI expText;

    private Tutorial currentTutorial;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrow = GameObject.FindGameObjectWithTag("ArrowTrash");
        arrowC = GameObject.FindGameObjectWithTag("ArrowShieldAdvice"); // arrow controler
        trash = GameObject.FindGameObjectWithTag("Trashcan");
        earthBar = GameObject.FindGameObjectWithTag("EarthBar");
        shieldBar = GameObject.FindGameObjectWithTag("ShieldBar");
        trashText = GameObject.Find("TrashcanShieldText");
        pointsText = GameObject.Find("PointsTextMesh");
        comboText = GameObject.Find("PointsMultiplierTextMesh");

        SetNextTutorial(0);
    }

    void Update()
    {
        if (currentTutorial)
        {
            currentTutorial.CheckIfHappening();
        }
    }

    IEnumerator delay(float _patternDelay)
    {
        yield return new WaitForSeconds(_patternDelay);
    }

    public void CompletedTutorial()
    {
        GameObject.FindGameObjectWithTag("Ground").GetComponent<LifesScript>().setLifes(6);
        SetNextTutorial(currentTutorial.Order + 1);
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }
        expText.text = currentTutorial.Explanation;
    }

    public void CompletedAllTutorials()
    {
        expText.text = "You have completed all tutorial.";

        SceneManager.LoadScene("GameBoard");

    }

    public Tutorial GetTutorialByOrder(int order)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].Order == order)
                return tutorials[i];
        }

        return null;
    }
}