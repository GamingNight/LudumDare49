using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyereImpactAudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;
    private bool played;

    public AudioClip[] smallImpactAudioClip;
    private float initSmallImpactPitch;
    private float initSmallImpactVolume;

    private bool firstSmallImpact;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        played = false;
        initSmallImpactPitch = audioSource.pitch;
        initSmallImpactVolume = audioSource.volume;
        firstSmallImpact = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!played && collision.gameObject.tag == "Ground") {
            audioSource.Play();
            played = true;
        }
        if (collision.gameObject.tag == "Object") {
            AudioSource objAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (firstSmallImpact) {
                initSmallImpactPitch = objAudioSource.pitch;
                initSmallImpactVolume = objAudioSource.volume;
            }
            if (objAudioSource != null) {
                objAudioSource.clip = smallImpactAudioClip[Random.Range(0, smallImpactAudioClip.Length)];
                objAudioSource.pitch = initSmallImpactPitch + (0.2f * Random.Range(-1f, 1f));
                objAudioSource.volume = initSmallImpactVolume + (0.3f * Random.Range(-1f, 1f));
                objAudioSource.Play();
            }
        }
    }
}
