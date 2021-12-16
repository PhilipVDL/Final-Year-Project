using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    WinState win;
    public int[] points = new int[4];
    public int finished;

    private void Start()
    {
        win = GameObject.Find("WinState").GetComponent<WinState>();
        finished = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //score
        if (other.CompareTag("Player"))
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);
           
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if(GameObject.FindWithTag("Player") == null)
        {
            //end round
            win.endRound = true;
        }
    }

    public void NewRound()
    {
        finished = 0;
    }
}