using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject SetttingsScreen;

    public bool paused;


    void Update()
    {
      Pause();
    }

    

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            PauseScreen.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            PauseScreen.SetActive(false);
            SetttingsScreen.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }

       
    }

    
}
