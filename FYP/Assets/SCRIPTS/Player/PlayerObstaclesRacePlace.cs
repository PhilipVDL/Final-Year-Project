using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstaclesRacePlace : MonoBehaviour
{
    ObstacleInventory inventory;
    SFXScript sfx;
    ControllerRumble rumble;
    PlayerController controller;

    public GameObject previewPrefab;
    private GameObject obstaclesOnMap;
    public bool canPlaceMore;
    public int maxPlaceThisRound;
    public int placedThisRound;
    public int currentPreviewIndex;

    private void Start()
    {
        inventory = GetComponent<ObstacleInventory>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
        rumble = GetComponent<ControllerRumble>();
        controller = GetComponent<PlayerController>();
        obstaclesOnMap = GameObject.Find("Obstacles On Map");
    }

    private void Update()
    {
        MaxPlaced();
    }

    void MaxPlaced()
    {
        if (placedThisRound < maxPlaceThisRound)
        {
            canPlaceMore = true;
        }
        else if (placedThisRound >= maxPlaceThisRound)
        {
            canPlaceMore = false;
        }
    }

    void SetPreviewModel(GameObject preview)
    {
        if (inventory.obstacles.Count > 0)
        {
            currentPreviewIndex = inventory.obstacles[inventory.selectedIndex].GetComponent<ObstacleID>()._obstacleID;
            ObstaclePreview _obstaclePreview = preview.GetComponent<ObstaclePreview>();
            _obstaclePreview.ActivePreview(currentPreviewIndex);
        }
    }

    public void PlaceObstacle()
    {
        if (canPlaceMore && inventory.obstacles.Count > 0)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit); //raycast to ground
            GameObject preview = Instantiate(previewPrefab, hit.point, Quaternion.Euler(0, 0, 0)); //spawn preview
            SetPreviewModel(preview); //set preview model
            GameObject obstacle = Instantiate(inventory.obstacles[inventory.selectedIndex], preview.transform); //place
            obstacle.SetActive(false); //deactivate
            placedThisRound++; //count 1 placement
            obstacle.transform.parent = obstaclesOnMap.transform; //unparent
            inventory.obstacles.RemoveAt(inventory.selectedIndex); //remove from inventory
            Destroy(preview); //delete preview
            sfx.PlaySFX(sfx.placeObstacle);
            rumble.PlaceRumble(controller.playerNumber);
        }
    }
}