using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSqueakAudioPlayer : MonoBehaviour
{
    public AudioClip[] squeaks;
    private AudioSource audioSource;

    private int phase;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        phase = 0;
    }

    void Update() {
        float angle = Vector3.Angle(Vector3.up, transform.up);
        if (angle < 2) {
            phase = 0;
        } else if (angle > 2 && phase == 0) {
            audioSource.clip = squeaks[2];
            audioSource.Play();
            phase = 1;
        } else if (angle > 20 && phase == 1) {
            audioSource.clip = squeaks[3];
            audioSource.Play();
            phase = 2;
        }
    }
}
