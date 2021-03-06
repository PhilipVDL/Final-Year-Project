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
        GetScoreText();
    }    

    void FixedUpdate()
    {
       GetScore();
    }

    void GetScoreText()
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("Score");
        playerScores = new GameObject[scoreTexts.Length];
        foreach(GameObject st in scoreTexts)
        {
            switch (st.name)
            {
                case "Player 1 Score":
                    playerScores[0] = st;
                    break;
                case "Player 2 Score":
                    playerScores[1] = st;
                    break;
                case "Player 3 Score":
                    playerScores[2] = st;
                    break;
                case "Player 4 Score":
                    playerScores[3] = st;
                    break;
            }
        }
    }

    void GetScore()
    {
        playerScores[0].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[0].ToString();
        playerScores[1].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[1].ToString();
        playerScores[2].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[2].ToString();
        playerScores[3].GetComponent<Text>().text =  winState.GetComponent<WinState>().scores[3].ToString();
    }
}