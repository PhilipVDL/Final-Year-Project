using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJoinCount : MonoBehaviour
{
    public bool playerJoined1, playerJoined2, playerJoined3, playerJoined4;
    public int joinCount;
    public bool DEBUG_Player1Only;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; //allows detecting when scene loads
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //run coroutine when scene loaded
        StartCoroutine(JoinedPlayers());
    }

    IEnumerator JoinedPlayers()
    {
        yield return new WaitForEndOfFrame();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (DEBUG_Player1Only)
        {
            //if debug mode, delete all but Player 1
            foreach (GameObject player in players)
            {
                if (player.name != "Player 1")
                {
                    Destroy(player.gameObject);
                }
            }
        }
        else if(joinCount >= 2)
        {
            //if at least 2 players joined, delete not joined
            if (!playerJoined1)
            {
                foreach (GameObject player in players)
                {
                    if (player.name == "Player 1")
                    {
                        Destroy(player.gameObject);
                    }
                }
            }

            if (!playerJoined2)
            {
                foreach (GameObject player in players)
                {
                    if (player.name == "Player 2")
                    {
                        Destroy(player.gameObject);
                    }
                }
            }

            if (!playerJoined3)
            {
                foreach (GameObject player in players)
                {
                    if (player.name == "Player 3")
                    {
                        Destroy(player.gameObject);
                    }
                }
            }

            if (!playerJoined4)
            {
                foreach (GameObject player in players)
                {
                    if (player.name == "Player 4")
                    {
                        Destroy(player.gameObject);
                    }
                }
            }
        }
        else
        {
            //else delete Players 3 and 4 (min 2 players)
            foreach (GameObject player in players)
            {
                if (player.name == "Player 3" || player.name == "Player 4")
                {
                    Destroy(player.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        PlayerJoin();
    }

    void PlayerJoin()
    {
        if (!playerJoined1 && Input.GetButtonDown("Jump1"))
        {
            playerJoined1 = true;
            joinCount++;
        }

        if (!playerJoined2 && Input.GetButtonDown("Jump2"))
        {
            playerJoined2 = true;
            joinCount++;
        }

        if (!playerJoined3 && Input.GetButtonDown("Jump3"))
        {
            playerJoined3 = true;
            joinCount++;
        }

        if (!playerJoined4 && Input.GetButtonDown("Jump4"))
        {
            playerJoined4 = true;
            joinCount++;
        }
    }
}