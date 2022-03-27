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
        playerScores[0].text = "P1      " + winState.GetComponent<WinState>().scores[0].ToString();
        playerScores[1].text = "P2      " + winState.GetComponent<WinState>().scores[1].ToString();
        playerScores[2].text = "P3      " + winState.GetComponent<WinState>().scores[2].ToString();
        playerScores[3].text = "P4      " + winState.GetComponent<WinState>().scores[3].ToString();
    }
}
