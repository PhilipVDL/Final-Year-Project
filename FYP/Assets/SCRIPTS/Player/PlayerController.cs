using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //components
    Rigidbody prb;
    GridManager gm;
    RaycastHit hit;
    SFXScript sfx;
    ObstacleInventory inventory;
    PlayerObstacles playerObstacles;
    PlayerObstaclesRacePlace playerObstaclesRacePlace;
    GameObject obstaclesOnMap;
    public GameObject spawn;
    EndDistance end;
    public GameObject[] Checkpoints;
    public GameObject particleSys;
    public FinishLine finishLine;

    //variables
    #region variables
    [Header("Control Modes")]
    [Range(1, 4)] public int playerNumber;

    [Header("Knockback")]
    public bool knockBacking;
    public float knockBackTime;
    private float knockBackTimer;
    public float knockbackForce;
    public float knockbackMult;

    [Header("Move Speeds")]
    public bool moveBackwards;
    public float currentSpeed;
    public float maxSpeed;
    public float maxBackSpeed;
    public float timeToMaxSpeed;
    public float boostTime;
    public float minSpeed;
    public float timeToMinSpeed;
    public float fDamp;

    public bool braking, speeding, goLeft, goRight;

    [Header("Position")]
    public int pos;

    [Header("Checkpoint Activation")]
    public GameObject[] checkpointActivations;

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

    //[Header("Placement Mode")]
    //public bool placementMode;

    [Header("Respawn")]
    public float deathHeight;
    public bool doesRespawn;
    public GameObject currentSpawn;
    private int currentSpawnNumber;

    [Header("Elim Count")]
    public float elimCount = 1.5f;

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

    [Header("Speed Pad Effect")]
    public bool speedPadded;
    public float padSpeed;
    public float padDuration;
    public float padTimer;
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Knockback(collision.gameObject, collision.GetContact(0).point);
        }
    }

    void Knockback(GameObject other, Vector3 point)
    {
        sfx.PlaySFX(sfx.playerCollide);
        knockbacked();
        prb.AddExplosionForce(knockbackForce * knockbackMult, point, 1);

        //PlayerController otherController = other.GetComponent<PlayerController>();
        //Rigidbody otherRB = other.GetComponent<Rigidbody>();
        //otherRB.AddExplosionForce(otherController.knockbackForce * otherController.knockbackMult, point, 1);
    }

    public void knockbacked()
    {
        knockBackTimer = knockBackTime;
        knockBacking = true;
    }

    void knockbackTimer()
    {
        if (knockBacking)
        {
            knockBackTimer -= Time.deltaTime;
            if(knockBackTimer <= 0)
            {
                knockBacking = false;
            }
        }
    }

    private void Start()
    {
        elimCount = 2.5f;
        finishLine = GameObject.Find("Finish").GetComponent<FinishLine>();
        prb = GetComponent<Rigidbody>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
        inventory = GetComponent<ObstacleInventory>();
        playerObstacles = GetComponent<PlayerObstacles>();
        playerObstaclesRacePlace = GetComponent<PlayerObstaclesRacePlace>();
        obstaclesOnMap = GameObject.Find("ObstaclesOnMap");

        Defaults();
        GetSpawnPos();
        Checkpoints = GameObject.FindGameObjectsWithTag("CP");
        currentSpawn = spawn;
        particleSys = GameObject.Find("UniParticle");

        gameObject.name = "Player " + playerNumber;
    }

    void GetSpawnPos()
    {
        switch (playerNumber)
        {
            case 1:
                spawn = GameObject.Find("Spawn 1");
                break;
            case 2:
                spawn = GameObject.Find("Spawn 2");
                break;
            case 3:
                spawn = GameObject.Find("Spawn 3");
                break;
            case 4:
                spawn = GameObject.Find("Spawn 4");
                break;
        }
        // spawn = GameObject.Find("StartSpawn");
        gameObject.name = "Player " + playerNumber;
    }

    void Defaults()
    {
        defaultMaxSpeed = maxSpeed;
        defaultAirControl = airControlMult;
        defaultAirSpeed = jumpSpeedMult;
    }

    private void Update()
    {
        // GetSpawnPos();
        GroundCheck();
        PlayerInput();
        ObstacleTimers();
        knockbackTimer();
        Begin();
        Respawn();
    }

    public void Begin()
    {
        if (GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown <= 0 && !knockBacking)
        {
            MoveCalculations();
        }
    }

    void PlayerInput()
    {
        ControlsControlledJump(playerNumber);
    }

    #region controls
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

        if (!goRight && !goLeft && !knockBacking)
        {
            StrafingDamping();
        }
        else if (knockBacking)
        {
            StrafingDamping();
        }

        if (currentSpeed == 0)
        {
            MovementDamping();
        }

        if (Input.GetButton("Jump" + playerNumber) && grounded)
        {
            chargingJump = true;
        }

        if (chargingJump && !grounded)
        {
            chargingJump = false;
            currentJumpForce = 0;
        }

        if (Input.GetButtonUp("Jump" + playerNumber) && grounded)
        {
            chargingJump = false;
            Jump();
        }

        if (Input.GetButtonDown("ObstacleSwitch" + playerNumber))
        {
            if (Input.GetAxis("ObstacleSwitch" + playerNumber) > 0)
            {
                inventory.SelectedIndex(1);
            }
            else if (Input.GetAxis("ObstacleSwitch" + playerNumber) < 0)
            {
                inventory.SelectedIndex(-1);
            }
        }

        if (Input.GetButtonDown("ObstaclePlace" + playerNumber))
        {
            playerObstaclesRacePlace.PlaceObstacle();
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
        float accRate;
        accRate = (maxSpeed / timeToMaxSpeed) * Time.deltaTime;
        if (speeding) //forward to move
        {
            currentSpeed += accRate;
        }
        else if (moveBackwards && braking) //move backwards
        {
            accRate = (maxBackSpeed / timeToMaxSpeed) * Time.deltaTime;
            currentSpeed += accRate;
            if (currentSpeed > maxBackSpeed)
            {
                currentSpeed = maxBackSpeed;
            }
        }
        else
        {
            currentSpeed -= accRate; //slow down if no input
        }
    }

    void Braking()
    {
        if (braking && !moveBackwards)
        {
            currentSpeed -= (maxSpeed / timeToMinSpeed) * Time.deltaTime;
        }
        else if (braking && moveBackwards)
        {
            if (prb.velocity.z > 0)
            {
                prb.velocity = new Vector3(prb.velocity.x, prb.velocity.y, 0);
            }
            prb.AddForce(transform.forward * currentSpeed * -1);
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
        if (grounded && !braking)
        {
            //move normal
            if (prb.velocity.z < 0)
            {
                prb.velocity = new Vector3(prb.velocity.x, prb.velocity.y, 0);
            }
            prb.AddForce((transform.forward * currentSpeed));
        }
        else if (!braking)
        {
            //move air
            if (prb.velocity.z < 0)
            {
                prb.velocity = new Vector3(prb.velocity.x, prb.velocity.y, 0);
            }
            prb.AddForce((transform.forward * currentSpeed * jumpSpeedMult));
        }
        MovementMax();
    }

    void MovementMax()
    {
        if (grounded)
        {
            if (prb.velocity.z > maxSpeed) //forward
            {
                float brakeMag = prb.velocity.z - maxSpeed;
                prb.AddForce(transform.forward * brakeMag * -1);
            }
            else if (Mathf.Abs(prb.velocity.z) > maxBackSpeed) //backwards
            {
                float brakeMag = Mathf.Abs(prb.velocity.z) - maxBackSpeed;
                prb.AddForce(transform.forward * brakeMag);
            }
        }
        else
        {
            if (prb.velocity.z > (maxSpeed * jumpSpeedMult)) //forwards air
            {
                float brakeMag = prb.velocity.z - (maxSpeed * jumpSpeedMult);
                prb.AddForce(transform.forward * brakeMag * -1);
            }
            else if (Mathf.Abs(prb.velocity.z) > (maxBackSpeed * jumpSpeedMult)) //backwards air
            {
                float brakeMag = Mathf.Abs(prb.velocity.z) - (maxBackSpeed * jumpSpeedMult);
                prb.AddForce(transform.forward * brakeMag);
            }
        }
    }

    void MovementDamping()
    {
        //damp at 0 speed
        float dampZ = Mathf.Lerp(prb.velocity.z, 0, fDamp);
        Vector3 dampedVelocity = new Vector3(prb.velocity.x, prb.velocity.y, dampZ);
        prb.velocity = dampedVelocity;
    }

    void Strafing()
    {
        //rigidbody
        if (grounded)
        {
            if (goRight)
            {
                if (prb.velocity.x >= 0) //if moving right
                {
                    if (currentSpeed * horizontalMoveSpeedMultiplier > horizontalMoveSpeedMin)
                    {
                        prb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier);
                    }
                    else
                    {
                        prb.AddForce(transform.right * horizontalMoveSpeedMin);
                    }
                }
                else
                {
                    prb.velocity = new Vector3(prb.velocity.x * -1, prb.velocity.y, prb.velocity.z); //switch direction
                }
            }
            else if (goLeft)
            {
                if (prb.velocity.x <= 0) //if moving left
                {
                    if (currentSpeed * horizontalMoveSpeedMultiplier > horizontalMoveSpeedMin)
                    {
                        prb.AddForce(transform.right * currentSpeed * horizontalMoveSpeedMultiplier * -1);
                    }
                    else
                    {
                        prb.AddForce(transform.right * horizontalMoveSpeedMin * -1);
                    }
                }
                else
                {
                    prb.velocity = new Vector3(prb.velocity.x * -1, prb.velocity.y, prb.velocity.z); //switch direction
                }
            }
        }
        StrafingMax();
    }

    void StrafingMax()
    {
        if (grounded)
        {
            if (Mathf.Abs(prb.velocity.x) > (maxSpeed * horizontalMoveSpeedMultiplier))
            {
                float brakeMag = Mathf.Abs(prb.velocity.x) - (maxSpeed * horizontalMoveSpeedMultiplier);
                if (goRight)
                {
                    prb.AddForce(transform.right * brakeMag * -1);
                }
                else if (goLeft)
                {
                    prb.AddForce(transform.right * brakeMag);
                }
            }
        }
        else
        {
            if (Mathf.Abs(prb.velocity.x) > (maxSpeed * horizontalMoveSpeedMultiplier * jumpSpeedMult))
            {
                float brakeMag = Mathf.Abs(prb.velocity.x) - (maxSpeed * horizontalMoveSpeedMultiplier * jumpSpeedMult);
                if (goRight)
                {
                    prb.AddForce(transform.right * brakeMag * -1);
                }
                else if (goLeft)
                {
                    prb.AddForce(transform.right * brakeMag);
                }
            }
        }
    }

    void StrafingDamping()
    {
        //damp horizontal movement
        float dampX = Mathf.Lerp(prb.velocity.x, 0, hDamp);
        Vector3 dampedVelocity = new Vector3(dampX, prb.velocity.y, prb.velocity.z);
        prb.velocity = dampedVelocity;
    }

    void ChargeJump()
    {
        float chargeRate = 15 * Time.deltaTime;
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
        prb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        currentJumpForce = 0;
    }

    void Respawn()
    {
       
        if (transform.position.y < deathHeight && !doesRespawn)
        {
            Destroy(gameObject);
        }
         else if (transform.position.y < deathHeight && doesRespawn && GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers != 1)
         {
             transform.position = currentSpawn.transform.position;
          //  particleSys.SetActive(true);
            // Instantiate(particleSys, transform.position, transform.rotation);
            currentSpeed = 0;
         }
         
        else if (transform.position.y < deathHeight && doesRespawn && GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers == 1)
        {

            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[0].SetActive(true);
            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[1].SetActive(true);
            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[2].SetActive(true);
            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[3].SetActive(true);

            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[0].transform.position = currentSpawn.transform.position;
            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[1].transform.position = currentSpawn.transform.position;
            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[2].transform.position = currentSpawn.transform.position;
            GameObject.Find("Finish").GetComponent<FinishLine>().PlayerClones[3].transform.position = currentSpawn.transform.position;
            transform.position = currentSpawn.transform.position;
          //  particleSys.SetActive(true);
            //Instantiate(particleSys, transform.position, transform.rotation);

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
        if (oiled)
        {
            maxSpeed = oilSpillSpeed;
            oilSpillTimer -= Time.deltaTime;
            if (oilSpillTimer <= 0)
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
            if (tackTimer <= 0)
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
            if (trampolineTimer <= 0)
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
            if (padTimer <= 0)
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
        GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers--;
        this.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {

            //currentSpawn = spawn;
            speeding = false;
            currentSpeed = 0;
            currentSpawn = spawn;
            transform.position = spawn.transform.position;
            braking = true;
        }

        if (GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers == 1 && other.CompareTag("Checkpoint 1"))
        {
            for(int i = 0; i < finishLine.PlayerClones.Length - 1; i++)
            {
                finishLine.PlayerClones[i + 1].SetActive(true);
                finishLine.PlayerClones[i].GetComponent<PlayerController>().currentSpawn = Checkpoints[i];
                finishLine.PlayerClones[i + 1].GetComponent<PlayerController>().currentSpawn = Checkpoints[i + 1];
                //finishLine.PlayerClones[i + 1].GetComponent<PlayerController>().particleSys.SetActive(true);
                finishLine.PlayerClones[i + 1].transform.position = currentSpawn.transform.position;
                //other.GetComponent<Checkpoint>().notifiers[0].SetActive(true);
            }

        }
        else if(GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers == 1 && other.CompareTag("Checkpoint 2"))
        {
            for (int i = 0; i < finishLine.PlayerClones.Length -1; i++)
            {
                finishLine.PlayerClones[i + 1].SetActive(true);
                finishLine.PlayerClones[i].GetComponent<PlayerController>().currentSpawn = Checkpoints[i + 4];
                finishLine.PlayerClones[i + 1].GetComponent<PlayerController>().currentSpawn = Checkpoints[i + 5];
                // finishLine.PlayerClones[i + 1].GetComponent<PlayerController>().particleSys.SetActive(true);
                finishLine.PlayerClones[i + 1].transform.position = currentSpawn.transform.position;
               // other.GetComponent<Checkpoint>().notifiers[1].SetActive(true);
            }

        }
        else if (GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers == 1 && other.CompareTag("Checkpoint 3"))
        {
            for (int i = 0; i < finishLine.PlayerClones.Length - 1; i++)
            {
                finishLine.PlayerClones[i + 1].SetActive(true);
                finishLine.PlayerClones[i].GetComponent<PlayerController>().currentSpawn = Checkpoints[i + 8];
                finishLine.PlayerClones[i + 1].GetComponent<PlayerController>().currentSpawn = Checkpoints[i + 9];
                // finishLine.PlayerClones[i + 1].GetComponent<PlayerController>().particleSys.SetActive(true);
                finishLine.PlayerClones[i + 1].transform.position = currentSpawn.transform.position;
              //  other.GetComponent<Checkpoint>().notifiers[2].SetActive(true);
            }

        }

        else
        {
            switch (other.tag)
            {
                case "Checkpoint 1":
                    switch (playerNumber)
                    {
                        case 1:
                            currentSpawn = Checkpoints[0];
                            break;
                        case 2:
                            currentSpawn = Checkpoints[1];
                            break;
                        case 3:
                            currentSpawn = Checkpoints[2];
                            break;
                        case 4:
                            currentSpawn = Checkpoints[3];
                            break;
                    }
                    //checkpointActivations[0].SetActive(true);
                    //other.GetComponent<Checkpoint>().notifiers[0].SetActive(true);
                    break;
                case "Checkpoint 2":
                    switch (playerNumber)
                    {
                        case 1:
                            currentSpawn = Checkpoints[4];
                            break;
                        case 2:
                            currentSpawn = Checkpoints[5];
                            break;
                        case 3:
                            currentSpawn = Checkpoints[6];
                            break;
                        case 4:
                            currentSpawn = Checkpoints[7];
                            break;
                    }
                   //checkpointActivations[1].SetActive(true);
                   //other.GetComponent<Checkpoint>().notifiers[1].SetActive(true);
                    break;
                case "Checkpoint 3":
                    switch (playerNumber)
                    {
                        case 1:
                            currentSpawn = Checkpoints[8];
                            break;
                        case 2:
                            currentSpawn = Checkpoints[9];
                            break;
                        case 3:
                            currentSpawn = Checkpoints[10];
                            break;
                        case 4:
                            currentSpawn = Checkpoints[11];
                            break;
                    }
                    //checkpointActivations[2].SetActive(true);
                    //other.GetComponent<Checkpoint>().notifiers[1].SetActive(true);
                    break;
            }
        }
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