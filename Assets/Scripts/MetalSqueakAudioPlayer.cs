using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSqueakAudioPlayer : MonoBehaviour
{
    public AudioClip[] squeaks;
    private AudioSource audioSource;

    private int phase;
    private float initPitch;
    private float initVolume;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        phase = 0;
        initPitch = audioSource.pitch;
        initVolume = audioSource.volume;
    }

    void Update() {
        float angle = Vector3.Angle(Vector3.up, transform.up);
        if (!audioSource.isPlaying) {
            if (angle < 2) {
                phase = 0;
            } else if (angle > 2 && phase == 0) {
                audioSource.pitch = initPitch + (0.2f * Random.Range(-1f, 1f));
                audioSource.volume = initVolume + (0.1f * Random.Range(-1f, 1f));
                audioSource.clip = squeaks[Random.Range(0, squeaks.Length - 1)];
                audioSource.Play();
                phase = 1;
            } else if (angle > 20 && phase == 1) {
                audioSource.pitch = initPitch + (0.2f * Random.Range(-1f, 1f));
                audioSource.volume = initVolume + (0.1f * Random.Range(-1f, 1f));
                audioSource.clip = squeaks[squeaks.Length - 1];
                audioSource.Play();
                phase = 2;
            }
        }
    }
}
