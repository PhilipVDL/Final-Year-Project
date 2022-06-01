using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinCount : MonoBehaviour
{
    public static PlayerJoinCount Instance;

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
    //public bool sky;
    //public bool foosball;

    [Header("Debug")]
    public bool DEBUG_Player1Only;
    public bool firstScene;
    public bool readyToLoad;

    public Text joined, ready;
   

    private void Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        transform.parent = null;
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
        if (scene.name == "Character_Select_Sky")
        {
            firstScene = true;
        }
        else if (!firstScene && this != null) //run coroutine when scene loaded, if not first scene
        {
            StartCoroutine(JoinedPlayers());
        }

        if(GameObject.Find("JoinedCount") != null && GameObject.Find("ReadyCount") != null)
        {
            joined = GameObject.Find("JoinedCount").GetComponent<Text>();
            ready = GameObject.Find("ReadyCount").GetComponent<Text>();
        }
    }

    void SetText()
    {
        if (firstScene)
        {
            if(joined != null && ready != null)
            {
                joined.text = joinCount.ToString();
                ready.text = readyCount.ToString();
            }
        }
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

        customs = GameObject.Find("PlayerCustoms").GetComponent<PlayerCustoms>();
        customs.SetPlayerCustoms();

        //bools
        playerJoined1 = false;
        playerJoined2 = false;
        playerJoined3 = false;
        playerJoined4 = false;
        playerReady1 = false;
        playerReady2 = false;
        playerReady3 = false;
        playerReady4 = false;
        joinCount = 0;
        readyCount = 0;
    }



    private void Update()
    {
        PlayerJoin();
        ReadyCount();
        ReadyLoad();
        SetText();

        if(readyToLoad && firstScene)
        {
            GameObject.Find("LevelLoader").GetComponent<LoadSceneScript>().LoadCurrentScene();
        }

        /*
        if(readyToLoad && firstScene && !sky && !foosball)
        {
            SceneManager.LoadScene(4);
        }
        else if(readyToLoad && firstScene && sky)
        {
            SceneManager.LoadScene(5);
        }
        else if (readyToLoad && firstScene && foosball)
        {
            SceneManager.LoadScene(6);
        }
        */
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