using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    //components
    public EndDistance eDist;
    public Vector3 placementRot;

    //Grids
    public GameObject[] Sections;
    public GameObject LookAt;

    //variables
    Vector3 desiredPos;
    public float defaultHeight;
    public float followDist, verticalZoomDistance, horizontalZoomDistance, verticalZoomScale, horizontalZoomScale, zoomScaleFactor, ZoomMax, maxDistance;
    public float lerpSpeed;
    public float placementCamSpeed;
    public float managerCount;
    public float camCountdown;
    public float maxCount;
    public int currentSection = 0;
    public float sectionDist;
    public float playerDist;
    public int totalPlayers;

    //modes
    public bool placementPhase;



    private void Start()
    {
        CountPlayers();
        eDist = GameObject.Find("End").GetComponent<EndDistance>();
        GetGrids();
    }


    private void FixedUpdate()
    {
        if (placementPhase)
        {
            PlacementPhaseCam();
            transform.rotation = Quaternion.Euler(50, 0, 0);
        }
        else
        {
            CameraMove();
            transform.rotation = Quaternion.Euler(15, 0, 0);
        }

        managerCount = GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown;
    }

    public void CountPlayers()
    {
        totalPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
    }




    void GetGrids()
    {
        Sections[0] = GameObject.Find("CamStart");
        Sections[1] = GameObject.Find("P3");
    }


    private void CameraMove()
    {
        if (GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown > 1 && LookAt != null)
        {
            transform.LookAt(LookAt.transform.position);
        }

        //follow furthest player
        if (eDist.furthestPlayer != null && eDist.playerDifference < ZoomMax && eDist.furthestPlayer.activeInHierarchy != false)
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
        if (eDist.closestPlayer != null)
        {
            if (eDist.playerDifference < maxDistance && eDist.closestPlayer.GetComponent<PlayerController>().grounded == true)
            {
                defaultHeight = eDist.closestPlayer.transform.position.y + 10;
            }
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
        camCountdown -= Time.deltaTime;

        //Go to start
        if (camCountdown > 0)
        {
            desiredPos = Sections[0].transform.position;
            transform.position = Vector3.Lerp(transform.position, desiredPos, 0.1f);
        }

        else
        {  //Set stats
            lerpSpeed = 0.001f;
            defaultHeight = 13;


            camCountdown -= Time.deltaTime;

            desiredPos = new Vector3(Sections[1].transform.position.x, sectionDist, Sections[1].transform.position.z);
            // LookAt = Sections[1];

            transform.position = Vector3.MoveTowards(transform.position, desiredPos, placementCamSpeed);

        }

        if (camCountdown <= -10)
        {

            placementPhase = false;
            camCountdown = maxCount;

        }
    }
}
       







        
    


        
        

    
    
    
