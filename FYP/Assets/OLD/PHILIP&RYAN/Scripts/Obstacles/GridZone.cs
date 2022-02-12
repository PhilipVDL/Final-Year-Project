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
    private bool lightUp;

    private void Start()
    {
        customs = GameObject.Find("Player Customs").GetComponent<PlayerCustoms>();
        highlight = transform.GetChild(0).gameObject;
        hlRenderer = highlight.GetComponent<Renderer>();
        hlMaterial = hlRenderer.material;
        lightUp = false;
    }

    private void Update()
    {
        ResetHighlight();
    }

    private void LateUpdate()
    {
        if (lightUp)
        {
            Lit();
            lightUp = false;
        }
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
        hlMaterial = customs.materials[playerNumber - 1];
        lightUp = true;
    }

    void Lit()
    {
        highlight.SetActive(true);
        hlRenderer.material = hlMaterial;
    }
}