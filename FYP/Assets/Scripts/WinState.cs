using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    FinishLine finish;
    PlayerCustoms customs;
    public GameObject player;
    public Transform[] spawns;

    //score
    public int p1Score, p2Score, p3Score, p4Score;
    public int targetScore;

    //rounds
    public int currentRound;
    public bool endRound;

    private void Start()
    {
        finish = GameObject.Find("Finish").GetComponent<FinishLine>();
        customs = GameObject.Find("Player Customs").GetComponent<PlayerCustoms>();
    }

    private void Update()
    {
        if (endRound)
        {
            NewRound();
        }
    }

    public void Score(int player, int score)
    {
        switch (player)
        {
            case 1:
                p1Score += score;
                break;
            case 2:
                p2Score += score;
                break;
            case 3:
                p3Score += score;
                break;
            case 4:
                p4Score += score;
                break;
        }
    }

    public void NewRound()
    {
        finish.NewRound();
        SpawnPlayers();
        currentRound++;
        endRound = false;
    }

    void SpawnPlayers()
    {
        for(int i = 1; i <= 4; i++)
        {
            GameObject thisPlayer = Instantiate(player, spawns[i - 1].position, Quaternion.identity);
            thisPlayer.GetComponent<PlayerController>().playerNumber = i;
            thisPlayer.GetComponent<Renderer>().material = customs.SetMaterial(i);
        }
    }
}