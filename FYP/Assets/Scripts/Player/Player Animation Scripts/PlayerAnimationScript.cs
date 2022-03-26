using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    PlayerController pc;
    Rigidbody rb;
    Animator anim;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PassBools();
    }

    void PassBools()
    {
        if(pc.currentSpeed == 0 && !pc.speeding && !pc.braking && !pc.goRight && !pc.goLeft)
        {
            anim.SetBool("IsIdle", true); //idle
            anim.SetBool("IsForward", false);
            anim.SetBool("IsBackward", false);
            anim.SetBool("IsRight", false);
            anim.SetBool("IsLeft", false);
        }
        else
        {
            anim.SetBool("IsIdle", false); //not idle

            //forward/backward
            if (pc.currentSpeed > 0 && rb.velocity.z > 0)
            {
                anim.SetBool("IsForward", true);
                anim.SetBool("IsBackward", false);
            }
            else if (pc.currentSpeed > 0 && rb.velocity.z < 0)
            {
                anim.SetBool("IsForward", false);
                anim.SetBool("IsBackward", true);
            }
            else if (pc.currentSpeed == 0 || rb.velocity.z == 0)
            {
                anim.SetBool("IsForward", false);
                anim.SetBool("IsBackward", false);
            }

            //right/left
            if (pc.goRight)
            {
                anim.SetBool("IsRight", true);
                anim.SetBool("IsLeft", false);
            }
            else if (pc.goLeft)
            {
                anim.SetBool("IsRight", false);
                anim.SetBool("IsLeft", true);
            }
            else if (!pc.goRight && !pc.goLeft)
            {
                anim.SetBool("IsRight", false);
                anim.SetBool("IsLeft", false);
            }
        }
    }
}