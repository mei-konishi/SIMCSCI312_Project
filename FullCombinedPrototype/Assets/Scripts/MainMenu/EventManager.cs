using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    //public PlayerHealthManager playerHealth;

    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    private bool paused = false;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over Panel
        /*if (playerHealth.currentHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }*/
        
        // Pausing Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if (!paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void Resume()
    {
        paused = false;
    }

    public void RestartLevel(string levelName)
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(levelName);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

