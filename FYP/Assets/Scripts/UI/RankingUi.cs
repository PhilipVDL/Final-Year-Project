using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingUi : MonoBehaviour
{
    public EndDistance end;
    public FinishLine finishLine;

    public GameObject[] playerImages;
    public GameObject[] players;
    public GameObject[] positions;

    public GameObject placementModetext;

    public float lerpSpd = 10f;

    private void Awake()
    {
        finishLine = GameObject.Find("Finish").GetComponent<FinishLine>();
        end = GameObject.Find("End").GetComponent<EndDistance>();
    }

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        GetPlayerImages();
        StartCoroutine(ActivatePositions());
    }
 
    void Update()
    {
        getPlayers();
        PLayerRanks();
        availablePositions();
    }

    void GetPlayerImages()
    {
        GameObject[] pImages = GameObject.FindGameObjectsWithTag("Player Image");
        playerImages = new GameObject[pImages.Length];
        foreach(GameObject pi in pImages)
        {
            switch (pi.name)
            {
                case "Player 1 img":
                    playerImages[0] = pi;
                    break;
                case "Player 2 img":
                    playerImages[1] = pi;
                    break;
                case "Player 3 img":
                    playerImages[2] = pi;
                    break;
                case "Player 4 img":
                    playerImages[3] = pi;
                    break;
            }
        }
    }

    public void getPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void availablePositions()
    {
        positions = GameObject.FindGameObjectsWithTag("Position");
    }

    void PLayerRanks()
    {
        foreach (GameObject player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            int pos = controller.pos;
            int pIndex = controller.playerNumber - 1;

            switch (pos)
            {
                case 1:
                    if(finishLine.finished == 0)
                    {
                        playerImages[pIndex].transform.position = Vector3.MoveTowards(playerImages[pIndex].transform.position, positions[0].transform.position, lerpSpd);
                    }
                    break;
                case 2:
                    if (finishLine.finished < 2)
                    {
                        playerImages[pIndex].transform.position = Vector3.MoveTowards(playerImages[pIndex].transform.position, positions[1].transform.position, lerpSpd);
                    }
                    break;
                case 3:
                    if (finishLine.finished < 3 && positions[2].activeInHierarchy)
                    {
                        playerImages[pIndex].transform.position = Vector3.MoveTowards(playerImages[pIndex].transform.position, positions[2].transform.position, lerpSpd);
                    }
                    break;
                case 4:
                    if (finishLine.finished < 4 && positions[3].activeInHierarchy)
                    {
                        playerImages[pIndex].transform.position = Vector3.MoveTowards(playerImages[pIndex].transform.position, positions[3].transform.position, lerpSpd);
                    }
                    break;
            }
        }

        #region original
        /*
        #region 2 players
        //Player 1
        if (players.Length == 2)
        {
            

            switch (players[0].GetComponent<PlayerController>().pos)
            {
                case 1:
                    if (finishLine.finished == 0)
                    {
                        playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[0].transform.position, lerpSpd);
                    }
                    break;
                case 2:
                    if (finishLine.finished < 2)
                    {
                        playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[1].transform.position, lerpSpd);
                    }
                    break;
            }
            //Player 2
            switch (players[1].GetComponent<PlayerController>().pos)
            {
                case 1:
                    if (finishLine.finished == 0)
                    {
                        playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[0].transform.position, lerpSpd);
                    }
                    break;
                case 2:
                    if (finishLine.finished < 2)
                    {
                        playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[1].transform.position, lerpSpd);
                    }
                    break;
            }
        }
        #endregion

        #region 3 players
        if (players.Length == 3)
        {
            {   //Player 1
                switch (players[0].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[0].transform.position, lerpSpd);
                        }
                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[1].transform.position, lerpSpd);
                        }
                        break;
                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[2].transform.position, lerpSpd);
                        }
                        break;
                }
            }
            //Player 2
            {
                switch (players[1].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[0].transform.position, lerpSpd);
                        }

                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[1].transform.position, lerpSpd);
                        }

                        break;

                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[2].transform.position, lerpSpd);
                        }

                        break;
                }

                //Player 3
                switch (players[2].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[0].transform.position, lerpSpd);
                        }

                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[1].transform.position, lerpSpd);
                        }

                        break;
                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[2].transform.position, lerpSpd);
                        }
                        break;


                }

            }
        }
        #endregion

        #region 4 players
        else if (players.Length == 4)
        {
            //Player 1
            if (players[0] != null && players[0] == true)
            {
                switch (players[0].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[0].transform.position, lerpSpd);
                        }
                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[1].transform.position, lerpSpd);
                        }
                        break;
                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[2].transform.position, lerpSpd);
                        }
                        break;
                    case 4:
                        if (finishLine.finished < 4)
                            playerImages[0].transform.position = Vector3.MoveTowards(playerImages[0].transform.position, positions[3].transform.position, lerpSpd);
                        break;
                }
            }
            //Player 2
            if (players[1] != null && players[1] == true)
            {
                switch (players[1].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[0].transform.position, lerpSpd);
                        }
                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[1].transform.position, lerpSpd);
                        }
                        break;
                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[2].transform.position, lerpSpd);
                        }
                        break;
                    case 4:
                        if (finishLine.finished < 4)
                            playerImages[1].transform.position = Vector3.MoveTowards(playerImages[1].transform.position, positions[3].transform.position, lerpSpd);
                        break;

                }
            }

            //Player 3
            if (players[2] != null && players[2] == true)
            {
                switch (players[2].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[0].transform.position, lerpSpd);
                        }
                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[1].transform.position, lerpSpd);
                        }
                        break;
                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[2].transform.position, lerpSpd);
                        }
                        break;
                    case 4:
                        if (finishLine.finished < 4)
                            playerImages[2].transform.position = Vector3.MoveTowards(playerImages[2].transform.position, positions[3].transform.position, lerpSpd);
                        break;

                }
            }

            //Player 4
            if (players[3] != null && players[3] == true)
            {
                switch (players[3].GetComponent<PlayerController>().pos)
                {
                    case 1:
                        if (finishLine.finished == 0)
                        {
                            playerImages[3].transform.position = Vector3.MoveTowards(playerImages[3].transform.position, positions[0].transform.position, lerpSpd);
                        }
                        break;
                    case 2:
                        if (finishLine.finished < 2)
                        {
                            playerImages[3].transform.position = Vector3.MoveTowards(playerImages[3].transform.position, positions[1].transform.position, lerpSpd);
                        }
                        break;
                    case 3:
                        if (finishLine.finished < 3)
                        {
                            playerImages[3].transform.position = Vector3.MoveTowards(playerImages[3].transform.position, positions[2].transform.position, lerpSpd);
                        }
                        break;
                    case 4:
                        if (finishLine.finished < 4)
                            playerImages[3].transform.position = Vector3.MoveTowards(playerImages[3].transform.position, positions[3].transform.position, lerpSpd);
                        break;

                }
            }
        }
        #endregion
        */
        #endregion
    }

    IEnumerator ActivatePositions()
    {
        yield return new WaitForSeconds(0.1f);

        if(players.Length < 4)
        {
            for(int i = 0; i < 4; i++)
            {
                if(GameObject.Find("Player " + (i + 1)) == null)
                {
                    //positions[i].SetActive(false);
                    playerImages[i].SetActive(false);
                    Scores score = GameObject.Find("Scores").GetComponent<Scores>();
                    score.playerImages[i].SetActive(false);
                    score.playerScores[i].SetActive(false);
                }
            }

            if(players.Length == 3)
            {
                positions[3].SetActive(false);
            }
            else if(players.Length == 2)
            {
                positions[3].SetActive(false);
                positions[2].SetActive(false);
            }
        }

        /*
        switch (players.Length)
        {
            case 3:
                positions[3].SetActive(false);
                playerImages[3].SetActive(false);
                GameObject.Find("Scores").GetComponent<Scores>().playerImages[3].SetActive(false);
                GameObject.Find("Scores").GetComponent<Scores>().playerScores[3].SetActive(false);
                break;
            case 2:
                GameObject.Find("Scores").GetComponent<Scores>().playerImages[2].SetActive(false);
                GameObject.Find("Scores").GetComponent<Scores>().playerImages[3].SetActive(false);
                GameObject.Find("Scores").GetComponent<Scores>().playerScores[2].SetActive(false);
                GameObject.Find("Scores").GetComponent<Scores>().playerScores[3].SetActive(false);
                positions[2].SetActive(false);
                positions[3].SetActive(false);
                playerImages[2].SetActive(false);
                playerImages[3].SetActive(false);
                break;
        }
        */
    }
}
