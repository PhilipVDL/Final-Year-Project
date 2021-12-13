using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInventory : MonoBehaviour
{
    public int maxObstacles;
    public int currentObstacles;
    public int selectedIndex;
    public List<GameObject> obstacles;

    private void Update()
    {
        CountObstacles();
    }

    void CountObstacles()
    {
        currentObstacles = 0;
        foreach(GameObject obstacle in obstacles)
        {
            if(obstacle != null)
            {
                currentObstacles++;
            }
        }
    }

    void SelectedIndex()
    {
        //switch obstacles
    }
}