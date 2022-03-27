using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupClippingCheck : MonoBehaviour
{
    float unclipSpeed = 0.2f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("PickupSpawn"))
        {
            Vector3 leave = transform.position - other.transform.position;
            transform.Translate(leave * unclipSpeed);
        }
    }
}