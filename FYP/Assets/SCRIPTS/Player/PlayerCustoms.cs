using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustoms : MonoBehaviour
{
    public GameObject[] players;
    public Material[] materials;
    public Mesh[] meshes;
    float[] scales;

    /*
    public Material p1, p2, p3, p4;
    public Mesh m1, m2, m3, m4;
    public float s1, s2, s3, s4;
    */

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        materials = new Material[players.Length];
        meshes = new Mesh[players.Length];
        scales = new float[players.Length];
        GetCustoms();

        /*
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
        */
    }

    void GetCustoms()
    {
        for (int i = 0; i < players.Length; i++)
        {
            materials[i] = players[i].transform.GetChild(0).GetComponent<Renderer>().material;
            meshes[i] = players[i].transform.GetChild(0).GetComponent<MeshFilter>().mesh;
            scales[i] = players[i].transform.GetChild(0).transform.localScale.x;
        }
    }

    public Material SetMaterial(int player)
    {
        return materials[player - 1];
    }

    public Mesh SetMesh(int player)
    {
        return meshes[player - 1];
    }

    public Vector3 SetScale(int player)
    {
        float scale = scales[player - 1];
        Vector3 scaleV = new Vector3(scale, scale, scale);
        return scaleV;
    }
}