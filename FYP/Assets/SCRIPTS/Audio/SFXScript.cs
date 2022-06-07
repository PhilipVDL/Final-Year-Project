using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public GameObject SFXPrefab;
    public float sfxVolume;
    float jumpVolumeScale = 0.6f;

    [Header("SFX")]
    public AudioClip offscreenEliminated;
    public AudioClip pickupObstacle;
    public AudioClip playerCollide;
    public AudioClip Checkpoint;
    public AudioClip Jump;
    public AudioClip obstacleCollide;
    public AudioClip timeRunningOut;
    public AudioClip _321Go;
    public AudioClip levelComplete;
    public AudioClip placeObstacle;

    public void PlaySFX(AudioClip clip)
    {
        GameObject oneShot = Instantiate(SFXPrefab); //spawn SFX source
        AudioSource source = oneShot.GetComponent<AudioSource>(); //get AudioSource
        source.volume = sfxVolume;
        if(clip == Jump)
        {
            source.PlayOneShot(clip, jumpVolumeScale); //play jump clip, reduced volume
        }
        else
        {
            source.PlayOneShot(clip); //play clip
        }
        oneShot.GetComponent<OneShot>().DestroyWhenDone(); //destroy SFX source when finished playing clip
    }
}