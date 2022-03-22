using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShot : MonoBehaviour
{
    private AudioSource source;

    public void DestroyWhenDone()
    {
        StartCoroutine(WaitTillEndOfClip());
    }

    IEnumerator WaitTillEndOfClip()
    {
        source = GetComponent<AudioSource>();

        while (source.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}