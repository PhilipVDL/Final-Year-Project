using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePickup : MonoBehaviour
{
    public GameObject[] obstacles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ObstacleInventory>().obstacles.Add(obstacles[RandomObstacle()]); //adds a random obstacle from the list
        }

        Destroy(gameObject);
    }

    int RandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length); //bassic random, equal chance
        return randomIndex;
    }
}