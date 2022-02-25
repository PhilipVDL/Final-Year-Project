using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDistance : MonoBehaviour
{
    public GameObject[] players;
    public GameObject closestPlayer, furthestPlayer, leftestPlayer, rightestPlayer;
    public float closestDist, furthestDist, playerDifference, leftDist, rightDist, horizontalDifference;
    public List<GameObject> playerPositions;

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
        HorizontalDIstance();
        GetPlayerPositions();
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

    void SetPlayerPos()
    {
        playerPositions[0].GetComponent<PlayerController>().pos = 1;
        playerPositions[1].GetComponent<PlayerController>().pos = 2;
        playerPositions[2].GetComponent<PlayerController>().pos = 3;
       playerPositions[3].GetComponent<PlayerController>().pos = 4;
    }

    private void HorizontalDIstance()
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