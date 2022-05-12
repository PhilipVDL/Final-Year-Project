using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject SettingsScreen;

    private void Start()
    {
        PauseScreen = GameObject.Find("Pause Menu");
        SettingsScreen = GameObject.Find("Settings Menu");
        PauseScreen.SetActive(false);
        SettingsScreen.SetActive(false);
    }

    void Update()
    {
        Pause();   
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseScreen.activeInHierarchy && !SettingsScreen.activeInHierarchy)
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SettingsScreen.activeInHierarchy)
        {
            SettingsScreen.SetActive(false);
            PauseScreen.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseScreen.activeInHierarchy)
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1;
        }      
    }
}