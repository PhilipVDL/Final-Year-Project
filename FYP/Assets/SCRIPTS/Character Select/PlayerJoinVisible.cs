using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinVisible : MonoBehaviour
{
    public PlayerJoinCount joinCount; //Assign manually
    public bool amJoined;
    MeshRenderer rend;

    [Range(1,4)] public int playerNumber;

    private void Start()
    {
        joinCount = GameObject.Find("PlayerJoinCount").GetComponent<PlayerJoinCount>();
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
    }

    private void Update()
    {
        CheckJoin();
    }

    void CheckJoin()
    {
        switch (playerNumber)
        {
            case 1:
                if (joinCount.playerJoined1)
                {
                    rend.enabled = true;
                    amJoined = true;
                }
                break;
            case 2:
                if (joinCount.playerJoined2)
                {
                    rend.enabled = true;
                    amJoined = true;
                }
                break;
            case 3:
                if (joinCount.playerJoined3)
                {
                    rend.enabled = true;
                    amJoined = true;
                }
                break;
            case 4:
                if (joinCount.playerJoined4)
                {
                    rend.enabled = true;
                    amJoined = true;
                }
                break;
        }
    }
}