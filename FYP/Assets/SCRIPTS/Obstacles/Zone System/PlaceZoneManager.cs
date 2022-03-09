using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceZoneManager : MonoBehaviour
{
    public GameObject[] _zones;
    public GameObject currentZone;
    public int activeZone;
    public bool zonesActive;

    private void Start()
    {
        GetZones();
        activeZone = 0;
    }

    void GetZones()
    {
        _zones = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            _zones[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        ActiveZone();
    }

    void ActiveZone()
    {
        if (!zonesActive)
        {
            foreach (GameObject zone in _zones)
            {
                zone.SetActive(false);
            }
        }
        else
        {
            if (activeZone >= 0 && activeZone < _zones.Length) //if valid value
            {
                for (int i = 0; i < _zones.Length; i++)
                {
                    if (i == activeZone)
                    {
                        _zones[i].SetActive(true);
                        currentZone = _zones[i];
                    }
                    else
                    {
                        _zones[i].SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogError("activeZone invalid value");
            }
        }
    }
}