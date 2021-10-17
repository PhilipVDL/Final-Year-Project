using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //components
    EndDistance eDist;

    //variables
    Vector3 desiredPos;
    public float defaultHeight;
    public float followDist, verticalZoomDistance, horizontalZoomDistance, verticalZoomScale, horizontalZoomScale, zoomScaleFactor;
    public float lerpSpeed;
    

    private void Start()
    {
        eDist = GameObject.Find("End").GetComponent<EndDistance>();
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        //follow furthest player
        if(eDist.furthestPlayer != null)
        {
            desiredPos = new Vector3(0, defaultHeight, eDist.furthestPlayer.transform.position.z - followDist);
        }

        //zoom
        if (eDist.playerDifference > verticalZoomDistance || eDist.horizontalDifference > horizontalZoomDistance)
        {
            verticalZoomScale = eDist.playerDifference - verticalZoomDistance;
            horizontalZoomScale = eDist.horizontalDifference - horizontalZoomDistance;

            if (verticalZoomScale > horizontalZoomScale)
            {
                desiredPos += (gameObject.transform.forward * verticalZoomScale * zoomScaleFactor * -1);
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