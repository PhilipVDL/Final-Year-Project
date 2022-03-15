using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTest : MonoBehaviour
{
    PlayerJoinCount pjc;
    public bool loadScene;
    public string sceneName;

    private void Start()
    {
        pjc = GameObject.Find("PlayerJoinCount").GetComponent<PlayerJoinCount>();
    }

    private void Update()
    {
        if(loadScene && pjc.DEBUG_Player1Only)
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (loadScene && pjc.joinCount >= 2)
        {
            SceneManager.LoadScene(sceneName);
        }
        else if(loadScene && pjc.joinCount < 2)
        {
            loadScene = false;
            Debug.Log("Not Enough Players to Play");
        }
    }
}