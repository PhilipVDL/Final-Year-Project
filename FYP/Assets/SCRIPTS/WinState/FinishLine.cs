using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public WinState win;
    public int[] points = new int[4];
    public int finished;
    public GameObject[] Players;
    public GameObject[] PlayerClones;
    public GameObject cam;
    public EndDistance end;
    public GameObject countdownSign;
    public GameObject manager;


    private void Start()
    {
        win = GameObject.Find("WinState").GetComponent<WinState>();
        finished = 0;
        PlayerClones = GameObject.FindGameObjectsWithTag("Player");
        cam = GameObject.Find("Main Camera");
        countdownSign = GameObject.Find("SIGN");
        manager = GameObject.Find("Background Tasks");
    }

    private void OnTriggerEnter(Collider other)
    {
        //score
        if (other.CompareTag("Player") && Players.Length == 1)
        {
            finished++;
            StartRound();
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;            
            win.Score(player, points[finished]);
            other.gameObject.transform.position = win.spawns[0].transform.position;
        }
        else if(other.CompareTag("Player"))
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);
            other.gameObject.transform.position = win.spawns[0].transform.position;
            other.gameObject.SetActive(false);
            GameObject.Find("UI").GetComponent<RankingUi>().positions[0] = null;
        }
    }

    private void Update()
    {
        GetPlayers();
    }

    void GetPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void StartRound()
    {
        countdownSign.GetComponent<Animator>().Play(0);
        manager.GetComponent<MainManager>().countdown = 3;

        //end round
        win.endRound = true;

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].SetActive(true);
        }

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].transform.position = win.spawns[i].transform.position;
        }

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].GetComponent<PlayerController>().currentSpawn = PlayerClones[i].GetComponent<PlayerController>().spawn;
        }
    }
}