using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}