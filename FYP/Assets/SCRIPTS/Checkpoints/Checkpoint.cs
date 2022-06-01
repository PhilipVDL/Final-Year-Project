using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointManager CPM;
    FinishLine finish;
    SFXScript sfx;
    TeamObjectivesManager TOM;

    public int checkpointNumber;
    public bool checkpointActivated;

    private void Start()
    {
        CPM = GameObject.Find("Checkpoint Manager").GetComponent<CheckpointManager>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLine>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
        TOM = GameObject.Find("Team Objectives Manager").GetComponent<TeamObjectivesManager>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !checkpointActivated) //if a player enters a checkpoint for the first time
        {
            CPM.currentCheckpoint = this; //set this as the current checkpoint
            checkpointActivated = true; //activate this checkpoint
            //respawn dead players
            foreach(GameObject player in finish.startPlayers) //for each player in this game
            {
                PlayerController controller = player.GetComponent<PlayerController>();
                controller.currentSpawn = gameObject; //sets this as current spawn

                if (!player.activeInHierarchy) //if player is dead
                {
                    player.transform.position = controller.currentSpawn.transform.position; //move to current spawn
                    player.SetActive(true); //respawn
                    sfx.PlaySFX(sfx.Checkpoint); //play checkpoint sfx
                    if (!TOM.checkpointRespawned && TOM.FM) //give points for objective 'Formation'
                    {
                        TOM.checkpointRespawned = true;
                        TOM.ObjectivePoints();
                    }
                }
            }
        }
    }
}