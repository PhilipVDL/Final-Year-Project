using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    FinishLine finish;
    PlayerCustoms customs;
    ObstaclesOnMap obstaclesOnMap;
    public GameObject playerPrefab;
    public GameObject[] players;
    public Transform[] spawns;
    public GameObject maincamera;
    CameraController camController;

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
        obstaclesOnMap = GameObject.Find("Obstacles On Map").GetComponent<ObstaclesOnMap>();
        maincamera = GameObject.Find("Main Camera");
        camController = maincamera.GetComponent<CameraController>();
        GetPlayers();
    }

    private void Update()
    {
        
        EndRound();
    }

    float CamCountDownTracker()
    {
        float countdown = camController.camCountdown;
        return countdown;
    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void EndRound()
    {
        if (endRound && !win)
        {
            //maincamera.GetComponent<CameraController>().placementPhase = true;

            NewRound();

           
        }


        else if (endRound && win)
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
        foreach (int points in scores)
        {
            if (points >= targetScore)
            {
                win = true;
                winnerNumber = player;
            }
        }
    }

    public void NewRound()
    {
       // finish.NewRound();
       // SpawnPlayers();
        currentRound++;
        obstaclesOnMap.ActivateObstacles();
        foreach(GameObject player in players)
        {
            player.GetComponent<PlayerObstaclesRacePlace>().placedThisRound = 0;
        }
       // StartCoroutine(CamNewRound());
       
        endRound = false;

       
    }

 

    void SpawnPlayers()
    {
        foreach (GameObject player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller.placementMode)
            {

                controller.placementMode = false;
               
            }
        }

           
        

        /*
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[0].SetActive(true);
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[1].SetActive(true);
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[2].SetActive(true);
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[3].SetActive(true);

           GameObject.Find("Finish").GetComponent<FinishLine>().Players[0].GetComponent<PlayerController>().placementMode = false;
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[1].GetComponent<PlayerController>().placementMode = false;
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[2].GetComponent<PlayerController>().placementMode = false;
           GameObject.Find("Finish").GetComponent<FinishLine>().Players[3].GetComponent<PlayerController>().placementMode = false;
        */
    }
}