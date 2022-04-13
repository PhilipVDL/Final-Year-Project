using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompts : MonoBehaviour
{
    public GameObject[] promptBoxes;
    public GameObject Player;

    public float duration;

    private bool moveTut = false;
    private bool jumpTut = false;
    private bool pickUpTut = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }


    void Prompts()
    {
        if(Player.GetComponent<PlayerController>().grounded != true && jumpTut == false)
        {
            promptBoxes[1].SetActive(true);
            jumpTut = true;
        }

        if (Player.GetComponent<PlayerController>().speeding == true && moveTut == false)
        {
            promptBoxes[0].SetActive(true);
            moveTut = true;
        }

        if(Player.GetComponent<ObstacleInventory>().obstacles[0] != null && pickUpTut == false)
        {
            promptBoxes[2].SetActive(true);
            pickUpTut = true;
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
        Player = this.gameObject;
        Prompts();
        
        EndPrompts();

       
    }
}
