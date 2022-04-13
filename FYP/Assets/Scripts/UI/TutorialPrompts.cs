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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MovePrompt()
    {
        if(Player.GetComponent<PlayerController>().speeding == true && moveTut == false)
        {
            
            promptBoxes[0].SetActive(true);
            moveTut = true;
        }
    }

    void JumpPrompt()
    {
        if(Player.GetComponent<PlayerController>().grounded != true && jumpTut == false)
        {
            promptBoxes[1].SetActive(true);
            jumpTut = true;
        }
    }

    void EndPrompts()
    {
        if(moveTut == true && jumpTut == true && duration > 0)
        {
            duration -= Time.deltaTime;
        }

        
    }
    
     void Update()
    {
        Player = this.gameObject;
        MovePrompt();
        JumpPrompt();
        EndPrompts();

        if (duration <= 0)
        {
            promptBoxes[0].SetActive(false);
            promptBoxes[1].SetActive(false);
            duration = 0;
        }
    }
}
