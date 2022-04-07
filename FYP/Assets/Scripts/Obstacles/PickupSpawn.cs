using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    public bool placeRandomObstacles;
    public GameObject pickupPrefab;
    public GameObject randomObstaclePrefab;
    BoxCollider collider;

    public int spawnNumber;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        if(spawnNumber <= 0)
        {
            spawnNumber = 1;
        }

        if (placeRandomObstacles)
        {
            SpawnRandomObstacles();
        }
        else
        {
            spawnPickups();
        }
    }

    public Vector3 GetRandomPointInsideCollider(BoxCollider boxCollider)
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y),
            Random.Range(-extents.z, extents.z)
        );

        return boxCollider.transform.TransformPoint(point);
    }

    void spawnPickups()
    {
        for(int i = 0; i < spawnNumber; i++)
        {
            Vector3 spawn = GetRandomPointInsideCollider(collider);
            Instantiate(pickupPrefab, spawn, Quaternion.identity);
        }
    }

    void SpawnRandomObstacles()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            Vector3 spawn = GetRandomPointInsideCollider(collider);
            Instantiate(randomObstaclePrefab, spawn, Quaternion.identity);
        }
    }
}