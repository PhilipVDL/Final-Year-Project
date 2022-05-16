using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerMenuNavigation : MonoBehaviour
{
    [Header("Main Menu Menus")]
    public GameObject main;
    public GameObject options;
    public GameObject mainCanvas;
    public GameObject optionsCanvas;
    [Header("Main Menu Buttons")]
    public GameObject mainDefault;
    public GameObject optionsOpenedDefault;
    public GameObject optionsClosedDefault;

    private void Update()
    {
        Reselect();
    }

    //Main Menu
    void Reselect()
    {
        //selects UI in case it gets deselected by accident
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            if (mainCanvas.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(mainDefault);
            }
            else if (optionsCanvas.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(optionsOpenedDefault);
            }
        }
    }

    public void OpenOptions()
    {
        //set correct menu
        main.SetActive(false);
        mainCanvas.SetActive(false);
        options.SetActive(true);
        optionsCanvas.SetActive(true);
        //set selected button
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsOpenedDefault);
    }

    public void CloseOptions()
    {
        main.SetActive(true);
        mainCanvas.SetActive(true);
        options.SetActive(false);
        optionsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedDefault);
    }
}