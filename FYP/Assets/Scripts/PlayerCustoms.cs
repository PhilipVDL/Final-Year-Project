using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustoms : MonoBehaviour
{
    //public Material[] materials;
    public Material p1, p2, p3, p4;

    private void Start()
    {
        //get materials
        p1 = GameObject.Find("Player 1").transform.GetChild(0).GetComponent<Renderer>().material;
        p2 = GameObject.Find("Player 2").transform.GetChild(0).GetComponent<Renderer>().material;
        p3 = GameObject.Find("Player 3").transform.GetChild(0).GetComponent<Renderer>().material;
        p4 = GameObject.Find("Player 4").transform.GetChild(0).GetComponent<Renderer>().material;
    }

    public Material SetMaterial(int player)
    {
        Material mat = null;
        switch (player)
        {
            case 1:
                mat = p1;
                break;
            case 2:
                mat = p2;
                break;
            case 3:
                mat = p3;
                break;
            case 4:
                mat = p4;
                break;
        }
        return mat;
    }
}