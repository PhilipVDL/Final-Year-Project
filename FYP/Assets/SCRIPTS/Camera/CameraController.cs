using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{

    WinState win;
    FinishLine finish;
    //components
    public EndDistance eDist;
   // public Vector3 placementRot;

    //Grids
    public GameObject[] Sections;
    public GameObject[] Zones;
    public GameObject LookAt;
    public GameObject ZoneManager;

  

    //UI
    public GameObject UI;

    //variables
    Vector3 desiredPos;
    public float defaultHeight;
    public float followDist, verticalZoomDistance, horizontalZoomDistance, verticalZoomScale, horizontalZoomScale, zoomScaleFactor, ZoomMax, maxDistance;
    public float lerpSpeed;
   // public float placementCamSpeed;
    public float managerCount;
    public float camCountdown;
    public float maxCount;
    public int currentSection = 0;
    public float sectionDist;
    public float playerDist;
    public int totalPlayers;
    public GameObject[] Players;

    //modes
   // public bool placementPhase;



    private void Start()
    {
        CountPlayers();
        eDist = GameObject.Find("End").GetComponent<EndDistance>();
        
        Zones = GameObject.FindGameObjectsWithTag("Zone");
        Players = GameObject.FindGameObjectsWithTag("Player");
        managerCount = GameObject.Find("Background Tasks").GetComponent<MainManager>().countdown;
    }


    private void FixedUpdate()
    {
        /*if (placementPhase)
       {
           PlacementPhaseCam();

           transform.rotation = Quaternion.Euler(50, 0, 0);
       }
       */


        CameraMove();
        transform.rotation = Quaternion.Euler(15, 0, 0);




        if (managerCount >= 3)
        {
            //Reset zone focus, phase and countdown
            // ZoneManager.GetComponent<PlaceZoneManager>().activeZone = 0;

            // camCountdown = maxCount;

            //reset start timer

           
            managerCount = 3;



            Players[0].GetComponent<PlayerController>().checkpointActivations[0].SetActive(false);
            Players[0].GetComponent<PlayerController>().checkpointActivations[1].SetActive(false);

        }
    }
            
        

        
    

    public void CountPlayers()
    {
        totalPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
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
        if (camCountdown > maxCount - 5)
        {
            UI.GetComponent<RankingUi>().placementModetext.SetActive(true);
            desiredPos = Sections[0].transform.position;
            transform.position = Vector3.Lerp(transform.position, desiredPos, 0.1f);
        }

    }


}

        
    

       







        
    


        
        

    
    
    
