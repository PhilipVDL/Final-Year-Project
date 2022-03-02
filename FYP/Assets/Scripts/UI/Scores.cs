using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text[] playerScores;

    public GameObject winState;
    // Start is called before the first frame update
    void Start()
    {
        winState = GameObject.Find("WinState");
    }    

    

    // Update is called once per frame
    void FixedUpdate()
    {
        GetScore();
    }

    void GetScore()
    {
        playerScores[0].text = "Player 1 - " +  winState.GetComponent<WinState>().scores[0].ToString();
        playerScores[1].text = "Player 2 - " + winState.GetComponent<WinState>().scores[1].ToString();
        playerScores[2].text = "Player 3 - " + winState.GetComponent<WinState>().scores[2].ToString();
        playerScores[3].text = "Player 4 - " + winState.GetComponent<WinState>().scores[3].ToString();
    }
}
