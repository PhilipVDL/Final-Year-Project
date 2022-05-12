using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public bool levelSelect;
    public bool characterSelect;
    public string sceneName;
    public string[] levelnames;
    public int currentLevel;

    private void Awake()
    {
        if (levelSelect)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneUnloaded += OnSceneUnloaded; //allows detecting when scene unloads
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        if (levelSelect)
        {
            levelSelect = false;
            characterSelect = true;
        }
        else if (characterSelect)
        {
            characterSelect = false;
        }
    }

    public void LoadThisScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void levelUp()
    {
        currentLevel++;
    }

    public void levelDown()
    {
        currentLevel--;
    }

    private void Update()
    {
        if (levelSelect)
        {
            if(currentLevel >= levelnames.Length)
            {
                currentLevel = 0;
            }
            else if(currentLevel < 0)
            {
                currentLevel = levelnames.Length - 1;
            }
        }
        else if (characterSelect)
        {
            sceneName = levelnames[currentLevel];
        }
    }
}