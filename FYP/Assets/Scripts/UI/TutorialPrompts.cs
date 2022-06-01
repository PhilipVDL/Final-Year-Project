using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompts : MonoBehaviour
{
    public PromptUIManager promptManager;
    public GameObject player;
    PlayerController controller;
    ObstacleInventory obsInv;

    void Start()
    {
        promptManager = GameObject.Find("Prompts").GetComponent<PromptUIManager>();
        player = gameObject;
        controller = player.GetComponent<PlayerController>();
        obsInv = player.GetComponent<ObstacleInventory>();
    }

    void Update()
    {
        PromptsBools();
    }

    void PromptsBools()
    {
        if (!promptManager.moveTut)
        {
            promptManager.moveTut = true;
        }
        else if(promptManager.moveTut && !promptManager.jumpTut && controller.speeding)
        {
            promptManager.jumpTut = true;
        }
        else if(promptManager.moveTut && promptManager.jumpTut && !controller.grounded)
        {
            promptManager.hasJumped = true;
        }

        if (obsInv.obstacles.Count > 0)
        {
            if (obsInv.obstacles[0] != null && !promptManager.pickUpTut)
            {
                promptManager.pickUpTut = true;
            }
        }
    }
}