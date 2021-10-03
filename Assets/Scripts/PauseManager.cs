using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    private bool paused = false;
    public GameObject pauseText;
    private List<AudioSource> pausedAudioSources;

    public void Start() {
        paused = false;
        pausedAudioSources = new List<AudioSource>();
    }

    private void PauseGame() {
        Time.timeScale = 0;
        pauseText.SetActive(true);
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audioSources) {
            if (a.isPlaying) {
                a.Pause();
                Debug.Log("pausedAudioSources = " + pausedAudioSources);
                Debug.Log("a = " + a);
                pausedAudioSources.Add(a);
            }
        }
    }

    private void ResumeGame() {
        Time.timeScale = 1;
        pauseText.SetActive(false);
        foreach (AudioSource a in pausedAudioSources) {
            a.UnPause();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                PauseGame();
                paused = true;
            } else {
                ResumeGame();
                paused = false;
            }
        }
    }
}
