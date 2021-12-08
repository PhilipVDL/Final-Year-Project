using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridZone : MonoBehaviour
{
    PlayerCustoms customs;
    public int gridX, gridZ;
    public bool filled;
    public GameObject obstacle;
    public GameObject highlight;
    Renderer hlRenderer;
    public Material hlMaterial;

    private void Start()
    {
        customs = GameObject.Find("Player Customs").GetComponent<PlayerCustoms>();
        highlight = transform.GetChild(0).gameObject;
        hlRenderer = highlight.GetComponent<Renderer>();
        hlMaterial = hlRenderer.material;
    }

    private void Update()
    {
        ResetHighlight();
    }

    public void Fill(GameObject fillObstacle)
    {
        filled = true;
        obstacle = fillObstacle;
    }

    void ResetHighlight()
    {
        highlight.SetActive(false);
    }

    public void Highlight(int playerNumber)
    {
        highlight.SetActive(true);
        hlMaterial = customs.materials[playerNumber - 1];
        hlRenderer.material = hlMaterial;
    }
}