using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompts : MonoBehaviour
{
    public GameObject[] promptBoxes;
    public GameObject player;
    PlayerController controller;
    ObstacleInventory obsInv;

    public float duration;
    private float _duration = 3;

    private bool moveTut = false;
    private bool jumpTut = false;
    private bool hasJumped = false;
    private bool pickUpTut = false;

    void Start()
    {
        player = gameObject;
        controller = player.GetComponent<PlayerController>();
        obsInv = player.GetComponent<ObstacleInventory>();
    }

    void Prompts()
    {
        if (!moveTut)
        {
            promptBoxes[0].SetActive(true); //active moveTut at start
            moveTut = true;
        }
        else if(moveTut && !jumpTut && controller.speeding)
        {
            promptBoxes[1].SetActive(true); //activate jumpTut if player has moved after moveTut
            jumpTut = true;
        }
        else if(moveTut && jumpTut && !controller.grounded)
        {
            hasJumped = true;
            duration = _duration;
        }

        if (obsInv.obstacles.Count > 0)
        {
            if (obsInv.obstacles[0] != null && pickUpTut == false)
            {
                promptBoxes[2].SetActive(true); //activate pickupTut if player got a pickup
                pickUpTut = true;
                duration = _duration;
            }
        }

        /*
        if (controller.grounded != true && jumpTut == false)
        {
            promptBoxes[1].SetActive(true);
            jumpTut = true;
        }

        if (controller.speeding == true && moveTut == false)
        {
            promptBoxes[0].SetActive(true);
            moveTut = true;
        }
        */
    }

    void EndPrompts()
    {
        if(pickUpTut && duration > 0)
        {
            duration -= Time.deltaTime; //timer for disabling pickup Tut
        }
        else if(moveTut && jumpTut && hasJumped && duration > 0)
        {
            duration -= Time.deltaTime; //timer for disabling move and jump Tuts
        }
        else if(pickUpTut && duration <= 0)
        {
            promptBoxes[2].SetActive(false); //disable pickup Tut
            duration = 0;
        }
        else if (moveTut && jumpTut && duration <= 0)
        {
            promptBoxes[0].SetActive(false); //disable move and jump Tuts
            promptBoxes[1].SetActive(false);
            duration = 0;
        }

        /*
        if (duration <= 0)
        {
            promptBoxes[0].SetActive(false);
            promptBoxes[1].SetActive(false);
            //promptBoxes[2].SetActive(false);
            duration = 0;
        }
        */
    }

    void Update()
    {
        Prompts();
        EndPrompts();
    }
}