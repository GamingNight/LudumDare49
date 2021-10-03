using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyereImpactAudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;
    private bool played;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        played = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!played && collision.gameObject.tag == "Ground") {
            audioSource.Play();
            played = true;
        }
    }
}
