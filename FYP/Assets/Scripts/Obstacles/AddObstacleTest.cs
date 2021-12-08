using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObstacleTest : MonoBehaviour
{
    public GameObject oil, tacks;
    public ObstacleInventory inventory;
    private void Start()
    {
        inventory.obstacles.Add(oil);
        inventory.obstacles.Add(tacks);
    }
}
