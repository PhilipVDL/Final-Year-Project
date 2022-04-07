using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBouncePlayer : MonoBehaviour
{
    SFXScript sfx;

    private float bounceForce = 250;
    public float bounceForceScale;

    private void Start()
    {
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
        if(bounceForceScale == 0)
        {
            bounceForceScale = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 point = collision.GetContact(0).point;

            sfx.PlaySFX(sfx.obstacleCollide);

            controller.knockbacked();
            controller.currentSpeed = 0;
            rb.AddExplosionForce(bounceForce * bounceForceScale, point, 1);
        }
    }
}