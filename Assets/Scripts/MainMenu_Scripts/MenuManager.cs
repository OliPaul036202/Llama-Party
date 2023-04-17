using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator levelSelector;


    public void startGame()
    {
        levelSelector.SetBool("isSlideIn", true);
    }

    public void backToMenu()
    {
        levelSelector.SetBool("isSlideIn", false);
    }

    public void endgame()
    {
        Application.Quit();
    }

    public void startTheEscape()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
