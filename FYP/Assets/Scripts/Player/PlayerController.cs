using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 4)] public int playerNumber;
    public float maxSpeed, currentSpeed, timeToMax, boostTime, horizontalMoveSpeedMultiplier, minSpeed, timeToMin, jumpForce;
    public bool braking, speeding, goLeft, goRight;

    private void Update()
    {
        PlayerInput();
        MoveCalculations();
    }

    void PlayerInput()
    {
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
    }

    #region controls
    void Player1()
    {
        float inputVertical = Input.GetAxis("Vertical1");
        if(inputVertical > 0)
        {
            speeding = true;
        }
        else
        {
            speeding = false;
        }
        if(inputVertical < 0)
        {
            braking = true;
        }
        else
        {
            braking = false;
        }

        float inputHorizontal = Input.GetAxis("Horizontal1");
        if(inputHorizontal > 0)
        {
            goRight = true;
        }
        else
        {
            goRight = false;
        }
        if(inputHorizontal < 0)
        {
            goLeft = true;
        }
        else
        {
            goLeft = false;
        }
    }

    void Player2()
    {
        float inputVertical = Input.GetAxis("Vertical2");
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

        float inputHorizontal = Input.GetAxis("Horizontal2");
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

    void Player3()
    {
        float inputVertical = Input.GetAxis("Vertical3");
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

        float inputHorizontal = Input.GetAxis("Horizontal3");
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

    void Player4()
    {
        float inputVertical = Input.GetAxis("Vertical4");
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

        float inputHorizontal = Input.GetAxis("Horizontal4");
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
    #endregion

    void MoveCalculations()
    {
        //accelerate
        if(currentSpeed < maxSpeed && !braking)
        {
            float accRate;
            if (!speeding)
            {
                //3 secs till max
                accRate = (maxSpeed / timeToMax) * Time.deltaTime;
            }
            else
            {
                //unless player inputVertical boosts rate
                accRate = (maxSpeed / (timeToMax - boostTime)) * Time.deltaTime;
            }
            //faster
            currentSpeed += accRate;
        }

        //brake
        if (braking)
        {
            currentSpeed -= (maxSpeed / timeToMin) * Time.deltaTime;
        }

        //min/max speed
        if(currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        //move
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime);

        //strafe
        if (goRight)
        {
            transform.Translate(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * Time.deltaTime);
        }
        else if (goLeft)
        {
            transform.Translate(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * Time.deltaTime * -1);
        }
    }
}