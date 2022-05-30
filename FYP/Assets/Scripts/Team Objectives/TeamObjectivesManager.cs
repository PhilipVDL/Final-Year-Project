using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeamObjectivesManager : MonoBehaviour
{
    EndDistance end;
    WinState win;
    public int numberOfObjectives;
    public List<string> currentObjectives;
    public string[] allObjectives;
    public GameObject[] UI;

    #region objectives
    [Header("Can't touch this")]
    public bool CTT;
    public bool noPlayerCollisionsThisRound;

    [Header("Tread Carefully")]
    public bool TC;
    public bool noObstacleCollisionsThisRound;

    [Header("Collision Course")]
    public bool CC;
    public bool allPlayersPlaced;
    public bool[] playersPlacedObstacles;
    private PlayerObstaclesRacePlace[] _playerObstacles;

    [Header("Formation")]
    public bool FM;
    public bool checkpointRespawned;

    [Header("Don't Look Down")]
    public bool DLD;
    public bool noPlayerFell;

    [Header("Keep Up")]
    public bool noPlayerOffCamera; //disabled

    [Header("Knockout")]
    public bool KO;
    public bool playerKnockout;

    [Header("Avoidance")]
    public bool AV;
    public bool noPlayerHitBack;

    [Header("Aim of the Game")]
    public bool ATG;
    public bool playerHitBack;

    [Header("Over the Line")]
    public bool playerHitOverLine; //disabled

    [Header("Dark Horse")]
    public bool DH;
    public bool darkHorse;
    public GameObject lastPlayer;
    #endregion

    public void ObjectivePoints()
    {
        StartCoroutine(ObjectiveComplete());
        for(int i = 0; i < win.scores.Length; i++)
        {
            win.scores[i] += 2; //add 2 points to everyone
        }
    }

    IEnumerator ObjectiveComplete()
    {
        foreach (GameObject ui in UI)
        {
            ui.SetActive(false);
        }
        UI[9].SetActive(true);
        yield return new WaitForSeconds(3f);
        UI[9].SetActive(false);
    }

    private void Start()
    {
        end = GameObject.Find("End").GetComponent<EndDistance>();
        win = GameObject.Find("WinState").GetComponent<WinState>();
        UI = new GameObject[GameObject.FindGameObjectsWithTag("ObjectiveUI").Length];
        foreach(GameObject ui in GameObject.FindGameObjectsWithTag("ObjectiveUI")) //gets them in the wrong order in build, so this is solution
        {
            switch (ui.name)
            {
                case "CTT":
                    UI[0] = ui;
                    break;
                case "TC":
                    UI[1] = ui;
                    break; 
                case "CC":
                    UI[2] = ui;
                    break; 
                case "Fm":
                    UI[3] = ui;
                    break; 
                case "DLD":
                    UI[4] = ui;
                    break; 
                case "KO":
                    UI[5] = ui;
                    break; 
                case "Av":
                    UI[6] = ui;
                    break; 
                case "ATG":
                    UI[7] = ui;
                    break; 
                case "DH":
                    UI[8] = ui;
                    break; 
                case "OC":
                    UI[9] = ui;
                    break; 
            }
        }

        foreach(GameObject ui in UI)
        {
            ui.SetActive(false);
        }
        GetAll();
        RandomObjectives();
    }

    void GetAll()
    {
        GetCollisionCourse();
        //GetFormation();
    }

    void GetCollisionCourse()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int playerCount = players.Length;
        playersPlacedObstacles = new bool[playerCount];
        _playerObstacles = new PlayerObstaclesRacePlace[playerCount];
        for (int i = 0; i < _playerObstacles.Length; i++)
        {
            _playerObstacles[i] = players[i].GetComponent<PlayerObstaclesRacePlace>();
        }
    }

    void GetFormation()
    {
        /*
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int playerCount = players.Length;
        _playerAttributes = new PlayerAttributes[playerCount];
        for (int i = 0; i < _playerAttributes.Length; i++)
        {
            _playerAttributes[i] = players[i].GetComponent<PlayerAttributes>();
        }
        */
    }

    public void RandomObjectives()
    {
        DeactivateObjectives();

        currentObjectives.Clear(); //clear for new round
        List<string> chooseObjectives = new List<string>(allObjectives);

        for(int i = 0; i < numberOfObjectives; i++)
        {
            int random = Random.Range(0, chooseObjectives.Count);
            currentObjectives.Add(chooseObjectives[random]); //add random objective
            chooseObjectives.RemoveAt(random); //remove chosen from list
        }

        ActivateObjectives();
    }

    void ActivateObjectives()
    {
        foreach(string objective in currentObjectives)
        {
            switch (objective)
            {
                case "Can't Touch This":
                    CTT = true;
                    noPlayerCollisionsThisRound = true;
                    UI[0].SetActive(true);
                    break;
                case "Tread Carefully":
                    TC = true;
                    noObstacleCollisionsThisRound = true;
                    UI[1].SetActive(true);
                    break;
                case "Collision Course":
                    CC = true;
                    allPlayersPlaced = false;
                    UI[2].SetActive(true);
                    for (int i = 0; i < playersPlacedObstacles.Length; i++)
                    {
                        playersPlacedObstacles[i] = false;
                    }
                    break;
                case "Formation":
                    FM = true;
                    checkpointRespawned = false;
                    UI[3].SetActive(true);
                    break;
                case "Don't Look Down":
                    DLD = true;
                    noPlayerFell = true;
                    UI[4].SetActive(true);
                    break;
                case "Keep Up":
                    noPlayerOffCamera = true; //disabled
                    break;
                case "Knockout":
                    KO = true;
                    playerKnockout = false;
                    UI[5].SetActive(true);
                    break;
                case "Avoidance":
                    AV = true;
                    noPlayerHitBack = true;
                    UI[6].SetActive(true);
                    break;
                case "Aim of the Game":
                    ATG = true;
                    playerHitBack = false;
                    UI[7].SetActive(true);
                    break;
                case "Over the Line":
                    playerHitOverLine = false; //disabled
                    break;
                case "Dark Horse":
                    DH = true;
                    StartCoroutine(DarkHorseActivated());
                    break;
            }
        }
    }

    void DeactivateObjectives()
    {
        //bools
        CTT = false;
        TC = false;
        CC = false;
        FM = false;
        DLD = false;
        KO = false;
        AV = false;
        ATG = false;
        DH = false;
        //checks
        noPlayerCollisionsThisRound = false;
        noObstacleCollisionsThisRound = false;
        allPlayersPlaced = true;
        checkpointRespawned = true;
        noPlayerFell = false;
        noPlayerOffCamera = false;
        playerKnockout = true;
        noPlayerHitBack = false;
        playerHitOverLine = true;
        darkHorse = false;
        lastPlayer = null;
        foreach(GameObject ui in UI)
        {
            ui.SetActive(false);
        }
    }

    IEnumerator DarkHorseActivated()
    {
        darkHorse = false;
        UI[8].SetActive(true);
        yield return new WaitForSeconds(6f);
        lastPlayer = end.lastPlayer;
    }

    private void Update()
    {
        CollisionCourseCheck();
        //FormationCheck();
        DarkHorseCheck();
    }

    void CollisionCourseCheck()
    {
        //check who has placed
        for (int i = 0; i < _playerObstacles.Length; i++)
        {
            if(_playerObstacles[i].placedThisRound > 0)
            {
                playersPlacedObstacles[i] = true;
            }
            else
            {
                playersPlacedObstacles[i] = false;
            }
        }

        //check has everyone placed
        int trueCount = 0;
        foreach (bool placed in playersPlacedObstacles)
        {
            if (placed)
            {
                trueCount++;
            }
        }

        //if all placed, objective true
        if(trueCount >= playersPlacedObstacles.Length && !allPlayersPlaced && CC)
        {
            allPlayersPlaced = true;
            ObjectivePoints();
        }
        else
        {
            allPlayersPlaced = false;
        }
    }

    void FormationCheck()
    {
        /*
        playerTypes.Clear(); //clear for new check
        foreach(PlayerAttributes att in _playerAttributes)
        {
            playerTypes.Add(att.ballTypeID); //add all types
        }
        playerTypes = playerTypes.Distinct().ToList(); //only list unique entries, using system.Linq

        if(playerTypes.Count == _playerAttributes.Length)
        {
            allPlayersDifferentType = true;
        }
        else
        {
            allPlayersDifferentType = false;
        }
        */
    }

    void DarkHorseCheck()
    {
        if(lastPlayer != null)
        {
            if (end.firstPlayer == lastPlayer && !darkHorse && DH)
            {
                darkHorse = true;
                ObjectivePoints();
            }
        }
    }
}