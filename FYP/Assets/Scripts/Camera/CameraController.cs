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
    public float followDist, zoomDistance, zoomScale, zoomScaleFactor;
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
        desiredPos = new Vector3(0, defaultHeight, eDist.furthestPlayer.transform.position.z - followDist);
        //zoom closest
        if (eDist.playerDifference > zoomDistance)
        {
            zoomScale = eDist.playerDifference - zoomDistance;
            //transform.Translate(Vector3.forward * -zoomScale * zoomScaleFactor);
            desiredPos += (gameObject.transform.forward * -zoomScale * zoomScaleFactor);
        }

        //lerp
        transform.position = Vector3.Lerp(transform.position, desiredPos, lerpSpeed);
    }
}