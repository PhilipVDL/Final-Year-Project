using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectBallType : MonoBehaviour
{
    PlayerJoinCount pjc;
    PlayerJoinVisible pjv;
    MeshFilter meshFilter;
    MeshRenderer rend;

    public bool characterSelect;

    private int playerNumber;
    public int ballTypeID;
    public int minID, maxID;
    public Mesh[] meshes;
    public Material[] materials;

    private void Start()
    {
        if (characterSelect)
        {
            pjc = GameObject.Find("PlayerJoinCount").GetComponent<PlayerJoinCount>();
            pjv = GetComponent<PlayerJoinVisible>();
            meshFilter = GetComponent<MeshFilter>();
            rend = GetComponent<MeshRenderer>();
            playerNumber = pjv.playerNumber;
        }
    }

    private void Update()
    {
        if (characterSelect)
        {
            SwitchBallType();
            SetBallType(ballTypeID);
        }
    }

    void SwitchBallType()
    {
        if (Input.GetButtonDown("ObstacleSwitch" + playerNumber))
        {
            if (Input.GetAxis("ObstacleSwitch" + playerNumber) > 0)
            {
                ballTypeID++;
                UnReady(playerNumber);
            }
            else if (Input.GetAxis("ObstacleSwitch" + playerNumber) < 0)
            {
                ballTypeID--;
                UnReady(playerNumber);
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

    void UnReady(int pNum)
    {
        switch (pNum)
        {
            case 1:
                pjc.playerReady1 = false;
                break;
            case 2:
                pjc.playerReady2 = false;
                break;
            case 3:
                pjc.playerReady3 = false;
                break;
            case 4:
                pjc.playerReady4 = false;
                break;
        }
    }
}