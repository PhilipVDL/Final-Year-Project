using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChooser : MonoBehaviour
{
    public LoadSceneScript loader;
    SpriteRenderer rend;
    public Sprite[] backgrounds;
    public GameObject[] levelName;
    public GameObject[] difficulty;

    private void Start()
    {
        loader = GameObject.Find("LevelLoader").GetComponent<LoadSceneScript>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(loader.currentLevel >= 0 && loader.currentLevel < backgrounds.Length)
        {
            rend.sprite = backgrounds[loader.currentLevel];
            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (i == loader.currentLevel)
                {
                    levelName[i].SetActive(true);
                    difficulty[i].SetActive(true);
                }
                else
                {
                    levelName[i].SetActive(false);
                    difficulty[i].SetActive(false);
                }
            }
        }
    }
}