using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EndDistance : MonoBehaviour
{
    public GameObject[] players;
    public GameObject closestPlayer, furthestPlayer, leftestPlayer, rightestPlayer;
    public float closestDist, furthestDist, playerDifference, leftDist, rightDist, horizontalDifference;
    public List<GameObject> playerPositions;
    public FinishLine finish;
    public GameObject lastPlayer;
    public GameObject firstPlayer;

    private void ListPlayers()
    {
        playerPositions.Clear();
        foreach(GameObject player in players)
        {
            playerPositions.Add(player);
        }
    }

    private void Update()
    {
        CountPlayers();
        PlayerDistance();
        HorizontalDistance();
        GetPlayerPositions();
        FirstLastPlayer();
        SetPlayerPos();
    }

    private void CountPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        ListPlayers();
    }

    private void PlayerDistance()
    {
        closestDist = 0;
        furthestDist = 0;

        foreach(GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            
            if(closestDist == 0 )
            {
                closestDist = distance;
                closestPlayer = player;
            }
            else if(closestDist > distance )
            {
                closestDist = distance;
                closestPlayer = player;
            }

            if (furthestDist == 0 && players.Length > 1)
            {
                furthestDist = distance;
                furthestPlayer = player;
            }
            else if (furthestDist < distance && players.Length > 1)
            {
                furthestDist = distance;
                furthestPlayer = player;
            }

            playerDifference = furthestDist - closestDist;
        }
    }

    void GetPlayerPositions()
    {
        playerPositions.Sort(delegate (GameObject a, GameObject b)
        {
            Vector3 pos = this.transform.position;
            Vector3 A = a.transform.position;
            Vector3 B = b.transform.position;
            return (pos - A).sqrMagnitude.CompareTo((pos-B).sqrMagnitude);
        });
    }

    void FirstLastPlayer()
    {
        if(playerPositions.Count > 0)
        {
            lastPlayer = playerPositions.Last();
            firstPlayer = playerPositions.First();
        }
    }

    void SetPlayerPos()
    {
        if (finish.finished == 0)
        {
            for(int i = 0; i < playerPositions.Count; i++)
            {
                playerPositions[i].GetComponent<PlayerController>().pos = i + 1;
            }

        }
        else if (finish.finished == 1)
        {
            for (int i = 0; i < playerPositions.Count; i++)
            {
                playerPositions[i].GetComponent<PlayerController>().pos = i + 2;
            }

        }
    }

    private void HorizontalDistance()
    {
        leftDist = 0;
        rightDist = 0;

        foreach(GameObject player in players)
        {
            float distance = player.transform.position.x;

            if(leftDist == 0)
            {
                leftDist = distance;
            }
            else if(leftDist > distance)
            {
                leftDist = distance;
            }

            if(rightDist == 0)
            {
                rightDist = distance;
            }
            else if(rightDist < distance)
            {
                rightDist = distance;
            }

            horizontalDifference = rightDist - leftDist;
        }
    }
}