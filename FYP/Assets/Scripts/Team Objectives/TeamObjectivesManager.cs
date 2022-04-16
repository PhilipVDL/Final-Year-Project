using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeamObjectivesManager : MonoBehaviour
{
    public int numberOfObjectives;
    public List<string> currentObjectives;
    public string[] allObjectives;

    #region objectives
    //Can't touch this
    public bool noPlayerCollisionsThisRound;

    //Tread Carefully
    public bool noObstacleCollisionsThisRound;

    //Collision Course
    public bool allPlayersPlaced;
    public bool[] playersPlacedObstacles;
    private PlayerObstaclesRacePlace[] _playerObstacles;

    //Formation
    public bool allPlayersDifferentType;
    public List<int> playerTypes;
    private PlayerAttributes[] _playerAttributes;

    #endregion

    private void Start()
    {
        GetAll();
        RandomObjectives();
    }

    void GetAll()
    {
        GetCollisionCourse();
        GetFormation();
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int playerCount = players.Length;
        _playerAttributes = new PlayerAttributes[playerCount];
        for (int i = 0; i < _playerAttributes.Length; i++)
        {
            _playerAttributes[i] = players[i].GetComponent<PlayerAttributes>();
        }
    }

    void RandomObjectives()
    {
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
                    noPlayerCollisionsThisRound = true;
                    break;
                case "Tread Carefully":
                    noObstacleCollisionsThisRound = true;
                    break;
                case "Collision Course":
                    allPlayersPlaced = false;
                    for(int i = 0; i < playersPlacedObstacles.Length; i++)
                    {
                        playersPlacedObstacles[i] = false;
                    }
                    break;
            }
        }
    }

    private void Update()
    {
        CollisionCourseCheck();
        FormationCheck();
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
        if(trueCount >= playersPlacedObstacles.Length)
        {
            allPlayersPlaced = true;
        }
        else
        {
            allPlayersPlaced = false;
        }
    }

    void FormationCheck()
    {
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
    }
}