using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Ball Type")] 
    public int ballTypeID;
    public string currentBallType;
    public string[] ballTypes;
    [Header("Bouncy")]
    public float bouncyAttribute;
    [Header("Bowling")]
    public float bowlingAttribute;
    [Header("Marble")]
    public float marbleAttribute;
    [Header("Snooker")]
    public float snookerAttribute;

    private void Update()
    {
        GetBallType();
    }

    void GetBallType()
    {
        if(ballTypeID >= 0 && ballTypeID < ballTypes.Length)
        {
            currentBallType = ballTypes[ballTypeID];
        }
        else
        {
            currentBallType = "ERROR";
        }
    }
}