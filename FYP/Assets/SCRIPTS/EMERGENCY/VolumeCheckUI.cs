using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCheckUI : MonoBehaviour
{
    SingletonFinder singletonFinder;
    Slider slider;
    public bool bgm, sfx;

    private void Start()
    {
        singletonFinder = GameObject.Find("SingletonFinder").GetComponent<SingletonFinder>();
        slider = GetComponent<Slider>();

        if (bgm)
        {
            slider.value = singletonFinder.CheckBGM();
        }
        else if (sfx)
        {
            slider.value = singletonFinder.CheckSFX();
        }
    }
}