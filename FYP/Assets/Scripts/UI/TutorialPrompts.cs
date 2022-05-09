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

    private bool moveTut = false;
    private bool jumpTut = false;
    private bool pickUpTut = false;

    void Start()
    {
        player = gameObject;
        controller = player.GetComponent<PlayerController>();
        obsInv = player.GetComponent<ObstacleInventory>();
    }

    void Prompts()
    {
        if(controller.grounded != true && jumpTut == false)
        {
            promptBoxes[1].SetActive(true);
            jumpTut = true;
        }

        if (controller.speeding == true && moveTut == false)
        {
            promptBoxes[0].SetActive(true);
            moveTut = true;
        }

        if(obsInv.obstacles.Count > 0)
        {
            if (obsInv.obstacles[0] != null && pickUpTut == false)
            {
                promptBoxes[2].SetActive(true);
                pickUpTut = true;
            }
        }
    }

    void EndPrompts()
    {
        if(moveTut == true && jumpTut == true && duration > 0)
        {
            duration -= Time.deltaTime;
        }

        if (duration <= 0)
        {
            promptBoxes[0].SetActive(false);
            promptBoxes[1].SetActive(false);
            promptBoxes[2].SetActive(false);
            duration = 0;
        }
    }

    void Update()
    {
        Prompts();
        EndPrompts();
    }
}