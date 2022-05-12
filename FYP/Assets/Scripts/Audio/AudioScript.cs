using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    public AudioSource bgm;
    public SFXScript sfx;

    private float musicVolume = 0.7f;
    private float sfxVolume = 0.7f;

    private void Awake()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; //allows detecting when scene unloads
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("BGM") != null)
        {
            bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        }

        if (GameObject.Find("SFX") != null)
        {
            sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
        }
    }
    
    void Update()
    {
        if (GameObject.Find("BGM") != null)
        {
            bgm.volume = musicVolume;
        }

        if (GameObject.Find("SFX") != null)
        {
            sfx.sfxVolume = sfxVolume;
        }   
    }

    public void UpdateBGM(float volume)
    {
        musicVolume = volume;
    }

    public void UpdateSFX(float volume)
    {
        sfxVolume = volume;
    }
}
