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

    private float countdown = 3;
    private float countSpd = 1;

    public GameObject placementModetext;

    public float lerpSpd = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerImages = GameObject.FindGameObjectsWithTag("Player Image");



    }


    // Update is called once per frame
    void Update()
    {
        getPlayers();
        PLayerRanks();
        availablePositions();

        if (players.Length < 4)
        {
            activatePositions();
        }
    }

    void getPlayers()
    {
        countdown -= countSpd * Time.deltaTime;

        if (countdown <= 0)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            countSpd = 0;
        }
    }

    void availablePositions()
    {
        positions = GameObject.FindGameObjectsWithTag("Position");
    }

    void PLayerRanks()
    {
        //2 Players
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
        //THREE PLAYERS


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
    }

    void activatePositions()
    {
        switch (players.Length)
        {
            case 3:
                positions[3].SetActive(false);
                playerImages[3].SetActive(false);
                break;

            case 2:
                for (int i = 0; i < 2; i++)
                {
                    positions[i + 2].SetActive(false); //this is giving errors
                    playerImages[i + 2].SetActive(false); //this is similar so probably also errors
                }
                break;

        }
    }
}