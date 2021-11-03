using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public bool jumpControlled;
    [Range(1, 4)] public int playerNumber;
    public float maxSpeed, maxVelocity, currentSpeed, timeToMaxSpeed, boostTime, horizontalMoveSpeedMultiplier, hDamp, minSpeed, timeToMinSpeed;
    private bool braking, speeding, goLeft, goRight;
    public float maxJumpForce, currentJumpForce, timeToMaxJumpForce, minJumpForce, jumpSpeedMult, jumpControlMult, elimCount;
    public bool grounded, chargingJump;
    public float castDistance;
    RaycastHit hit;
    public float deathHeight;
    public bool doesRespawn;
    public GameObject currentSpawn;
    private int currentSpawnNumber;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();
        PlayerInput();
        MoveCalculations();
        Respawn();
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

        if (Input.GetButtonUp("Jump" + playerNumber) && grounded)
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

        if(!goRight && !goLeft)
        {
            StrafingDamping();
            Debug.Log(playerNumber + ": " + "damping");
        }


        if (Input.GetButton("Jump" + playerNumber) && grounded)
        {
            chargingJump = true;
        }

        if (Input.GetButtonUp("Jump" + playerNumber) && grounded)
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
        //translate
        /*
        if (grounded)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime * jumpSpeedMult);
        }
        */

        //rigidbody
        if (grounded)
        {
            //move normal
            rb.AddForce((transform.forward * currentSpeed));
        }
        else
        {
            //move air
            rb.AddForce((transform.forward * currentSpeed * jumpSpeedMult));
        }
        MovementMax();
    }

    void MovementMax()
    {
        if (grounded)
        {
            if (rb.velocity.z > maxVelocity)
            {
                float brakeMag = rb.velocity.z - maxVelocity;
                rb.AddForce(transform.forward * brakeMag * -1);
            }
        }
        else
        {
            if (rb.velocity.z > (maxVelocity * jumpSpeedMult))
            {
                float brakeMag = rb.velocity.z - (maxVelocity * jumpSpeedMult);
                rb.AddForce(transform.forward * brakeMag * -1);
            }
        }
    }

    void Strafing()
    {
        //translate
        /*
        if (grounded)
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
        else
        {
            if (goRight)
            {
                transform.Translate(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * Time.deltaTime * jumpControlMult);
            }
            else if (goLeft)
            {
                transform.Translate(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * Time.deltaTime * jumpControlMult * -1);
            }
        }
        */

        //rigidbody
        if (grounded)
        {
            if (goRight)
            {
                rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier);
            }
            else if (goLeft)
            {
                rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * -1);
            }
        }
        else
        {
            if (goRight)
            {
                rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * jumpControlMult);
            }
            else if (goLeft)
            {
                rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * jumpControlMult * -1);
            }
        }
        StrafingMax();
        //StrafingDamping();
    }

    void StrafingMax()
    {
        if (grounded)
        {
            if (Mathf.Abs(rb.velocity.x) > (maxVelocity * horizontalMoveSpeedMultiplier))
            {
                float brakeMag = Mathf.Abs(rb.velocity.x) - (maxVelocity * horizontalMoveSpeedMultiplier);
                if (goRight)
                {
                    rb.AddForce(transform.right * brakeMag * -1);
                }
                else if (goLeft)
                {
                    rb.AddForce(transform.right * brakeMag);
                }
            }
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x) > (maxVelocity * horizontalMoveSpeedMultiplier * jumpSpeedMult))
            {
                float brakeMag = Mathf.Abs(rb.velocity.x) - (maxVelocity * horizontalMoveSpeedMultiplier * jumpSpeedMult);
                if (goRight)
                {
                    rb.AddForce(transform.right * brakeMag * -1);
                }
                else if (goLeft)
                {
                    rb.AddForce(transform.right * brakeMag);
                }
            }
        }
    }

    void StrafingDamping()
    {
        //damp horizontal movement
        float dampX = Mathf.Lerp(rb.velocity.x, 0, hDamp);
        Vector3 dampedVelocity = new Vector3(dampX, rb.velocity.y, rb.velocity.z);
        rb.velocity = dampedVelocity;
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

    void MinMaxJump()
    {
        if (currentJumpForce < minJumpForce)
        {
            currentJumpForce = minJumpForce;
        }
        else if (currentJumpForce > maxJumpForce)
        {
            currentJumpForce = maxJumpForce;
        }
    }

    void Jump()
    {
        //translate
        //transform.Translate(transform.up * currentJumpForce * Time.deltaTime);

        //rigibody
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        currentJumpForce = 0;
    }

    void Respawn()
    {
        if(currentSpawn == null && doesRespawn)
        {
            //set to start spawn if no spawn
            currentSpawn = GameObject.FindGameObjectWithTag("Respawns").transform.GetChild(0).gameObject;
        }

        //check if past next spawn checkpoint
        GameObject nextSpawn = null;
        if (currentSpawnNumber < GameObject.FindGameObjectWithTag("Respawns").transform.childCount - 1 && doesRespawn)
        {
            nextSpawn = GameObject.FindGameObjectWithTag("Respawns").transform.GetChild(currentSpawnNumber + 1).gameObject;

            if (transform.position.z >= nextSpawn.transform.position.z)
            {
                currentSpawn = nextSpawn;
                currentSpawnNumber++;
            }
        }


        if (transform.position.y < deathHeight && !doesRespawn)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < deathHeight && doesRespawn)
        {
            transform.position = currentSpawn.transform.position;
        }
    }

    void OnBecameInvisible()
    {
        elimCount--;
        if (elimCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}