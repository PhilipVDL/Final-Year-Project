using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintInputName : MonoBehaviour
{
    public bool logInput;

    private void Update()
    {
        if (logInput)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    Debug.Log("KeyCode down: " + kcode);
                }
            }
        }
    }
}