using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject[] Levels;

 public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        if (GameObject.Find("Pinball") == true)
        {
            SceneManager.LoadScene(1);
        }
        else if(GameObject.Find("SkyLevel") == true)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void NextButton()
    {
        if(Levels[1] == false)
            {
            Levels[1].SetActive(true);
            Levels[0].SetActive(false);
        }

        else if(Levels[1] == true)
        {
            Levels[0].SetActive(true);
            Levels[1].SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
