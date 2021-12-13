using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    FinishLine finish;
    PlayerCustoms customs;
    public GameObject player;
    public Transform[] spawns;

    //players
    public int numberOfPlayers;

    //score
    public int[] scores;
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

        scores = new int[numberOfPlayers];
    }

    private void Update()
    {
        if (endRound && !win)
        {
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
        SpawnPlayersNewRound();
        currentRound++;
        endRound = false;
    }

    void SpawnPlayersNewRound()
    {
        for(int i = 1; i <= numberOfPlayers; i++)
        {
            GameObject thisPlayer = Instantiate(player, spawns[i - 1].position, Quaternion.identity);
            thisPlayer.GetComponent<PlayerController>().playerNumber = i;
            GameObject skin = thisPlayer.transform.GetChild(0).gameObject;
            skin.GetComponent<Renderer>().material = customs.SetMaterial(i);
            skin.GetComponent<MeshFilter>().mesh = customs.SetMesh(i);
            skin.transform.localScale = customs.SetScale(i);
        }
    }

    public void SpawnPlayersMidRound(GameObject spawn)
    {
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            GameObject thisPlayer = Instantiate(player, spawn.transform.position, Quaternion.identity);
            thisPlayer.GetComponent<PlayerController>().playerNumber = i;
            GameObject skin = thisPlayer.transform.GetChild(0).gameObject;
            skin.GetComponent<Renderer>().material = customs.SetMaterial(i);
            skin.GetComponent<MeshFilter>().mesh = customs.SetMesh(i);
            skin.transform.localScale = customs.SetScale(i);
            thisPlayer.transform.Translate(0, i, 0);
        }
    }
}