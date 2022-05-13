using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public GameObject[] playerScores;
    public GameObject[] playerImages;

    public GameObject winState;

    void Start()
    {
        winState = GameObject.Find("WinState");
        playerScores = GameObject.FindGameObjectsWithTag("Score");
    }    

    void FixedUpdate()
    {
       GetScore();
    }

    void GetScore()
    {
        playerScores[0].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[0].ToString();
        playerScores[1].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[1].ToString();
        playerScores[2].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[2].ToString();
        playerScores[3].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[3].ToString();
    }
}