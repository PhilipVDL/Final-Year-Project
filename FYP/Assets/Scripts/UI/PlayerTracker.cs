using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [Range(1, 4)] public int num;

    public GameObject[] players;

    void Start()
    {
       
    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        GetPlayers();
        TrackPlayer();
    }

    void TrackPlayer()
    {
        switch (num)
        {
            case 1:
                transform.position = players[0].transform.position;
                break;
        }
    }
}
