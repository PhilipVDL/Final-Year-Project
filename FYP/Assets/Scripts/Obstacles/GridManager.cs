using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] zones;
    public int smallestX, largestX, smallestZ, largestZ;

    private void Start()
    {
        zones = GameObject.FindGameObjectsWithTag("Grid Zone");
        CoordSetup();
    }

    void CoordSetup()
    {
        smallestX = smallestZ = largestX = largestZ = 0;
        for (int i = 0;i < zones.Length; i++)
        {
            GridZone zone = zones[i].GetComponent<GridZone>();
            //smallest/largest
            if(zone.gridX < smallestX)
            {
                smallestX = zone.gridX;
            }
            else if(zone.gridX > largestX)
            {
                largestX = zone.gridX;
            }

            if (zone.gridZ < smallestZ)
            {
                smallestZ = zone.gridZ;
            }
            else if(zone.gridZ > largestZ)
            {
                largestZ = zone.gridZ;
            }
        }
    }
}