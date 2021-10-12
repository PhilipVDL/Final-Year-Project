using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDistance : MonoBehaviour
{
    public GameObject[] players;
    public GameObject closestPlayer, furthestPlayer;
    public float closestDist, furthestDist, playerDifference;

    private void Start()
    {
        CountPlayers();
    }

    private void Update()
    {
        PlayerDistance();
    }

    private void CountPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void PlayerDistance()
    {
        float[] dists = new float[players.Length];

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
}