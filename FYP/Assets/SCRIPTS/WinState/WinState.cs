using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    FinishLine finish;
    PlayerCustoms customs;
    public GameObject[] players;
    public Transform[] spawns;
    public GameObject maincamera;

    //score
    public int[] scores = new int[4];
    public int targetScore;
    public bool win;
    public int winnerNumber;

    //rounds
    public int currentRound;
    public bool endRound;

    private void Start()
    {
        finish = GameObject.Find("Finish").GetComponent<FinishLine>();
        customs = GameObject.Find("Player Customs").GetComponent<PlayerCustoms>();
        maincamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        if (endRound && !win)
        {
            maincamera.GetComponent<CameraController>().placementPhase = true;
            
           NewRound();

            
        }
        else if(endRound && win)
        {
            //win
            Debug.Log("Player " + winnerNumber + " Wins!");
        }
    }

    public void Score(int player, int score)
    {
        //add points
        scores[player - 1] += score;

        //check for winner
        foreach(int points in scores)
        {
            if(points >= targetScore)
            {
                win = true;
                winnerNumber = player;
            }
        }
    }

    public void NewRound()
    {
       
        finish.NewRound();
        SpawnPlayers();
        currentRound++;
       
        //GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers = 4;
       
    }

   public void SpawnPlayers()
    {
  
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[0].SetActive(true);
       
     

        GameObject.Find("Finish").GetComponent<FinishLine>().Players[0].GetComponent<PlayerController>().placementMode = true;
        


    }
}