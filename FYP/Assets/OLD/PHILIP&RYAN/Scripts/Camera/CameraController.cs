using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    //components
    public EndDistance eDist;

    //Grids
    public GameObject[] Grids;
    public GameObject LookAt;

    //variables
    Vector3 desiredPos;
    public float defaultHeight;
    public float followDist, verticalZoomDistance, horizontalZoomDistance, verticalZoomScale, horizontalZoomScale, zoomScaleFactor, ZoomMax, maxDistance;
    public float lerpSpeed;
    public float managerCount;
    public float camCountdown;
    public float maxCount;
    public int currentGrid = 0;
    public float gridDist;
    public float playerDist;
    public float totalPlayers;

    //modes
    public bool placementPhase;


    private void Start()
    {

        eDist = GameObject.Find("End").GetComponent<EndDistance>();
        GetGrids();

    }

    private void FixedUpdate()
    {
        if (placementPhase)
        {
            PlacementPhaseCam();
        }
        else { CameraMove(); }

        managerCount = GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown;



    }



    void GetGrids()
    {
        Grids[0] = GameObject.Find("Grid 1");
        Grids[1] = GameObject.Find("Grid 2");
        Grids[2] = GameObject.Find("Grid 3");
    }

    private void CameraMove()
    {
        if (GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown > 1)
        {
            transform.LookAt(LookAt.transform.position);
        }
        //follow furthest player
        if (eDist.furthestPlayer != null && eDist.playerDifference < ZoomMax)
        {
            lerpSpeed = 0.1f;
            desiredPos = new Vector3(0, defaultHeight, eDist.furthestPlayer.transform.position.z - followDist);
        }

        else if (eDist.playerDifference > ZoomMax)
        {
            lerpSpeed = 0.0045f;
            desiredPos = new Vector3(0, defaultHeight, eDist.closestPlayer.transform.position.z - followDist);
        }

        else if (eDist.players.Length == 1)
        {
            lerpSpeed = 0.1f;
            desiredPos = new Vector3(eDist.closestPlayer.transform.position.x, defaultHeight, eDist.closestPlayer.transform.position.z - followDist);
        }

        //Slope movement

        if (eDist.playerDifference < maxDistance && eDist.closestPlayer.GetComponent<PlayerController>().grounded == true)
        {
            defaultHeight = eDist.closestPlayer.transform.position.y + 10;
        }

        //zoom
        if (eDist.playerDifference > verticalZoomDistance || eDist.playerDifference > horizontalZoomDistance || eDist.players.Length == 1)
        {
            verticalZoomScale = eDist.playerDifference - verticalZoomDistance;
            horizontalZoomScale = eDist.horizontalDifference - horizontalZoomDistance;


            if (verticalZoomScale > horizontalZoomScale)
            {
                desiredPos += (gameObject.transform.up * verticalZoomScale * zoomScaleFactor * .1f);
                desiredPos += (gameObject.transform.forward * verticalZoomScale * zoomScaleFactor * -.1f);
            }

            else if (verticalZoomScale < horizontalZoomScale)
            {
                desiredPos += (gameObject.transform.forward * horizontalZoomScale * zoomScaleFactor * -.5f);
            }



            //lerp

            transform.position = Vector3.Lerp(transform.position, desiredPos, lerpSpeed);

        }
    }

    public void PlacementPhaseCam()
    {
        //Set stats
        lerpSpeed = 0.01f;
        defaultHeight = 18;
        followDist = gridDist;

        camCountdown -= Time.deltaTime;
        LookAt = Grids[currentGrid];

        // Grid Calculations
        if (camCountdown <= 0 && currentGrid == 0)
        {
            currentGrid = 1;
            camCountdown = maxCount;
        }

        if (currentGrid == 0)
        {
            Grids[1].SetActive(false);
            Grids[0].SetActive(true);
            Grids[2].SetActive(false);
        }

        if (camCountdown <= 0 && currentGrid == 1)
        {
            currentGrid = 2;
            camCountdown = maxCount;
        }

        if (camCountdown <= 0 && currentGrid == 2)
        {
            currentGrid = 0;
            followDist = playerDist;
            LookAt = GameObject.Find("LookAt");
            GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown = 3;
            GameObject.Find("WinState").GetComponent<WinState>().NewRound();
            placementPhase = false;
        }


        //Grid Movement
        if (currentGrid != 0)
        {
            Grids[currentGrid - 1].SetActive(false);
        }
        Grids[currentGrid].SetActive(true);

        desiredPos = new Vector3(Grids[currentGrid].transform.position.x, defaultHeight, Grids[currentGrid].transform.position.z);
        transform.LookAt(LookAt.transform.position);
        transform.position = Vector3.Lerp(transform.position, desiredPos, lerpSpeed);
    }


}        
        

    
    
    
