using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jumpControlled;
    [Range(1, 4)] public int playerNumber;
    public float maxSpeed, currentSpeed, timeToMaxSpeed, boostTime, horizontalMoveSpeedMultiplier, minSpeed, timeToMinSpeed;
    public bool braking, speeding, goLeft, goRight;
    public float maxJumpForce, currentJumpForce, timeToMaxJumpForce, minJumpForce, gravity, jumpSpeedMult;
    public bool grounded, chargingJump;
    public float castDistance;
    RaycastHit hit;

    private void Update()
    {
        GroundCheck();
        PlayerInput();
        MoveCalculations();
    }

    void PlayerInput()
    {
        if (jumpControlled)
        {
            ControlsControlledJump(playerNumber);
        }
        else
        {
            ControlsUncontrolledJump(playerNumber);
        }

        /*
        switch (playerNumber)
        {
            case 1:
                Player1();
                break;
            case 2:
                Player2();
                break;
            case 3:
                Player3();
                break;
            case 4:
                Player4();
                break;
        }
        */
    }

    #region controls
    void ControlsUncontrolledJump(int playerNumber)
    {
        if (grounded)
        {
            float inputVertical = Input.GetAxis("Vertical" + playerNumber);
            if (inputVertical > 0)
            {
                speeding = true;
            }
            else
            {
                speeding = false;
            }
            if (inputVertical < 0)
            {
                braking = true;
            }
            else
            {
                braking = false;
            }

            float inputHorizontal = Input.GetAxis("Horizontal" + playerNumber);
            if (inputHorizontal > 0)
            {
                goRight = true;
            }
            else
            {
                goRight = false;
            }
            if (inputHorizontal < 0)
            {
                goLeft = true;
            }
            else
            {
                goLeft = false;
            }
        }

        if (Input.GetButton("Jump" +playerNumber) && grounded)
        {
            chargingJump = true;
        }
        else
        {
            chargingJump = false;
            Jump();
        }
    }

    void ControlsControlledJump(int playerNumber)
    {
        float inputVertical = Input.GetAxis("Vertical" + playerNumber);
        if (inputVertical > 0)
        {
            speeding = true;
        }
        else
        {
            speeding = false;
        }
        if (inputVertical < 0)
        {
            braking = true;
        }
        else
        {
            braking = false;
        }

        float inputHorizontal = Input.GetAxis("Horizontal" + playerNumber);
        if (inputHorizontal > 0)
        {
            goRight = true;
        }
        else
        {
            goRight = false;
        }
        if (inputHorizontal < 0)
        {
            goLeft = true;
        }
        else
        {
            goLeft = false;
        }


        if (Input.GetButton("Jump" + playerNumber) && grounded)
        {
            chargingJump = true;
        }
        else
        {
            chargingJump = false;
            Jump();
        }
    }
    #endregion

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, castDistance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void MoveCalculations()
    {
        Acceleration();
        Braking();
        MinMaxSpeed();
        Movement();
        Strafing();
        ChargeJump();
        Gravity();
    }

    void Acceleration()
    {
        if (currentSpeed < maxSpeed && !braking)
        {
            float accRate;
            if (!speeding)
            {
                //3 secs till max
                accRate = (maxSpeed / timeToMaxSpeed) * Time.deltaTime;
            }
            else
            {
                //unless player inputVertical boosts rate
                accRate = (maxSpeed / (timeToMaxSpeed - boostTime)) * Time.deltaTime;
            }
            currentSpeed += accRate;
        }
    }

    void Braking()
    {
        if (braking)
        {
            currentSpeed -= (maxSpeed / timeToMinSpeed) * Time.deltaTime;
        }
    }

    void MinMaxSpeed()
    {
        if (currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    void Movement()
    {
        if (grounded)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime * jumpSpeedMult);
        }
    }

    void Strafing()
    {
        if (goRight)
        {
            transform.Translate(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * Time.deltaTime);
        }
        else if (goLeft)
        {
            transform.Translate(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * Time.deltaTime * -1);
        }
    }

    void ChargeJump()
    {
        float chargeRate = maxJumpForce / timeToMaxJumpForce * Time.deltaTime;
        if (chargingJump)
        {
            currentJumpForce += chargeRate;
            MinMaxJump();
        }
    }

    void Jump()
    {
        transform.Translate(transform.up * currentJumpForce * Time.deltaTime);
    }

    void Gravity()
    {
        if (!grounded && currentJumpForce > 0)
        {
            currentJumpForce -= gravity * Time.deltaTime;
            transform.Translate(transform.up * currentJumpForce * Time.deltaTime);
        }
        else if (!grounded && currentJumpForce <= 0)
        {
            currentJumpForce -= gravity * Time.deltaTime;
            transform.Translate(transform.up * currentJumpForce * Time.deltaTime);
        }
        else if(grounded && currentJumpForce < 0)
        {
            currentJumpForce = 0;
        }
    }

    void MinMaxJump()
    {
        if(currentJumpForce < minJumpForce)
        {
            currentJumpForce = minJumpForce;
        }
        else if(currentJumpForce > maxJumpForce)
        {
            currentJumpForce = maxJumpForce;
        }
    }
}