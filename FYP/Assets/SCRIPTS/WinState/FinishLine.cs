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
    public GameObject[] currentPlayers;
    public GameObject[] startPlayers;
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
        currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(GetStartPlayers());
        cam = GameObject.Find("Main Camera");
        manager = GameObject.Find("Background Tasks");
        countdownSign = GameObject.Find("SIGN");
        end = GameObject.Find("End").GetComponent<EndDistance>();
        sfx.PlaySFX(sfx._321Go);
    }

    IEnumerator GetStartPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        startPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        //score
        if (other.CompareTag("Player") && currentPlayers.Length == 1)
        {
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);

            finished++;
            StartRound();
            GameObject.Find("UI").GetComponent<RankingUi>().getPlayers();

            if (other.GetComponent<PlayerController>().knockbackObjectiveTimer > 0 && !TOM.playerHitOverLine)
            {
                TOM.playerHitOverLine = true;
                TOM.ObjectivePoints();
            }

          //  other.gameObject.GetComponent<PlayerController>().currentSpawn = other.gameObject.GetComponent<PlayerController>().spawn;
           // other.gameObject.transform.position = win.spawns[0].transform.position;
        }
        else if(other.CompareTag("Player") && currentPlayers.Length != 1)
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);
            other.gameObject.transform.position = win.spawns[0].transform.position;
            other.gameObject.SetActive(false);
            GameObject.Find("UI").GetComponent<RankingUi>().positions[0] = null;

            if (other.GetComponent<PlayerController>().knockbackObjectiveTimer > 0 && !TOM.playerHitOverLine)
            {
                TOM.playerHitOverLine = true;
                TOM.ObjectivePoints();
            }
        }
    }

    /*
    void GetPlayerClones()
    {
        countdown -= countamount * Time.deltaTime;

        if(countdown <= 0)
        {
            Players = GameObject.FindGameObjectsWithTag("Player");
            countamount = 0;
            countdown = 10;
        }
    }
    */

    private void Update()
    {
        GetPlayers();
    }

    void GetPlayers()
    {
        currentPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    public void StartRound()
    {
        countdownSign.GetComponent<Animator>().Play(0);
        manager.GetComponent<MainManager>().countdown = 3;
        if (!win.win)
        {
            sfx.PlaySFX(sfx._321Go);
        }

        //end round
        win.endRound = true;

        for (int i = 0; i < currentPlayers.Length; i++)
        {
            currentPlayers[i ].SetActive(true);
        }

        for (int i = 0; i < currentPlayers.Length; i++)
        {
            currentPlayers[i].transform.position = win.spawns[i].transform.position;
        }

        for (int i = 0; i < currentPlayers.Length; i++)
        {
            currentPlayers[i].GetComponent<PlayerController>().currentSpawn = currentPlayers[i].GetComponent<PlayerController>().spawn;
        }
    }

    public void CheckObjectives()
    {
        if (TOM.noPlayerCollisionsThisRound)
        {
            TOM.noPlayerCollisionsThisRound = false;
            TOM.ObjectivePoints();
        }

        if (TOM.noObstacleCollisionsThisRound)
        {
            TOM.noObstacleCollisionsThisRound = false;
            TOM.ObjectivePoints();
        }

        if (TOM.noPlayerFell)
        {
            TOM.noPlayerFell = false;
            TOM.ObjectivePoints();
        }

        if (TOM.noPlayerOffCamera)
        {
            TOM.noPlayerOffCamera = false;
            TOM.ObjectivePoints();
        }

        if (TOM.noPlayerHitBack)
        {
            TOM.noPlayerHitBack = false;
            TOM.ObjectivePoints();
        }
    }
}