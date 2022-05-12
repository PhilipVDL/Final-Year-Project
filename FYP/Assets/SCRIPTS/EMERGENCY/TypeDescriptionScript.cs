using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeDescriptionScript : MonoBehaviour
{
    PlayerSelectBallType type;
    PlayerJoinVisible pjv;
    public GameObject[] descriptions;
    int currentType;

    private void Start()
    {
        type = GetComponent<PlayerSelectBallType>();
        pjv = GetComponent<PlayerJoinVisible>();
    }

    private void Update()
    {
        currentType = type.ballTypeID;

        for(int i = 0;i < descriptions.Length; i++)
        {
            if(i == currentType && pjv.amJoined)
            {
                descriptions[i].SetActive(true);
            }
            else
            {
                descriptions[i].SetActive(false);
            }
        }
    }
}