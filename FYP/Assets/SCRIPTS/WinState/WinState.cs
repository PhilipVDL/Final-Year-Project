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
        if (endRound && !win )
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
        
        currentRound++;
       
        //GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers = 4;
        if (GameObject.Find("Main Camera").GetComponent<CameraController>().camCountdown <= -9)
        {
            SpawnPlayers();
            endRound = true;
            maincamera.GetComponent<CameraController>().placementPhase = false;
        }
    }

    void SpawnPlayers()
    {
     /*
        Instantiate(players[0], spawns[0].position, Quaternion.identity);
        Instantiate(players[1], spawns[1].position, Quaternion.identity);
        Instantiate(players[2], spawns[2].position, Quaternion.identity);
        Instantiate(players[3], spawns[3].position, Quaternion.identity);

        GameObject.Find("Finish").GetComponent<FinishLine>().Players[0] = players[0];
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[1] = players[1];
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[2] = players[2];
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[3] = players[3];
   */
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[0].SetActive(true);
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[1].SetActive(true);
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[2].SetActive(true);
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[3].SetActive(true);

        GameObject.Find("Finish").GetComponent<FinishLine>().Players[0].GetComponent<PlayerController>().placementMode = false;
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[1].GetComponent<PlayerController>().placementMode = false;
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[2].GetComponent<PlayerController>().placementMode = false;
        GameObject.Find("Finish").GetComponent<FinishLine>().Players[3].GetComponent<PlayerController>().placementMode = false;


    }
}