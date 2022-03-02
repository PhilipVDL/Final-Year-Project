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
    public GameObject cam;
    public EndDistance end;


    private void Start()
    {
        win = GameObject.Find("WinState").GetComponent<WinState>();
        finished = 0;
        PlayerClones = GameObject.FindGameObjectsWithTag("Player");
        cam = GameObject.Find("Main Camera");
    }
        

    private void OnTriggerEnter(Collider other)
    {
        //score
        if (other.CompareTag("Player") && Players.Length == 1)
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            GameObject playerObj = other.gameObject;
            
            win.Score(player, points[finished]);
            other.gameObject.transform.position = win.spawns[0].transform.position;
            EnablePlacing();
          //  GameObject.Find("Place Zone Manager").GetComponent<PlaceZoneManager>().activeZone = 1;
          
            
        }

        else
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            GameObject playerObj = other.gameObject;

            win.Score(player, points[finished]);
            other.gameObject.transform.position = win.spawns[0].transform.position;
            other.gameObject.SetActive(false);
            GameObject.Find("UI").GetComponent<RankingUi>().positions[0] = null;
            

           
        }

     
    }

    private void Update()
    {
        GetPlayers();

      
    }    
    

  

    void GetPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void NewRound()
    {
        finished = 0;
    }

    public void EnablePlacing()
    {
        //(GameObject.FindWithTag("Player") == null)

        //end round
        win.endRound = true;
        finished = 0;

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].SetActive(true);
        }

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].transform.position = win.spawns[0].transform.position;
        }

        for (int i = 0; i < PlayerClones.Length; i++)
        {
            PlayerClones[i].GetComponent<PlayerController>().PlacementMove();
        }

     
       
    }
}