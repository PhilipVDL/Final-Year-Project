using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [Range(1, 4)] public int num;

    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {

    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
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
