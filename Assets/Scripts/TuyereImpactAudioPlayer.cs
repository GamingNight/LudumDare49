using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyereImpactAudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;
    private bool played;

    public AudioSource smallImpactSource;
    public AudioClip[] allAudioClip;
    private float initSmallImpactPitch;
    private float initSmallImpactVolume;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        played = false;
        initSmallImpactPitch = audioSource.pitch;
        initSmallImpactVolume = audioSource.volume / 3;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!played && collision.gameObject.tag == "Ground") {
            audioSource.Play();
            played = true;
        }
        if(collision.gameObject.tag == "Object") {
            AudioSource objAuduiSource = collision.gameObject.GetComponent<AudioSource>();
            if (objAuduiSource != null)
            {
                int rand = (int)Random.Range(0f, 2.99f);
                objAuduiSource.clip = allAudioClip[rand];

                objAuduiSource.pitch = initSmallImpactPitch + (0.3f * Random.Range(-1f, 1f));
                objAuduiSource.volume = initSmallImpactVolume + (0.3f * Random.Range(-1f, 1f));
                objAuduiSource.Play();

            }
            else
            {
                Debug.Log("AudioSource of "+collision.gameObject.name+" is NULLLLLLLLLLL");
            }

        }
    }
}
