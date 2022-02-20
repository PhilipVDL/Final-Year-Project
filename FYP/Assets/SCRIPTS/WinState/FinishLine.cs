using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    WinState win;
    public int[] points = new int[4];
    public int finished;
    public GameObject[] Players;
    public GameObject[] PlayerClones;

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
            other.gameObject.transform.position = win.spawns[0].transform.position;
            other.gameObject.GetComponent<PlayerController>().currentSpeed = 0;
           finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);

        
        }
    }

    private void Update()
    {
       EnablePlacing();
    }

    public void NewRound()
    {
        finished = 0;

      
    }

    public void EnablePlacing()
    {
        if (finished == GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers && finished != 0)//GameObject.FindWithTag("Player") == null)
        {
            //end round
            Players[0].SetActive(false);
            
            
            win.endRound = true;

            GameObject.Find("Player 1").GetComponent<PlayerController>().PlacementMove();
            
        }



      
        
        }
    }
