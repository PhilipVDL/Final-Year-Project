using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    public Slider jumpBar;

    private float maxFill;
    private float currentFill;

    public static JumpBar instance;
    private void Awake()
    {
       
    }

    void Start()
    {
        maxFill = GetComponentInParent<PlayerController>().maxJumpForce;
        currentFill = GetComponentInParent<PlayerController>().currentJumpForce;

        jumpBar.maxValue = maxFill;
        jumpBar.value = currentFill;
    }

    private void Update()
    {
        currentFill += GetComponentInParent<PlayerController>().currentJumpForce / 100;
        jumpBar.value = currentFill;
        currentFill = GetComponentInParent<PlayerController>().currentJumpForce;
    }
}


