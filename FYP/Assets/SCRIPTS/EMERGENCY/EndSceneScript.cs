using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    public WinState win;
    public PlayerCustoms customs;

    private void Awake()
    {
        Destroy(GameObject.Find("PlayerJoinCount"));
    }

    private void Start()
    {
        win = GameObject.Find("WinState").GetComponent<WinState>();
        customs = GameObject.Find("PlayerCustoms").GetComponent<PlayerCustoms>();

        customs.SetEndCustoms(win.winnerNumber - 1);
        //StartCoroutine(SetEnd());
    }

    IEnumerator SetEnd()
    {
        yield return new WaitForSeconds(0.1f);
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump1"))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetButtonDown("Jump2"))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetButtonDown("Jump3"))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetButtonDown("Jump4"))
        {
            SceneManager.LoadScene(0);
        }
    }
}