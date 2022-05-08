using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject SetttingsScreen;

   
    void Update()
    {
        Pause();   
    }

    public void Pause()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        if((Input.GetKey(KeyCode.Escape) && PauseScreen == true))
        {
            PauseScreen.SetActive(false);
        }
        else if(Input.GetKey(KeyCode.Escape) && SetttingsScreen == true)
        {
            SetttingsScreen.SetActive(false);
        }

        
            
    }
}
