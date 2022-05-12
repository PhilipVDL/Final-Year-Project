using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public float countdown = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        //Restart();
    }

    void Restart()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void CountDown()
    {
        countdown -= Time.deltaTime;

    }
}
