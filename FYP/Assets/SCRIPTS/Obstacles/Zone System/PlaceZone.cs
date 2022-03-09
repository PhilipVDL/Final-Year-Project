using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceZone : MonoBehaviour
{
    public GameObject[] previews;
    Collider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        previews = GameObject.FindGameObjectsWithTag("Player Obstacle Preview");
        PreviewBounding();
    }

    void PreviewBounding()
    {
        foreach (GameObject preview in previews)
        {
            if (!myCollider.bounds.Contains(preview.transform.position)) //if preview not in bounds
            {
                preview.transform.position = transform.position; //move to me
            }
        }
    }

    private void Update()
    {
        PreviewBounding();
    }
}