using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void retryCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene(); SceneManager.LoadScene(currentScene.name);
    }

    public void nextLevel()
    {
        // load the nextlevel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void backToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
