using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerGameOver : MonoBehaviour
{
	private levelManager levelManagerScript = null;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "GameOverTrigger") {
            if (levelManagerScript != null)
            {
                levelManagerScript.onGameOver();
            }
        }
    }

    public void setLevelManager(levelManager obj)
    {
        levelManagerScript = obj;
    }
}
