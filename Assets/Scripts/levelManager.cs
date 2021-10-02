using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{

    public GameObject groundPrefabs;
    public GameObject EnvironmentPrefabs;
    public int nextLevelOffset = 120;
    public float cameraSpeed = 150;

    private float deathCount = 0;
    private bool cameraIsMoving = false;
    private float PositionYTarget;
    private GameObject currentGround = null;
    private GameObject currentEnvironment = null;
    private GameObject nextGround = null;
    private GameObject nextEnvironment = null;

    // Start is called before the first frame update
    void Start()
    {
        currentEnvironment = Instantiate<GameObject>(EnvironmentPrefabs, Vector3.zero, Quaternion.identity);
        currentGround = Instantiate<GameObject>(groundPrefabs, Vector3.zero, Quaternion.identity);

        PositionYTarget = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > PositionYTarget)
        {
            cameraIsMoving = true;
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

    private void onGameOver()
    {
        deathCount = deathCount + 1;
        Vector3 new_position = Vector3.zero;
        new_position.y = new_position.y - deathCount * nextLevelOffset;
        Debug.Log ("new_position.y");
        Debug.Log (new_position.y);
        nextEnvironment = Instantiate<GameObject>(EnvironmentPrefabs, new_position, Quaternion.identity);
        nextGround = Instantiate<GameObject>(groundPrefabs, new_position, Quaternion.identity);

        PositionYTarget = PositionYTarget - nextLevelOffset;
    }

    private void OnArrive2TheNextLevel()
    {
        cameraIsMoving = false;
        Destroy(currentGround);
        Destroy(currentEnvironment);
        currentGround = nextGround;
        currentEnvironment = nextEnvironment;
        nextGround = null;
        nextEnvironment = null;


    }

    private void popLevel(float depth)
    {

    }

}
