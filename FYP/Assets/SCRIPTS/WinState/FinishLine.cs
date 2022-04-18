using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    TeamObjectivesManager TOM;
    SFXScript sfx;
    public WinState win;
    public int[] points = new int[4];
    public int finished;
    public GameObject[] Players;
    public GameObject[] PlayerClones;
    public GameObject cam;
    public EndDistance end;
    public GameObject countdownSign;
    public GameObject manager;

    public float countdown = 3;
    private float countamount = 1;


    private void Start()
    {
        TOM = GameObject.Find("Team Objectives Manager").GetComponent<TeamObjectivesManager>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
        win = GameObject.Find("WinState").GetComponent<WinState>();
        finished = 0;
        PlayerClones = GameObject.FindGameObjectsWithTag("Player");
        cam = GameObject.Find("Main Camera");
        manager = GameObject.Find("Background Tasks");
        countdownSign = GameObject.Find("SIGN");
        end = GameObject.Find("End").GetComponent<EndDistance>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //score
        if (other.CompareTag("Player") && Players.Length == 1)
        {
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);

            finished++;
            StartRound();
            GameObject.Find("UI").GetComponent<RankingUi>().getPlayers();

            if (other.GetComponent<PlayerController>().knockbackObjectiveTimer > 0)
            {
                TOM.playerHitOverLine = true;
            }

          //  other.gameObject.GetComponent<PlayerController>().currentSpawn = other.gameObject.GetComponent<PlayerController>().spawn;
           // other.gameObject.transform.position = win.spawns[0].transform.position;
        }
        else if(other.CompareTag("Player") && Players.Length != 1)
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);
            other.gameObject.transform.position = win.spawns[0].transform.position;
            other.gameObject.SetActive(false);
            GameObject.Find("UI").GetComponent<RankingUi>().positions[0] = null;

            if (other.GetComponent<PlayerController>().knockbackObjectiveTimer > 0)
            {
                TOM.playerHitOverLine = true;
            }
        }
    }

    void GetPlayerClones()
    {
        countdown -= countamount * Time.deltaTime;

        if(countdown <= 0)
        {
            PlayerClones = GameObject.FindGameObjectsWithTag("Player");
            countamount = 0;
            countdown = 10;
        }
    }

    private void Update()
    {
        GetPlayers();
        GetPlayerClones();
    }

    void GetPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void StartRound()
    {
        countdownSign.GetComponent<Animator>().Play(0);
        manager.GetComponent<MainManager>().countdown = 3;
        sfx.PlaySFX(sfx._321Go);

        //end round
        win.endRound = true;

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i ].SetActive(true);
        }

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i + 1].transform.position = win.spawns[i+ 1].transform.position;
        }

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].GetComponent<PlayerController>().currentSpawn = PlayerClones[i].GetComponent<PlayerController>().spawn;
        }
    }
}