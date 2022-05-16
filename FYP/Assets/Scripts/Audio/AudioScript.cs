using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    public static AudioScript Instance;

    public AudioSource bgm;
    public SFXScript sfx;

    public float musicVolume;
    public float sfxVolume;

    private bool startVolume = false;

    private void Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; //allows detecting when scene unloads
    }

    private void Start()
    {
        if (!startVolume)
        {
            musicVolume = 0.7f;
            sfxVolume = 0.7f;
            startVolume = true;
        }
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
