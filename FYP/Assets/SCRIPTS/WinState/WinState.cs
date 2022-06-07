using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    public static WinState Instance;

    FinishLine finish;
    SFXScript sfx;
    TeamObjectivesManager TOM;
    public ObstaclesOnMap obstaclesOnMap;
    public GameObject playerPrefab;
    public GameObject[] players;
    public GameObject[] spawns;
    public GameObject maincamera;
    CameraController camController;

    //score
    public int[] scores = new int[4];
    public int targetScore;
    public bool win;
    private bool won = false;
    public int winnerNumber;

    //rounds
    public int currentRound;
    public bool endRound;

    private void Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }

        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; //detects scene loading
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //runs at the start of each scene
    {
        if (Instance == this) //if this is the singleton
        {
            Debug.Log("WinState: Scene Loaded");
            if (GameObject.FindGameObjectWithTag("Player") != null) //if there is a player
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() != null) //with a playercontroller
                {
                    Debug.Log("WinState: Start Code");
                    spawns = GameObject.FindGameObjectsWithTag("Start Spawn");
                    finish = GameObject.Find("Finish").GetComponent<FinishLine>();
                    sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
                    TOM = GameObject.Find("Team Objectives Manager").GetComponent<TeamObjectivesManager>();
                    obstaclesOnMap = GameObject.Find("Obstacles On Map").GetComponent<ObstaclesOnMap>();
                    maincamera = GameObject.Find("Main Camera");
                    camController = maincamera.GetComponent<CameraController>();
                    GetPlayers();
                }
            }
        }
    }

    private void Update()
    {
        EndRound();
    }

    float CamCountDownTracker()
    {
        float countdown = camController.camCountdown;
        return countdown;
    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void EndRound()
    {
        if (endRound && !win)
        {
            NewRound();
        }
        else if (win && !won)
        {
            won = true;
            Debug.Log("Player " + winnerNumber + " Wins!"); //win
            Camera.main.gameObject.GetComponent<AudioSource>().Stop();
            sfx.PlaySFX(sfx.levelComplete);
            StartCoroutine(MainMenu());
        }
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("END_SCREEN_UI");
    }

    public void Score(int player, int score)
    {
        //add points
        scores[player - 1] += score;

        //check for winner
        foreach (int points in scores)
        {
            if (points >= targetScore)
            {
                win = true;
                winnerNumber = player;
            }
        }
    }

    public void NewRound()
    {
        currentRound++;
        finish.finished = 0;
        obstaclesOnMap.ActivateObstacles();
        finish.CheckObjectives();
        TOM.RandomObjectives();
        GetPlayers();
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerObstaclesRacePlace>().placedThisRound = 0;
        }
        endRound = false;
    }
}