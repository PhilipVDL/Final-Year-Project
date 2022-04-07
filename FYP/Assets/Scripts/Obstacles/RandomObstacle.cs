using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject previewPrefab;
    private GameObject obstaclesOnMap;

    private void Start()
    {
        obstaclesOnMap = GameObject.Find("Obstacles On Map");
        StartCoroutine(SpawnRandomObstacle());
    }

    IEnumerator SpawnRandomObstacle()
    {
        yield return new WaitForSeconds(1f);
        int random = Random.Range(0, 5);
        PlaceObstacle(random);
    }

    void SetPreviewModel(GameObject preview, int index)
    {
        int currentPreviewIndex = obstacles[index].GetComponent<ObstacleID>()._obstacleID;
        ObstaclePreview _obstaclePreview = preview.GetComponent<ObstaclePreview>();
        _obstaclePreview.ActivePreview(currentPreviewIndex);
    }

    void PlaceObstacle(int index)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit); //raycast to ground
        GameObject preview = Instantiate(previewPrefab, hit.point, Quaternion.Euler(0, 0, 0)); //spawn preview
        SetPreviewModel(preview, index); //set preview model
        GameObject obstacle = Instantiate(obstacles[index], preview.transform); //place
        obstacle.SetActive(true); //activate
        obstacle.transform.parent = obstaclesOnMap.transform; //unparent
        Destroy(preview); //delete preview
        Destroy(gameObject); //delete this
    }
}