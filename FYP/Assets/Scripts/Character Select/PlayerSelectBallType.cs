using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectBallType : MonoBehaviour
{
    PlayerJoinCount pjc;
    PlayerJoinVisible pjv;
    MeshFilter meshFilter;
    MeshRenderer rend;

    public bool characterSelect;
    private int playerNumber;

    #region variables
    [Header("Basic")]
    public int currentEditIndex;
    public int minIndex, maxIndex;
    /*
     * 0 = ball type
     * 1 = skin
     * 2 = hat
     * 3 = glasses
     * 4 = moustache
     */
    public int currentEditValue;
    public int[] lastValue;
    [Header("Type")]
    public int ballTypeID;
    public int minID, maxID;
    [Header("Skin")]
    public Mesh[] meshes;
    public Material[] materials;
    public Mesh[] meshesBowling;
    public Material[] materialsBowling;
    public Mesh[] meshesBouncy;
    public Material[] materialsBouncy;
    public Mesh[] meshesSnooker;
    public Material[] materialsSnooker;
    private Mesh[][] meshesAll;
    private Material[][] materialsAll;
    [Header("Hat")]
    public GameObject[] hats;
    public GameObject currentHat;
    public GameObject currentHatPrefab;
    [Header("Glasses")]
    public GameObject[] glasses;
    public GameObject currentGlasses;
    public GameObject currentGlassesPrefab;
    [Header("Moustache")]
    public GameObject[] moustaches;
    public GameObject currentMoustache;
    public GameObject currentMoustachePrefab;
    #endregion

    private void Start()
    {
        if (characterSelect)
        {
            pjc = GameObject.Find("PlayerJoinCount").GetComponent<PlayerJoinCount>();
            pjv = GetComponent<PlayerJoinVisible>();
            meshFilter = GetComponent<MeshFilter>();
            rend = GetComponent<MeshRenderer>();
            playerNumber = pjv.playerNumber;
            currentEditIndex = 0;

            ArrayAll();
        }
    }

    void ArrayAll()
    {
        //array of mesh arrays
        meshesAll = new Mesh[3][];
        meshesAll[0] = meshesBowling;
        meshesAll[1] = meshesBouncy;
        meshesAll[2] = meshesSnooker;

        //array of material arrays
        materialsAll = new Material[3][];
        materialsAll[0] = materialsBowling;
        materialsAll[1] = materialsBouncy;
        materialsAll[2] = materialsSnooker;
    }

    private void Update()
    {
        if (characterSelect)
        {
            EditValue();
        }
    }

    void EditValue()
    {
        //change target
        if(Input.GetButtonDown("EditIndex" + playerNumber))
        {
            if (Input.GetAxis("EditIndex" + playerNumber) > 0)
            {
                currentEditIndex++;
                IndexLoopBounds();
                currentEditValue = lastValue[currentEditIndex];
            }
            else if (Input.GetAxis("EditIndex" + playerNumber) < 0)
            {
                currentEditIndex--;
                IndexLoopBounds();
                currentEditValue = lastValue[currentEditIndex];
            }
        }

        //change value of target
        if (Input.GetButtonDown("ObstacleSwitch" + playerNumber))
        {
            if (Input.GetAxis("ObstacleSwitch" + playerNumber) > 0)
            {
                currentEditValue++;
                UnReady(playerNumber);
            }
            else if (Input.GetAxis("ObstacleSwitch" + playerNumber) < 0)
            {
                currentEditValue--;
                UnReady(playerNumber);
            }
        }

        //send edit
        switch (currentEditIndex)
        {
            case 0:
                SwitchBallType();
                break;
            case 1:
                SwitchSkin(currentEditValue);
                break;
            case 2:
                SwitchHat();
                break;
            case 3:
                SwitchGlasses();
                break;
            case 4:
                SwitchMoustache();
                break;
        }
    }

    void IndexLoopBounds()
    {
        //loop bounds
        if (currentEditIndex > maxIndex)
        {
            currentEditIndex = minIndex;
        }
        else if (currentEditIndex < minIndex)
        {
            currentEditIndex = maxIndex;
        }
    }

    void SwitchBallType()
    {
        //loop back if outside bounds
        if(currentEditValue > maxID)
        {
            currentEditValue = minID;
        }
        else if(currentEditValue < minID)
        {
            currentEditValue = maxID;
        }

        //set value
        ballTypeID = currentEditValue;
        lastValue[currentEditIndex] = currentEditValue;
        lastValue[1] = 0;
        SwitchSkin(0);
    }

    void SwitchSkin(int value)
    {
        if (value >= materialsAll[ballTypeID].Length)
        {
            currentEditValue = 0;
            value = 0;
        }
        else if (value < 0)
        {
            currentEditValue = materialsAll[ballTypeID].Length - 1;
            value = materialsAll[ballTypeID].Length - 1;
        }

        lastValue[currentEditIndex] = currentEditValue;
        meshFilter.mesh = meshesAll[ballTypeID][0]; //only one mesh per type
        rend.material = materialsAll[ballTypeID][value];
    }

    void SwitchHat()
    {
        if(currentEditValue >= hats.Length)
        {
            currentEditValue = 0;
        }
        else if(currentEditValue < 0)
        {
            currentEditValue = hats.Length - 1;
        }

        lastValue[currentEditIndex] = currentEditValue;
        GameObject desiredHat = hats[currentEditValue];

        if(currentHat != null)
        {
            if(desiredHat == null)
            {
                Destroy(currentHat); //destroy hat if null desired
            }
            else if(currentHat.name != desiredHat.name)
            {
                Destroy(currentHat); //destroy hat if not desired type
            }   
        }

        if (currentHat == null && desiredHat != null)
        {
            currentHat = Instantiate(desiredHat, transform.parent); //replace with correct hat
            currentHatPrefab = desiredHat;
            currentHat.name = desiredHat.name;
            currentHat.transform.parent = transform;
        }
    }

    void SwitchGlasses()
    {
        if (currentEditValue >= glasses.Length)
        {
            currentEditValue = 0;
        }
        else if (currentEditValue < 0)
        {
            currentEditValue = glasses.Length - 1;
        }

        lastValue[currentEditIndex] = currentEditValue;
        GameObject desiredGlasses = glasses[currentEditValue];

        if (currentGlasses != null)
        {
            if (desiredGlasses == null)
            {
                Destroy(currentGlasses); //destroy if null desired
            }
            else if (currentGlasses.name != desiredGlasses.name)
            {
                Destroy(currentGlasses); //destroy if not desired type
            }
        }

        if (currentGlasses == null && desiredGlasses != null)
        {
            currentGlasses = Instantiate(desiredGlasses, transform.parent); //replace with correct type
            currentGlassesPrefab = desiredGlasses;
            currentGlasses.name = desiredGlasses.name;
            currentGlasses.transform.parent = transform;
        }
    }

    void SwitchMoustache()
    {
        if (currentEditValue >= moustaches.Length)
        {
            currentEditValue = 0;
        }
        else if (currentEditValue < 0)
        {
            currentEditValue = moustaches.Length - 1;
        }

        lastValue[currentEditIndex] = currentEditValue;
        GameObject desiredMoustache = moustaches[currentEditValue];

        if (currentMoustache != null)
        {
            if (desiredMoustache == null)
            {
                Destroy(currentMoustache); //destroy if null desired
            }
            else if (currentMoustache.name != desiredMoustache.name)
            {
                Destroy(currentMoustache); //destroy if not desired type
            }
        }

        if (currentMoustache == null && desiredMoustache != null)
        {
            currentMoustache = Instantiate(desiredMoustache, transform.parent); //replace with correct type
            currentMoustachePrefab = desiredMoustache;
            currentMoustache.name = desiredMoustache.name;
            currentMoustache.transform.parent = transform;
        }
    }

    void UnReady(int pNum)
    {
        switch (pNum)
        {
            case 1:
                pjc.playerReady1 = false;
                break;
            case 2:
                pjc.playerReady2 = false;
                break;
            case 3:
                pjc.playerReady3 = false;
                break;
            case 4:
                pjc.playerReady4 = false;
                break;
        }
    }
}