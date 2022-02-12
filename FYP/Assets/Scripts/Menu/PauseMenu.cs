using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
     void Update()
    {
        Pause();   
    }

    public void Pause()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
       
    }
}
