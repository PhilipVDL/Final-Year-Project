using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public GameObject SFXPrefab;
    public bool test;

    [Header("SFX")]
    public AudioClip obstaclePlaced;
    public AudioClip offscreenEliminated;
    public AudioClip pickupObstacle;
    public AudioClip playerCollide;

    public void PlaySFX(AudioClip clip)
    {
        GameObject oneShot = Instantiate(SFXPrefab); //spawn SFX source
        AudioSource source = oneShot.GetComponent<AudioSource>(); //get AudioSource
        source.PlayOneShot(clip); //play clip
        oneShot.GetComponent<OneShot>().DestroyWhenDone(); //destroy SFX source when finished playing clip
    }

    private void Update()
    {
        if (test)
        {
            PlaySFX(obstaclePlaced);
            test = false;
        }
    }
}