using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonFinder : MonoBehaviour
{
    public LoadSceneScript loader;
    public AudioScript audioController;

    private void Start()
    {
        if(GameObject.Find("LevelLoader") != null)
        {
            loader = GameObject.Find("LevelLoader").GetComponent<LoadSceneScript>();
        }
        
        if(GameObject.Find("Audio Control"))
        {
            audioController = GameObject.Find("Audio Control").GetComponent<AudioScript>();
        }
    }

    public void CallLoadThisScene(string name)
    {
        loader.LoadThisScene(name);
    }

    public void CallLoadCurrentScene()
    {
        loader.LoadCurrentScene();
    }

    public void CallLevelUp()
    {
        loader.levelUp();
    }

    public void CallLevelDown()
    {
        loader.levelDown();
    }

    public void CallUpdateBGM(float volume)
    {
        audioController.UpdateBGM(volume);
    }

    public void CallUpdateSFX(float volume)
    {
        audioController.UpdateSFX(volume);
    }

    public float CheckBGM()
    {
        return audioController.musicVolume;
    }

    public float CheckSFX()
    {
        return audioController.sfxVolume;
    }
}