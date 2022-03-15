using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectBallType : MonoBehaviour
{
    PlayerJoinVisible pjv;
    MeshFilter meshFilter;
    MeshRenderer rend;

    private int playerNumber;
    public int ballTypeID;
    public int minID, maxID;
    public Mesh[] meshes;
    public Material[] materials;

    private void Start()
    {
        pjv = GetComponent<PlayerJoinVisible>();
        meshFilter = GetComponent<MeshFilter>();
        rend = GetComponent<MeshRenderer>();
        playerNumber = pjv.playerNumber;
    }

    private void Update()
    {
        SwitchBallType();
        SetBallType(ballTypeID);
    }

    void SwitchBallType()
    {
        if (Input.GetButtonDown("ObstacleSwitch" + playerNumber))
        {
            if (Input.GetAxis("ObstacleSwitch" + playerNumber) > 0)
            {
                ballTypeID++;
            }
            else if (Input.GetAxis("ObstacleSwitch" + playerNumber) < 0)
            {
                ballTypeID--;
            }
        }

        //loop back if outside bounds
        if(ballTypeID > maxID)
        {
            ballTypeID = minID;
        }
        else if(ballTypeID < minID)
        {
            ballTypeID = maxID;
        }
    }

    void SetBallType(int id)
    {
        meshFilter.mesh = meshes[id];
        rend.material = materials[id];
    }
}