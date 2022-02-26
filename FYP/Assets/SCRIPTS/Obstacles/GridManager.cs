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

    public Transform FindGridZone(int x, int z, int playerNumber, GameObject obstacle)
    {
        Transform tf = null;
        foreach(GameObject zone in zones)
        {
            GridZone gz = zone.GetComponent<GridZone>();
            if(gz.gridX == x && gz.gridZ == z && !gz.filled)
            {
                tf = zone.transform;
                gz.Fill(obstacle);
            }
        }
        return tf;
    }

    public void HighlightGridZone(int x, int z, int playerNumber)
    {
        foreach (GameObject zone in zones)
        {
            GridZone gz = zone.GetComponent<GridZone>();
            if (gz.gridX == x && gz.gridZ == z)
            {
                gz.Highlight(playerNumber);
            }
        }
    }
}