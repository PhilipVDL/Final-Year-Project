using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //components
   public EndDistance eDist;

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
    }

    private void CameraMove()
    {
        //follow furthest player
        if (eDist.furthestPlayer != null && eDist.playerDifference < ZoomMax)
        {
            
            desiredPos = new Vector3(0, defaultHeight, eDist.furthestPlayer.transform.position.z - followDist );
        }
        else if (eDist.playerDifference > ZoomMax)
        {
            desiredPos = new Vector3(0, defaultHeight, eDist.furthestPlayer.transform.position.z - followDist);
        }
        //Slope movement

        if (eDist.playerDifference < maxDistance && eDist.closestPlayer.GetComponent<PlayerController>().grounded == true)
        {
            defaultHeight = eDist.closestPlayer.transform.position.y + 12;
        }
      
        //elimination zoom
         if(eDist.playerDifference >= maxDistance)
        {
            followDist -= 2 * Time.deltaTime;
            verticalZoomScale = 0;
        }
        else if (eDist.playerDifference >= maxDistance + 3)
        {
            followDist -= 4 * Time.deltaTime;
            verticalZoomScale = 0;
        }
        else if(eDist.playerDifference <= 10)
        {
            zoomScaleFactor = 1;
        }

         if(followDist <= 3 && eDist.players.Length > 1)
        {
            Destroy(eDist.furthestPlayer);
            followDist = 8;
        }

         if(eDist.players.Length == 1)
        {
            followDist = 7;
        }

        //zoom
        if (eDist.playerDifference > verticalZoomDistance || eDist.horizontalDifference > horizontalZoomDistance)
        {
            verticalZoomScale = eDist.playerDifference - verticalZoomDistance;
            horizontalZoomScale = eDist.horizontalDifference - horizontalZoomDistance;

            if (verticalZoomScale > horizontalZoomScale)
            {
                //desiredPos += (gameObject.transform.forward * verticalZoomScale * zoomScaleFactor * -1);
            }
            else if (verticalZoomScale < horizontalZoomScale)
            {
                desiredPos += (gameObject.transform.forward * horizontalZoomScale * zoomScaleFactor * -1);
            }
        }
       

        //lerp

        transform.position = Vector3.Lerp(transform.position, desiredPos, lerpSpeed);
        
    }
}