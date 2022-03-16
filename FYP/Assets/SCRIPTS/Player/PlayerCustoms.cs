using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCustoms : MonoBehaviour
{
    PlayerJoinCount pjc;

    public bool characterSelect;

    public bool[] currentPlayers;
    public List<GameObject> players;
    public Mesh[] playerMeshes;
    public Material[] playerMaterials;
    public int[] playerTypes;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded; //allows detecting when scene unloads
    }

    void OnSceneUnloaded(Scene scene)
    {
        characterSelect = false;
    }

    private void Start()
    {
        pjc = GameObject.Find("PlayerJoinCount").GetComponent<PlayerJoinCount>();
    }

    private void Update()
    {
        GetPlayerCustoms();
    }

    void GetPlayerCustoms()
    {
        if (characterSelect) //if in character select
        {
            players.Clear(); //clear list, so it doesn't keep growing
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                players.Add(player); //add each player to the list
            }
            players.Sort((p1, p2) => p1.name.CompareTo(p2.name)); //sort list by name of parent
            currentPlayers = new bool[] { pjc.playerJoined1, pjc.playerJoined2, pjc.playerJoined3, pjc.playerJoined4 }; //which players have joined
            playerMeshes = new Mesh[players.Count];
            playerMaterials = new Material[players.Count];
            playerTypes = new int[players.Count];

            for (int i = 0; i < players.Count; i++)
            {
                if (currentPlayers[i])
                {
                    GameObject skin = players[i].transform.GetChild(0).gameObject;
                    playerMeshes[i] = skin.GetComponent<MeshFilter>().mesh;
                    playerMaterials[i] = skin.GetComponent<Renderer>().material;
                    playerTypes[i] = skin.GetComponent<PlayerSelectBallType>().ballTypeID;
                }
                else
                {
                    playerMeshes[i] = null;
                    playerMaterials[i] = null;
                    playerTypes[i] = -1;
                }
            }
        }
    }

    public void SetPlayerCustoms()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            int pNum = player.GetComponent<PlayerController>().playerNumber;
            GameObject skin = player.transform.GetChild(0).gameObject;

            skin.GetComponent<MeshFilter>().mesh = playerMeshes[pNum - 1];
            skin.GetComponent<Renderer>().material = playerMaterials[pNum - 1];
            PlayerAttributes attributes = player.GetComponent<PlayerAttributes>();
            attributes.ballTypeID = playerTypes[pNum - 1];
            attributes.set = true;
        }
    }
}