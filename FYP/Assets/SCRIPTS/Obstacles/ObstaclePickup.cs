using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePickup : MonoBehaviour
{
    public GameObject[] obstacles;
    SFXScript sfx;

    private void Start()
    {
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sfx.PlaySFX(sfx.pickupObstacle);
            other.GetComponent<ObstacleInventory>().obstacles.Add(obstacles[RandomObstacle()]); //adds a random obstacle from the list
            Destroy(gameObject);
        }
    }

    int RandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length); //bassic random, equal chance
        return randomIndex;
    }
}