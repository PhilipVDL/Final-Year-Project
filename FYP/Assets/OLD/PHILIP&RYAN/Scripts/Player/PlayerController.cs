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
    ObstacleInventory inventory;
    GameObject obstaclesOnMap;

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
    public float fDamp;

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
    public float airControlMult;

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

    [Header("Defaults")]
    private float defaultMaxSpeed;
    private float defaultAirControl;
    private float defaultAirSpeed;

    [Header("Oil Spill Effect")]
    public bool oiled;
    public float oilSpillSpeed;
    public float oilSpillDuration;
    public float oilSpillTimer;

    [Header("Thumbtacks Effect")]
    public bool deflated;
    public float tackSpeed;
    public float tackAirControl;
    public float tackDuration;
    public float tackTimer;

    [Header("Trampoline Effect")]
    public bool trampolined;
    public float trampolineJumpSpeedMult;
    public float trampolineDuration;
    public float trampolineTimer;

    [Header("Laser Effect")]
    public bool lasered;

    [Header("Laser Effect")]
    public bool speedPadded;
    public float padSpeed;
    public float padDuration;
    public float padTimer;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameObject.FindGameObjectWithTag("Grid Manager").GetComponent<GridManager>();
        inventory = GetComponent<ObstacleInventory>();
        obstaclesOnMap = GameObject.Find("ObstaclesOnMap");
        placementX = 0;
        placementZ = 0;
        Defaults();
    }

    void Defaults()
    {
        defaultMaxSpeed = maxSpeed;
        defaultAirControl = airControlMult;
        defaultAirSpeed = jumpSpeedMult;
    }

    private void Update()
    {
        GroundCheck();
        PlayerInput();
        ObstacleTimers();
        Begin();
        PlacementHighlight();
        PlacementDebugToggle();
        Respawn();
    }

    public void Begin()
    {
      if(GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown <= 0)
        {
            MoveCalculations();
        }
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

        if(currentSpeed == 0)
        {
            MovementDamping();
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
            if(inventory.obstacles.Count > 0 && inventory.obstacles[0] != null)
            {
                Transform grid = gm.FindGridZone(placementX, placementZ, playerNumber, inventory.obstacles[inventory.selectedIndex]);
                //find, check
                if (grid != null)
                {
                    GameObject obstacle = Instantiate(inventory.obstacles[inventory.selectedIndex], grid); //place
                    obstacle.transform.parent = obstaclesOnMap.transform; //unparent
                    inventory.obstacles.RemoveAt(inventory.selectedIndex); //remove from inventory
                }
            }
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

    void MovementDamping()
    {
        //damp at 0 speed
        float dampZ = Mathf.Lerp(rb.velocity.z, 0, fDamp);
        Vector3 dampedVelocity = new Vector3(rb.velocity.x, rb.velocity.y, dampZ);
        rb.velocity = dampedVelocity;
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
                    rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * -1);
                }
                else
                {
                    rb.AddForce(transform.right * horizontalMoveSpeedMin * -1);
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
                    rb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * airControlMult * -1);
                }
                else
                {
                    rb.AddForce(transform.right * horizontalMoveSpeedMin * airControlMult * -1);
                }
            }
        }
        StrafingMax();
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

    void PlacementDebugToggle()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlacementMove();
        }
    }

    public void PlacementMove()
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

    void PlacementHighlight()
    {
        if (placementMode)
        {
            gm.HighlightGridZone(placementX, placementZ, playerNumber);
        }
    }

    #region obstacles
    void RestoreDefaults()
    {
        maxSpeed = defaultMaxSpeed;
        airControlMult = defaultAirControl;
        jumpSpeedMult = defaultAirSpeed;
    }

    public void Oiled()
    {
        oiled = true;
        oilSpillTimer = oilSpillDuration;
    }

    public void Deflated()
    {
        deflated = true;
        tackTimer = tackDuration;
    }

    public void Trampolined()
    {
        trampolined = true;
        trampolineTimer = trampolineDuration;
    }

    public void SpeedPadded()
    {
        speedPadded = true;
        padTimer = padDuration;
    }

    public void Lasered()
    {
        lasered = true;
    }

    void ObstacleTimers()
    {
        //oil
        if(oiled)
        {
            maxSpeed = oilSpillSpeed;
            oilSpillTimer -= Time.deltaTime;
            if(oilSpillTimer <= 0)
            {
                oilSpillTimer = 0;
                oiled = false;
                RestoreDefaults();
            }
        }

        //tack
        if (deflated)
        {
            maxSpeed = tackSpeed;
            airControlMult = tackAirControl;
            tackTimer -= Time.deltaTime;
            if(tackTimer <= 0)
            {
                tackTimer = 0;
                deflated = false;
                RestoreDefaults();
            }
        }

        //trampoline
        if (trampolined)
        {
            jumpSpeedMult = trampolineJumpSpeedMult;
            trampolineTimer -= Time.deltaTime;
            if(trampolineTimer <= 0)
            {
                trampolineTimer = 0;
                trampolined = false;
                RestoreDefaults();
            }
        }

        //Lasers
        if (lasered)
        {
            Destroy(this.gameObject);
        }

        //Speed Pad
        if (speedPadded)
        {
            maxSpeed = padSpeed;
            padTimer -= Time.deltaTime;
            if(padTimer <= 0)
            {
                padTimer = 0;
                speedPadded = false;
                RestoreDefaults();
            }
        }
    }
    #endregion
    //Death Count
    private IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(elimCount);
        Destroy(this.gameObject);
    }

    void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(DeathCount());
        }
    }

    void OnBecameVisible()
    {
        StopAllCoroutines();
    }
}