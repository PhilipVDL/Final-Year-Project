using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    FinishLine finish;
    PlayerCustoms customs;
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
        maincamera = GameObject.Find("Main Camera");
        camController = maincamera.GetComponent<CameraController>();
    }

    private void Update()
    {
        GetPlayers();
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
            maincamera.GetComponent<CameraController>().placementPhase = true;
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
        finish.NewRound();
        //SpawnPlayers();
        currentRound++;
        StartCoroutine(CamNewRound());
        //GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers = 4;
        /*
        if (GameObject.Find("Main Camera").GetComponent<CameraController>().camCountdown < -48)
        {
            maincamera.GetComponent<CameraController>().placementPhase = false;
        }
        */
        endRound = false;
    }

    IEnumerator CamNewRound()
    {
        //yield return new WaitUntil(() => CamCountDownTracker() < -48);
        //maincamera.GetComponent<CameraController>().placementPhase = false;

        yield return new WaitUntil(() => !camController.placementPhase);
        SpawnPlayers();
        //endRound = false;
    }

    void SpawnPlayers()
    {
        /*
        for (int i = 1; i <= 4; i++)
        {
            GameObject thisPlayer = Instantiate(playerPrefab, spawns[i - 1].position, Quaternion.identity);
            thisPlayer.GetComponent<PlayerController>().playerNumber = i;
            GameObject skin = thisPlayer.transform.GetChild(0).gameObject;
            skin.GetComponent<Renderer>().material = customs.SetMaterial(i);
            skin.GetComponent<MeshFilter>().mesh = customs.SetMesh(i);
            skin.transform.localScale = customs.SetScale(i);
        }
        */

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

        foreach(GameObject player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller.placementMode)
            {
                //controller.PlacementMove();
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