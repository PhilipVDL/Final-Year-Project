using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesOnMap : MonoBehaviour
{
    public bool test;

    private void Update()
    {
        if (test)
        {
            ActivateObstacles();
            test = false;
        }
    }

    public void ActivateObstacles()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            Debug.Log(child.gameObject);
        }
    }
}