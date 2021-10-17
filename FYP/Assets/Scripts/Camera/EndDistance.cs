using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDistance : MonoBehaviour
{
    public GameObject[] players;
    public GameObject closestPlayer, furthestPlayer, leftestPlayer, rightestPlayer;
    public float closestDist, furthestDist, playerDifference, leftDist, rightDist, horizontalDifference;

    private void Start()
    {
        CountPlayers();
    }

    private void Update()
    {
        PlayerDistance();
        HorizontalDIstance();
    }

    private void CountPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void PlayerDistance()
    {
        closestDist = 0;
        furthestDist = 0;

        foreach(GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            
            if(closestDist == 0)
            {
                closestDist = distance;
                closestPlayer = player;
            }
            else if(closestDist > distance)
            {
                closestDist = distance;
                closestPlayer = player;
            }

            if (furthestDist == 0)
            {
                furthestDist = distance;
                furthestPlayer = player;
            }
            else if (furthestDist < distance)
            {
                furthestDist = distance;
                furthestPlayer = player;
            }

            playerDifference = furthestDist - closestDist;
        }
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