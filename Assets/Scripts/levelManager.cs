using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    private static levelManager instance;

    public static levelManager GetInstance() {
        return instance;
    }

    public GameObject groundPrefabs;
    public GameObject EnvironmentPrefabs;
    public GameObject GameOverAreaPrefabs;
    public GameObject TerminatorPrefabs;
    public int nextLevelOffset = 80;
    public float cameraSpeed = 15;

    private int deathCount = 0;
    private bool cameraIsMoving = false;
    private float positionYTarget;
    private GameObject currentGround = null;
    private GameObject currentEnvironment = null;
    private GameObject currentGameOverArea = null;
    private GameObject currentTerminator = null;
    private GameObject nextGround = null;
    private GameObject nextEnvironment = null;

    private AudioSource windAudioSource;
    private float initWindVolume;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        currentEnvironment = Instantiate<GameObject>(EnvironmentPrefabs, Vector3.zero, Quaternion.identity);
        currentGround = Instantiate<GameObject>(groundPrefabs, Vector3.zero, Quaternion.identity);

        Vector3 gameOverAreaPosition = Vector3.zero;
        gameOverAreaPosition.y = gameOverAreaPosition.y - 10;
        currentGameOverArea = Instantiate<GameObject>(GameOverAreaPrefabs, gameOverAreaPosition, Quaternion.identity);
        TriggerGameOver triggerGameOver = currentGameOverArea.GetComponent<TriggerGameOver>();
        triggerGameOver.setLevelManager(this);

        Vector3 terminatorPosition = Vector3.zero;
        terminatorPosition.y = terminatorPosition.y - 150;
        currentTerminator = Instantiate<GameObject>(TerminatorPrefabs, terminatorPosition, Quaternion.identity);



        positionYTarget = transform.position.y;

        windAudioSource = GetComponent<AudioSource>();
        initWindVolume = windAudioSource.volume;
    }

    // Update is called once per frame
    void Update() {

        if (transform.position.y > positionYTarget) {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * cameraSpeed, Space.World);
            if (windAudioSource.volume < 0.8f)
                windAudioSource.volume += 0.002f;
        } else if (cameraIsMoving) {
            OnArrive2TheNextLevel();
        }
        else if (windAudioSource.volume > initWindVolume)
                windAudioSource.volume -= 0.005f;
        {

        }

    }

    public void onGameOver() {
        // Already trigged
        if (cameraIsMoving) {
            return;
        }
        cameraIsMoving = true;
        deathCount = deathCount + 1;
        Vector3 new_position = Vector3.zero;
        new_position.y = new_position.y - deathCount * nextLevelOffset;
        nextEnvironment = Instantiate<GameObject>(EnvironmentPrefabs, new_position, Quaternion.identity);
        nextGround = Instantiate<GameObject>(groundPrefabs, new_position, Quaternion.identity);

        positionYTarget = positionYTarget - nextLevelOffset;

        CollectableManager.GetInstance().Init();
        TowerableManager.GetInstance().Init();
    }

    private void OnArrive2TheNextLevel() {
        cameraIsMoving = false;

        // delete by Terminator
        //Destroy(currentGround);
        Destroy(currentEnvironment);
        Destroy(currentGameOverArea);
        Destroy(currentGameOverArea);
        currentGround = nextGround;
        currentEnvironment = nextEnvironment;
        nextGround = null;
        nextEnvironment = null;

        Vector3 gameOverAreaPosition = Vector3.zero;
        gameOverAreaPosition.y = gameOverAreaPosition.y - deathCount * nextLevelOffset - 10;
        currentGameOverArea = Instantiate<GameObject>(GameOverAreaPrefabs, gameOverAreaPosition, Quaternion.identity);
        TriggerGameOver triggerGameOver = currentGameOverArea.GetComponent<TriggerGameOver>();
        triggerGameOver.setLevelManager(this);

        GameObject oldTerminator = currentTerminator;
        Vector3 terminatorPosition = Vector3.zero;
        terminatorPosition.y = terminatorPosition.y - deathCount * nextLevelOffset - 150;
        currentTerminator = Instantiate<GameObject>(TerminatorPrefabs, terminatorPosition, Quaternion.identity);
        Destroy(oldTerminator);

    }

    public int GetDeathCount() {

        return deathCount;
    }

    private void popLevel(float depth) {

    }

}
