using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    //components
   public EndDistance eDist;
   public float managerCount;

    //variables
    Vector3 desiredPos;
    public float defaultHeight;
    public float followDist, verticalZoomDistance, horizontalZoomDistance, verticalZoomScale, horizontalZoomScale, zoomScaleFactor, ZoomMax, maxDistance;
    public float lerpSpeed;
    

    private void Start()
    {
        eDist = GameObject.Find("End").GetComponent<EndDistance>();
        
    }

    private void FixedUpdate()
    {
        CameraMove();
        managerCount = GameObject.Find("BackgroundTasks").GetComponent<MainManager>().countdown;
    }

    private void CameraMove()
    {
        
        
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
        if (eDist.playerDifference > verticalZoomDistance || eDist.playerDifference > horizontalZoomDistance || eDist.players.Length == 1 )
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
    
}