using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinJump : MonoBehaviour
{
    Animator anim;
    private int playerNumber;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerNumber = GetComponentInChildren<PlayerJoinVisible>().playerNumber;
    }

    private void Update()
    {
        JumpInput(playerNumber);
    }

    void JumpInput(int pNum)
    {
        if(Input.GetButtonDown("Jump" + pNum))
        {
            Jump();
        }
    }

    void Jump()
    {
        StartCoroutine(JumpAnimation());
    }

    IEnumerator JumpAnimation()
    {
        anim.SetBool("Jump", true);
        yield return new WaitForEndOfFrame();
        anim.SetBool("Jump", false);
    }
}