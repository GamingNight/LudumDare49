using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodRool : MonoBehaviour
{

    private AudioSource audioSource;
    private float initSmallImpactPitch;
    private float initSmallImpactVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initSmallImpactPitch = audioSource.pitch;
        initSmallImpactVolume = audioSource.volume;
    }

    private void OnCollisionEnter(Collision collision)
    {
            audioSource.pitch = initSmallImpactPitch + (Random.Range(-1f, 1f));
            audioSource.volume = initSmallImpactVolume + (0.3f * Random.Range(-1f, 1f));
            audioSource.Play();
    }

}
