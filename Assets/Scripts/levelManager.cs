using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{

    public GameObject groundPrefabs;
    public GameObject EnvironmentPrefabs;
    public GameObject GameOverAreaPrefabs;
    public GameObject TerminatorPrefabs;
    public int nextLevelOffset = 80;
    public float cameraSpeed = 15;

    private float deathCount = 0;
    private bool cameraIsMoving = false;
    private float PositionYTarget;
    private GameObject currentGround = null;
    private GameObject currentEnvironment = null;
    private GameObject currentGameOverArea = null;
    private GameObject currentTerminator = null;
    private GameObject nextGround = null;
    private GameObject nextEnvironment = null;

    // Start is called before the first frame update
    void Start()
    {
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



        PositionYTarget = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > PositionYTarget)
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * cameraSpeed);
            return;
        }
        else if (cameraIsMoving)
        {
            OnArrive2TheNextLevel();
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            onGameOver();
        }
    }

    public void onGameOver()
    {
        // Already trigged
        if (cameraIsMoving)
        {
            return;
        }
        cameraIsMoving = true;
        deathCount = deathCount + 1;
        Vector3 new_position = Vector3.zero;
        new_position.y = new_position.y - deathCount * nextLevelOffset;
        nextEnvironment = Instantiate<GameObject>(EnvironmentPrefabs, new_position, Quaternion.identity);
        nextGround = Instantiate<GameObject>(groundPrefabs, new_position, Quaternion.identity);

        PositionYTarget = PositionYTarget - nextLevelOffset;
    }

    private void OnArrive2TheNextLevel()
    {
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

    private void popLevel(float depth)
    {

    }

}
