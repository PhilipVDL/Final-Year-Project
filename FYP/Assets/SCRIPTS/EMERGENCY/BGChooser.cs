using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChooser : MonoBehaviour
{
    LoadSceneScript loader;
    SpriteRenderer rend;
    public Sprite[] backgrounds;

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
        }
    }
}