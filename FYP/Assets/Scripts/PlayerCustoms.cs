using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustoms : MonoBehaviour
{
    //public Material[] materials;
    public Material p1, p2, p3, p4;
    public Mesh m1, m2, m3, m4;
    public float s1, s2, s3, s4;

    private void Start()
    {
        //get materials
        p1 = GameObject.Find("Player 1").transform.GetChild(0).GetComponent<Renderer>().material;
        p2 = GameObject.Find("Player 2").transform.GetChild(0).GetComponent<Renderer>().material;
        p3 = GameObject.Find("Player 3").transform.GetChild(0).GetComponent<Renderer>().material;
        p4 = GameObject.Find("Player 4").transform.GetChild(0).GetComponent<Renderer>().material;
        //get meshes
        m1 = GameObject.Find("Player 1").transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        m2 = GameObject.Find("Player 2").transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        m3 = GameObject.Find("Player 3").transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        m4 = GameObject.Find("Player 4").transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        //get scales
        s1 = GameObject.Find("Player 1").transform.GetChild(0).transform.localScale.x;
        s2 = GameObject.Find("Player 2").transform.GetChild(0).transform.localScale.x;
        s3 = GameObject.Find("Player 3").transform.GetChild(0).transform.localScale.x;
        s4 = GameObject.Find("Player 4").transform.GetChild(0).transform.localScale.x;

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

    public Mesh SetMesh(int player)
    {
        Mesh mesh = null;
        switch (player)
        {
            case 1:
                mesh = m1;
                break;
            case 2:
                mesh = m2;
                break;
            case 3:
                mesh = m3;
                break;
            case 4:
                mesh = m4;
                break;
        }
        return mesh;
    }

    public Vector3 SetScale(int player)
    {
        float scale = 0;
        switch (player)
        {
            case 1:
                scale = s1;
                break;
            case 2:
                scale = s2;
                break;
            case 3:
                scale = s3;
                break;
            case 4:
                scale = s4;
                break;
        }
        Vector3 scaleV = new Vector3(scale, scale, scale);
        return scaleV;
    }
}