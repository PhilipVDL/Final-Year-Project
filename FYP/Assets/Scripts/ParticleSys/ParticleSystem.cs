using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    public float duration;
    public int maxDuration = 1;
    // Start is called before the first frame update
    void Start()
    {
        duration = maxDuration;
    }

    // Update is called once per frame
    void Update()
    {
        Duration();
    }

    void Duration()
    {
        duration -= Time.deltaTime;

        if(duration <= 0)
        {
            duration = maxDuration;
            Destroy(this.gameObject);
        }
    }
}
