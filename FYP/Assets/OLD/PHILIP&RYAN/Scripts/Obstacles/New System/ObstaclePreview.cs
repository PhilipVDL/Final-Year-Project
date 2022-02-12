using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePreview : MonoBehaviour
{
    public GameObject[] previewModels;

    public void ActivePreview(int index)
    {
        for(int i = 0; i < previewModels.Length; i++)
        {
            if(i == index)
            {
                previewModels[i].SetActive(true);
            }
            else
            {
                previewModels[i].SetActive(false);
            }
        }
    }
}