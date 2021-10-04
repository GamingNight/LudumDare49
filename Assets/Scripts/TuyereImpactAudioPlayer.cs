using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyereImpactAudioPlayer : MonoBehaviour
{

    public float defaultSmallImpactPitch = 1f;
    public float defaultSmallImpactVolume = 0.3f;

    private AudioSource audioSource;
    private bool played;

    public AudioClip[] smallImpactAudioClip;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        played = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!played && collision.gameObject.tag == "Ground") {
            audioSource.Play();
            played = true;
        }
        if (collision.gameObject.tag == "Object") {
            AudioSource objAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (objAudioSource != null) {
                objAudioSource.clip = smallImpactAudioClip[Random.Range(0, smallImpactAudioClip.Length)];
                objAudioSource.pitch = defaultSmallImpactPitch + (0.2f * Random.Range(-1f, 1f));
                objAudioSource.volume = defaultSmallImpactVolume + (0.3f * Random.Range(-1f, 1f));
                objAudioSource.Play();
            }
        }
    }
}
