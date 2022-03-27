using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    public Slider jumpBar;
    public GameObject jumpBarObj;

    public float maxFill;
    public float minFill;
    public float currentFill;

    public static JumpBar instance;

    void Start()
    {
        PlayerController controller = GetComponentInParent<PlayerController>();
        maxFill = controller.maxJumpForce;
        minFill = controller.minJumpForce;
        currentFill = controller.currentJumpForce;

        jumpBar.maxValue = maxFill;
        jumpBar.minValue = minFill;
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