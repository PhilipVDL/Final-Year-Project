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
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            win.Score(player, points[finished]);
           
            //Destroy(other.gameObject);
        }
    }

    private void Update()
    {
       

        enablePlacing();
    }

    public void NewRound()
    {
        finished = 0;

      
    }

    public void enablePlacing()
    {
        if (finished == GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers && finished != 0)//GameObject.FindWithTag("Player") == null)
        {
            //end round
            Players[0].SetActive(true);
            Players[1].SetActive(true);
            Players[2].SetActive(true);
            Players[3].SetActive(true);
            win.endRound = true;

            GameObject.Find("Player 1").GetComponent<PlayerController>().PlacementMove();
            GameObject.Find("Player 2").GetComponent<PlayerController>().PlacementMove();
            GameObject.Find("Player 3").GetComponent<PlayerController>().PlacementMove();
            GameObject.Find("Player 4").GetComponent<PlayerController>().PlacementMove();
        }



        else if (finished == GameObject.Find("Main Camera").GetComponent<CameraController>().totalPlayers && GameObject.Find("Win").GetComponent<WinState>().currentRound > 1)
        {
            PlayerClones[0].SetActive(true);
            PlayerClones[1].SetActive(true);
            PlayerClones[2].SetActive(true);
            PlayerClones[3].SetActive(true);

            GameObject.Find("Player 1(Clone)").GetComponent<PlayerController>().PlacementMove();
            GameObject.Find("Player 2(Clone)").GetComponent<PlayerController>().PlacementMove();
            GameObject.Find("Player 3(Clone)").GetComponent<PlayerController>().PlacementMove();
            GameObject.Find("Player 4(Clone)").GetComponent<PlayerController>().PlacementMove();
        }

        }
    }
