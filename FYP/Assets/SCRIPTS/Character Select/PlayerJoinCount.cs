using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinCount : MonoBehaviour
{
    public PlayerCustoms customs;
    [Header("Joined")]
    public bool playerJoined1;
    public bool playerJoined2;
    public bool playerJoined3;
    public bool playerJoined4;

    [Header("Ready")]
    public bool playerReady1;
    public bool playerReady2;
    public bool playerReady3;
    public bool playerReady4;

    [Header("Counts")]
    public int joinCount;
    public int readyCount;

    [Header("Debug")]
    public bool DEBUG_Player1Only;
    public bool firstScene;
    public bool readyToLoad;

    public Text[] joinedandReady;
   

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded; //allows detecting when scene unloads
        SceneManager.sceneLoaded += OnSceneLoaded; //allows detecting when scene loads
    }

    void OnSceneUnloaded(Scene scene)
    {
        firstScene = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //run coroutine when scene loaded, if not first scene
        if (!firstScene)
        {
            StartCoroutine(JoinedPlayers());
        }
    }

    void SetText()
    {
        joinedandReady[0].text = joinCount.ToString();
        joinedandReady[1].text = readyCount.ToString();
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

        customs = GameObject.Find("PlayerCustoms").GetComponent<PlayerCustoms>();
        customs.SetPlayerCustoms();
    }



    private void Update()
    {
        PlayerJoin();
        ReadyCount();
        ReadyLoad();
        SetText();

        if(joinCount > 1 && readyCount == joinCount)
        {
            SceneManager.LoadScene(2);
        }
    }

    void PlayerJoin()
    {
        //join, else ready

        //Player 1
        if (!playerJoined1 && Input.GetButtonDown("Jump1"))
        {
            playerJoined1 = true;
            joinCount++;
           
        }
        else if(playerJoined1 && Input.GetButtonDown("Jump1"))
        {
            playerReady1 = true;
            readyCount ++;
        }

        //Player 2
        if (!playerJoined2 && Input.GetButtonDown("Jump2"))
        {
            playerJoined2 = true;
            joinCount++;
            
        }
        else if(playerJoined2 && Input.GetButtonDown("Jump2"))
        {
            playerReady2 = true;
        }

        //Player 3
        if (!playerJoined3 && Input.GetButtonDown("Jump3"))
        {
            playerJoined3 = true;
            joinCount++;
        }
        else if(playerJoined3 && Input.GetButtonDown("Jump3"))
        {
            playerReady3 = true;
        }

        //Player 4
        if (!playerJoined4 && Input.GetButtonDown("Jump4"))
        {
            playerJoined4 = true;
            joinCount++;
        }
        else if(playerJoined4 && Input.GetButtonDown("Jump4"))
        {
            playerReady4 = true;
        }
    }

    void ReadyCount()
    {
        bool[] ready = new bool[] { playerReady1, playerReady2, playerReady3, playerReady4 };
        readyCount = 0;
        foreach(bool r in ready)
        {
            if (r)
            {
                readyCount++;
            }
        }
    }

    void ReadyLoad()
    {
        if (DEBUG_Player1Only)
        {
            readyToLoad = true;
        }
        else if(joinCount >= 2 && readyCount == joinCount)
        {
            readyToLoad = true;
        }
        else
        {
            readyToLoad = false;
        }
    }
}