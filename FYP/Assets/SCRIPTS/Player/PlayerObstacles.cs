using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstacles : MonoBehaviour
{
    ObstacleInventory inventory;
    PlayerController controller;

    public GameObject obstaclePreview;
    private GameObject obstaclesOnMap;
    ObstaclePreview _obstaclePreviw;
    public bool DEBUG_MaxPlace;
    public int maxPlaceThisRound;
    public int placedThisRound;
    public bool preview;
    public int currentPreviewIndex;
    public float previewSpeed;

    private void Start()
    {
        inventory = GetComponent<ObstacleInventory>();
        controller = GetComponent<PlayerController>();
        _obstaclePreviw = obstaclePreview.GetComponent<ObstaclePreview>();
        obstaclesOnMap = GameObject.Find("Obstacles On Map");
    }

    private void Update()
    {
        MaxPlaced();
        PreviewMode();
        MovePreview();
    }

    void MaxPlaced()
    {
        if(placedThisRound >= maxPlaceThisRound && !DEBUG_MaxPlace)
        {
            preview = false;
        }
        else if(inventory.obstacles.Count == 0)
        {
            preview = false;
        }
    }

    void PreviewMode()
    {
        if (preview)
        {
            obstaclePreview.SetActive(true);
            SetPreviewModel();
        }
        else
        {
            obstaclePreview.SetActive(false);
        }
    }

    void SetPreviewModel()
    {
        if(inventory.obstacles.Count > 0)
        {
            currentPreviewIndex = inventory.obstacles[inventory.selectedIndex].GetComponent<ObstacleID>()._obstacleID;
            _obstaclePreviw.ActivePreview(currentPreviewIndex);
        }
    }

    void MovePreview()
    {
        int x = 0;
        int z = 0;
      /*  if (controller.placementMode)
        {
            //vertical
            if (controller.speeding)
            {
                z = 1;
            }
            else if (controller.braking)
            {
                z = -1;
            }

            //horizontal
            if (controller.goRight)
            {
                x = 1;
            }
            else if (controller.goLeft)
            {
                x = -1;
            }
        }
        */
        Vector3 move = new Vector3(x, 0, z);
        Vector3 moveSpeed = move.normalized * previewSpeed;
        obstaclePreview.transform.Translate(moveSpeed * Time.deltaTime);
    }

    public void PlaceObstacle()
    {
        GameObject obstacle = Instantiate(inventory.obstacles[inventory.selectedIndex], obstaclePreview.transform); //place
        placedThisRound++; //count 1 placement
        obstacle.transform.parent = obstaclesOnMap.transform; //unparent
        inventory.obstacles.RemoveAt(inventory.selectedIndex); //remove from inventory 
    }
}