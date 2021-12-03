using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //components
    Rigidbody rb;
    GridManager gm;
    RaycastHit hit;

    //variables
    #region variables
    [Header("Control Modes")] 
    public bool autoForward;
    [Range(1, 4)] public int playerNumber;

    [Header("Move Speeds")]
    public float currentSpeed;
    public float maxSpeed;
    public float maxVelocity;
    public float timeToMaxSpeed;
    public float boostTime;
    public float minSpeed;
    public float timeToMinSpeed;

    private bool braking, speeding, goLeft, goRight;

    [Header("Strafing")]
    public float horizontalMoveSpeedMultiplier;
    public float horizontalMoveSpeedMin;
    public float hDamp;

    [Header("Jump Checks")]
    public float castDistance;
    public bool grounded;
    public bool chargingJump;

    [Header("Jump Force")]
    public float currentJumpForce;
    public float minJumpForce;
    public float maxJumpForce;
    public float timeToMaxJumpForce;

    [Header("Jump Speeds")]
    public float jumpSpeedMult;
    public bool jumpControlled;
    public float jumpControlMult;

    [Header("Placement Mode")]
    public bool placementMode;
    public int placementX, placementZ;
    public float placementMoveDelay;

    [Header("Respawn")]
    public float deathHeight;
    public bool doesRespawn;
    public GameObject currentSpawn;
    private int currentSpawnNumber;

    [Header("Elim Count")]
    public float elimCount;

    [Header("Oil Spill Effect")]
    public float oilSpillSpeed;
    public float oilSpillDuration;

    [Header("Thumbtacks Effect")]
    public float tackSpeed;
    public float tackAirControl;
    public float tackDuration;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameObject.Find("Grid").GetComponent<GridManager>();
        placementX = 0;
        placementZ = 0;
    }

    private void Update()
    {
        GroundCheck();
        PlayerInput();
        MoveCalculations();
        PlacementMove();
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

        if (Input.GetButton("Jump" + playerNumber) && grounded)
        {
            chargingJump = true;
        }

        if(chargingJump && !grounded)
        {
            chargingJump = false;
            currentJumpForce = 0;
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

        if (!goRight && !goLeft)
        {
            StrafingDamping();
        }


        if (Input.GetButton("Jump" + playerNumber) && grounded && !placementMode)
        {
            chargingJump = true;
        }

        if (chargingJump && !grounded)
        {
            chargingJump = false;
            currentJumpForce = 0;
        }

        if (Input.GetButtonUp("Jump" + playerNumber) && grounded && !placementMode)
        {
            chargingJump = false;
            Jump();
        }

        if(Input.GetButtonDown("Jump" + playerNumber) && placementMode)
        {
            //place obstacle
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
        if (autoForward && !placementMode)
        {
            //always forward
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
        else if (!placementMode)
        {
            //forward to move
            float accRate;
            accRate = (maxSpeed / timeToMaxSpeed) * Time.deltaTime;
            if (speeding)
            {
                currentSpeed += accRate;
            }
            else
            {
                currentSpeed -= accRate;
            }
        }

    }

    void Braking()
    {
        if (braking && !placementMode)
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
        //rigidbody
        if (grounded && !placementMode)
        {
            //move normal
            rb.AddForce((transform.forward * currentSpeed));
        }
        else if(!placementMode)
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
        //rigidbody
        if (grounded && !placementMode)
        {
            if (goRight)
            {
                if(currentSpeed * horizontalMoveSpeedMultiplier > horizontalMoveSpeedMin)
                {
                    rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier);
                }
                else
                {
                    rb.AddForce(transform.right * horizontalMoveSpeedMin);
                }
            }
            else if (goLeft)
            {
                if (currentSpeed * horizontalMoveSpeedMultiplier > horizontalMoveSpeedMin)
                {
                    rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * jumpControlMult * -1);
                }
                else
                {
                    rb.AddForce(transform.right * horizontalMoveSpeedMin * jumpControlMult * -1);
                }
            }
        }
        else if (!placementMode)
        {
            if (goRight)
            {
                if (currentSpeed * horizontalMoveSpeedMultiplier > horizontalMoveSpeedMin)
                {
                    rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier);
                }
                else
                {
                    rb.AddForce(transform.right * horizontalMoveSpeedMin);
                }
            }
            else if (goLeft)
            {
                if (currentSpeed * horizontalMoveSpeedMultiplier > horizontalMoveSpeedMin)
                {
                    rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * jumpControlMult * -1);
                }
                else
                {
                    rb.AddForce(transform.right * horizontalMoveSpeedMin * jumpControlMult * -1);
                }
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
        float chargeRate = 15 * Time.deltaTime;
        if (chargingJump && ! placementMode)
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
        //rigibody
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        currentJumpForce = 0;
    }

    void Respawn()
    {
        if (currentSpawn == null && doesRespawn)
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

    void PlacementMove()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!placementMode)
            {
                placementMode = true;
                StartCoroutine(PlacementMoving());
            }
            else
            {
                placementMode = false;
            }
        }
    }

    IEnumerator PlacementMoving()
    {
        while (placementMode)
        {
            if (speeding)
            {
                //z up
                PlacementCoords(true, 1);
            }
            else if (braking)
            {
                //z down
                PlacementCoords(true, -1);
            }
            else if (goRight)
            {
                //x up
                PlacementCoords(false, 1);
            }
            else if (goLeft)
            {
                //x down
                PlacementCoords(false, -1);
            }

            yield return new WaitForSeconds(placementMoveDelay);
        }
    }

    void PlacementCoords(bool axis, int amount)
    {
        if (axis)
        {
            //move z
            placementZ += amount;
            if(placementZ < gm.smallestZ)
            {
                placementZ = gm.largestZ;
            }
            else if(placementZ > gm.largestZ)
            {
                placementZ = gm.smallestZ;
            }
        }
        else if (!axis)
        {
            //moxe x
            placementX += amount;
            if (placementX < gm.smallestX)
            {
                placementX = gm.largestX;
            }
            else if (placementX > gm.largestX)
            {
                placementX = gm.smallestX;
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);

    }
}