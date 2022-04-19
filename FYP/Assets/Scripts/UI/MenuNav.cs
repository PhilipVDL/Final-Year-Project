using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuNav : MonoBehaviour
{
    public GameObject[] ScreeensFirstButtons;

    public GameObject[] screens;
    public bool currentScreen;
    public EventSystem nav;

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
            nav.GetComponent<EventSystem>().SetSelectedGameObject(ScreeensFirstButtons[1]);
        }
    }

    public void SetActiveButtonStart()
    {

        if (screens[1] == true)
        {
            nav.GetComponent<EventSystem>().SetSelectedGameObject(ScreeensFirstButtons[0]);
        }
    }

    public void SetActiveButtonPlay()
    {

        if (screens[0] == true)
        {
            nav.GetComponent<EventSystem>().SetSelectedGameObject(ScreeensFirstButtons[2]);
        }
    }
}
    
 
