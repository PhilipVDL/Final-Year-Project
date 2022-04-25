using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuNav : MonoBehaviour
{
    public GameObject[] ScreeensFirstButtons;

    public GameObject[] screens;
    public bool currentScreen;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActiveButton()
    {

        if (screens[0] == true)
        {
            EventSystem.current.SetSelectedGameObject(ScreeensFirstButtons[1]);
        }
    }

    public void SetActiveButtonStart()
    {

        if (screens[1] == true || screens[2] == true)
        {
            EventSystem.current.SetSelectedGameObject(ScreeensFirstButtons[0]);
        }
        
    }

    public void SetActiveButtonPlay()
    {

        if (screens[0] == true)
        {
            EventSystem.current.SetSelectedGameObject(ScreeensFirstButtons[2]);
        }
    }

    
}
    
 
