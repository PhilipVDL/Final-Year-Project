using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    FinishLine finish;
    ObstaclesOnMap obstaclesOnMap;
    public GameObject playerPrefab;
    public GameObject[] players;
    public GameObject[] spawns;
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
        spawns = GameObject.FindGameObjectsWithTag("Start Spawn");
        finish = GameObject.Find("Finish").GetComponent<FinishLine>();
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
            NewRound();
        }
        else if (endRound && win)
        {
            Debug.Log("Player " + winnerNumber + " Wins!"); //win
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
        currentRound++;
        finish.finished = 0;
        obstaclesOnMap.ActivateObstacles();
        foreach(GameObject player in players)
        {
            player.GetComponent<PlayerObstaclesRacePlace>().placedThisRound = 0;
        }
        endRound = false;
    }
}