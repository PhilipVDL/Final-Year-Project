using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    public Slider jumpBar;
    public GameObject jumpBarObj;

    private float maxFill;
    public float currentFill;

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
        ActivateBar();
        FillBar();

        
    }    
    

    void FillBar()
    {
        currentFill += GetComponentInParent<PlayerController>().currentJumpForce / 100;
        jumpBar.value = currentFill;
        currentFill = GetComponentInParent<PlayerController>().currentJumpForce;
    }

    void ActivateBar()
    {
        if(currentFill > 0)
        {
            jumpBarObj.SetActive(true);
        }
        else { jumpBarObj.SetActive(false); }
    }

    
}


