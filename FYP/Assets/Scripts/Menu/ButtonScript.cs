using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
  
 public void StartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
