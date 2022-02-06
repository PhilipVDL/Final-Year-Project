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

    public void SelectedIndex(int change)
    {
        selectedIndex += change;

        if(selectedIndex >= obstacles.Count)
        {
            selectedIndex = 0;
        }
        else if(selectedIndex < 0)
        {
            selectedIndex = obstacles.Count - 1;
        }
    }
}