using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    WinState win;
    public int[] points = new int[4];
    public int finished;
    public List<GameObject> Players;
    public GameObject[] PlayerClones;

    private void Start()
    {
        win = GameObject.Find("WinState").GetComponent<WinState>();
        finished = 0;
        GetPlayers();
    }

    private void OnTriggerEnter(Collider other)
    {
        //score
        if (other.CompareTag("Player"))
        {
            finished++;
            int player = other.gameObject.GetComponent<PlayerController>().playerNumber;
            GameObject playerObj = other.gameObject;
            
            win.Score(player, points[finished]);

            EnablePlacing();
            other.gameObject.transform.position = win.spawns[0].transform.position;
            //Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        
       
    }

  

    void GetPlayers()
    {
        Players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
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

            foreach (GameObject player in Players)
            {
                
                player.SetActive(true);
                player.GetComponent<PlayerController>().PlacementMove();
                
            }

          
        
    }
}