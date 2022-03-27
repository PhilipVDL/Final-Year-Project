using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    PlayerController controller;
    public bool set;

    [Header("Ball Type")] 
    public int ballTypeID;
    public string currentBallType;
    public string[] ballTypes;
    [Header("Bouncy")]
    public float bouncyJumpMult; //add physics material
    public PhysicMaterial bouncyMaterial;
    [Header("Bowling")]
    public float bowlingSpeedMult;
    public float bowlingKnockbackMult;
    [Header("Marble")]
    public float marbleSpeedMult;
    public float marbleKnockbackMult;
    [Header("Snooker")]
    public float snookerSpeedMult;
    public float snookerDampMult;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        GetBallType();
        SetBallType();
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

    public void SetBallType()
    {
        if (set)
        {
            switch (ballTypeID)
            {
                case 0: //bouncy
                    controller.minJumpForce *= bouncyJumpMult;
                    controller.maxJumpForce *= bouncyJumpMult;
                    GetComponent<Collider>().material = bouncyMaterial;
                    break;
                case 1: //bowling
                    controller.knockbackMult = bowlingKnockbackMult;
                    controller.maxSpeed *= bowlingSpeedMult;
                    controller.maxBackSpeed *= bowlingSpeedMult;
                    break;
                case 2: //marble
                    controller.knockbackMult = marbleKnockbackMult;
                    controller.maxSpeed *= marbleSpeedMult;
                    controller.maxBackSpeed *= marbleSpeedMult;
                    break;
                case 3: //snooker
                    controller.maxSpeed *= snookerSpeedMult;
                    controller.maxBackSpeed *= snookerSpeedMult;
                    controller.fDamp *= snookerDampMult;
                    controller.hDamp *= snookerDampMult;
                    break;
            }
            set = false;
        }
    }
}