using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");
            other.GetComponent<PlayerController>().Oiled();
        }
    }
}
