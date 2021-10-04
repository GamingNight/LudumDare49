using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pauseCaneva;
    public Text resumeText;
    public Text restartText;
    public Text quitText;
    public Color defaultColor;
    public Color highLightColor;

    private bool paused = false;
    private List<AudioSource> pausedAudioSources;
    private int indexSelection = 0;
    private float prevVerticalvalue = 0;


    public void Start() {
        paused = false;
        pausedAudioSources = new List<AudioSource>();
    }

    private void PauseGame() {
        Time.timeScale = 0;
        pauseCaneva.SetActive(true);
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audioSources) {
            if (a.isPlaying) {
                a.Pause();
                pausedAudioSources.Add(a);
            }
        }
        FindObjectOfType<DropTowerable>().enabled = false;
    }

    private void ResumeGame() {
        Time.timeScale = 1;
        pauseCaneva.SetActive(false);
        foreach (AudioSource a in pausedAudioSources) {
            a.UnPause();
        }
        FindObjectOfType<DropTowerable>().enabled = true;
    }

    void Quit() {
        Application.Quit();
    }

    void Restart() {
/*        TowerableData[] objs = FindObjectsOfType<TowerableData>();
        foreach (TowerableData obj in objs) {
            float angle = Random.Range(0, 90);
            obj.transform.Rotate(angle, 2*angle, 3*angle);
        }
        DropTowerable plane = FindObjectOfType<DropTowerable>();
        if (plane != null)
        {
            Vector3 position = plane.gameObject.transform.localPosition;
            position.z = position.z +1 ;
            position.x = position.x + 1;
            plane.gameObject.transform.localPosition = position;
            Vector3 angles = plane.gameObject.transform.eulerAngles;
            angles.z = position.z;
            angles.x = position.x + 1;
            plane.gameObject.transform.eulerAngles = angles;
            //transform.eulerAngles;
        }
        */
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (!paused) {
                PauseGame();
                paused = true;
                SetSelection(0);
            } else {
                ResumeGame();
                paused = false;
            }
        }

        if (paused)
        {
            float v = Input.GetAxisRaw("Vertical");

            if (v != 0 && v != prevVerticalvalue) {
                indexSelection = indexSelection - (int)v;
                if (indexSelection < 0) {
                    indexSelection = 2;
                } else if (indexSelection > 2) {
                    indexSelection = 0;
                }
                SetSelection(indexSelection);
            }
            prevVerticalvalue = v;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
                ExecuteSelection();
            }
        }
    }

    public void SetSelection(int i) {
        indexSelection = i;
        switch (indexSelection) {
            case 0:
                resumeText.fontStyle = FontStyle.Bold;
                resumeText.color = highLightColor;

                restartText.fontStyle = FontStyle.Normal;
                restartText.color = defaultColor;

                quitText.fontStyle = FontStyle.Normal;
                quitText.color = defaultColor;
                break;
            case 1:
                resumeText.fontStyle = FontStyle.Normal;
                resumeText.color = defaultColor;

                restartText.fontStyle = FontStyle.Bold;
                restartText.color = highLightColor;

                quitText.fontStyle = FontStyle.Normal;
                quitText.color = defaultColor;
                break;
            case 2:
                resumeText.fontStyle = FontStyle.Normal;
                resumeText.color = defaultColor;

                restartText.fontStyle = FontStyle.Normal;
                restartText.color = defaultColor;

                quitText.fontStyle = FontStyle.Bold;
                quitText.color = highLightColor;
                break;
            default:
                break;
        }
    }

    public void ExecuteSelection() {
        switch (indexSelection) {
            case 0:
                ResumeGame();
                paused = false;
                break;
            case 1:
                Restart();
                paused = false;
                break;
            case 2:
                Quit();
                break;
            default:
                break;
        }
    }
}
