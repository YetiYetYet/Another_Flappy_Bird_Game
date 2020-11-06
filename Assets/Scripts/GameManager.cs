using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int actualScore;
    public int scorePerObstacle = 10;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverMenu;
    
    void Start()
    {
        Time.timeScale = 1;
        actualScore = 0;
        gameOverMenu.SetActive(false);
        Screen.SetResolution(450, 800, false);
    }

    public void AddScore()
    {
        actualScore += scorePerObstacle;
        scoreText.text = actualScore.ToString();
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
