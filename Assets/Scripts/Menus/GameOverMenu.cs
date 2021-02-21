using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        int _score = PointsController.PlayerPoints;
        scoreText.text = _score.ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameBoard");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
