using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBouncePlayer : MonoBehaviour
{
    private float bounceForce = 250;
    public float bounceForceScale;

    private void Start()
    {
        if(bounceForceScale == 0)
        {
            bounceForceScale = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 point = collision.GetContact(0).point;

            rb.AddExplosionForce(bounceForce * bounceForceScale, point, 1);
        }
    }
}