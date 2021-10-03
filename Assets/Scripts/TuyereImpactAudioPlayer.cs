using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyereImpactAudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;
    private bool played;

    public AudioSource smallImpactSource;
    private float initSmallImpactPitch;
    private float initSmallImpactVolume;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        played = false;
        initSmallImpactPitch = smallImpactSource.pitch;
        initSmallImpactVolume = smallImpactSource.volume;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!played && collision.gameObject.tag == "Ground") {
            audioSource.Play();
            played = true;
        }
        if(collision.gameObject.tag == "Object") {
            smallImpactSource.pitch = initSmallImpactPitch + (0.1f * Random.Range(-1f, 1f));
            smallImpactSource.volume = initSmallImpactVolume + (0.2f * Random.Range(-1f, 1f));
            smallImpactSource.Play();
        }
    }
}
