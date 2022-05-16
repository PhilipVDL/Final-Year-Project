using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public static LoadSceneScript Instance { get; private set; }

    public bool levelSelect;
    public bool characterSelect;
    public string sceneName;
    public string[] levelnames;
    public int currentLevel;

    private void Awake()
    {
        //Singleton Pattern
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

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

    public void LoadThisScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadCurrentScene()
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
        if (currentLevel >= levelnames.Length)
        {
            currentLevel = 0;
        }
        else if (currentLevel < 0)
        {
            currentLevel = levelnames.Length - 1;
        }

        sceneName = levelnames[currentLevel];
    }
}