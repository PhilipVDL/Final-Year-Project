using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUIManager : MonoBehaviour
{
    public GameObject[] promptBoxes;

    public float duration;
    private float _duration = 3;

    public bool moveTut = false;
    public bool jumpTut = false;
    public bool hasJumped = false;
    public bool moveJumpComplete = false;
    public bool pickUpTut = false;
    public bool pickUpComplete = false;

    void Update()
    {
        Prompts();
        EndPrompts();
    }

    void Prompts()
    {
        if (moveJumpComplete)
        {
            //do nothing
        }
        else if (moveTut && jumpTut && hasJumped && !moveJumpComplete)
        {
            duration = _duration;
            moveJumpComplete = true;
        }
        else if (moveTut && jumpTut)
        {
            promptBoxes[1].SetActive(true); //activate jumpTut if player has moved after moveTut
        }
        else if (moveTut)
        {
            promptBoxes[0].SetActive(true); //active moveTut at start
        }

        if (pickUpTut && !pickUpComplete)
        {
            promptBoxes[2].SetActive(true); //activate pickupTut if player got a pickup
            duration = _duration;
            pickUpComplete = true;
        }
    }

    void EndPrompts()
    {
        if (pickUpTut && duration > 0)
        {
            duration -= Time.deltaTime; //timer for disabling pickup Tut
        }
        else if (moveTut && jumpTut && hasJumped && duration > 0)
        {
            duration -= Time.deltaTime; //timer for disabling move and jump Tuts
        }
        else if (pickUpTut && duration <= 0)
        {
            promptBoxes[2].SetActive(false); //disable pickup Tut
            duration = 0;
        }
        else if (moveTut && jumpTut && hasJumped && duration <= 0)
        {
            promptBoxes[0].SetActive(false); //disable move and jump Tuts
            promptBoxes[1].SetActive(false);
            duration = 0;
        }
    }
}