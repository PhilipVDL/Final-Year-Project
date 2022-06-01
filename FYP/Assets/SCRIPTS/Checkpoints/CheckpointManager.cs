using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] checkpoints;
    public Checkpoint currentCheckpoint;

    private void Start()
    {
        SetupCheckpoints();
    }

    void SetupCheckpoints()
    {
        //get checkpoints
        GameObject[] checkpointGO = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpoints = new Checkpoint[checkpointGO.Length];
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i] = checkpointGO[i].GetComponent<Checkpoint>();
        }
        //set current checkpoint to first checkpoint
        foreach (Checkpoint cp in checkpoints)
        {
            if (cp.checkpointNumber == 1)
            {
                //currentCheckpoint = cp;
            }
        }
    }
}