using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerableManager : MonoBehaviour
{
    private static TowerableManager instance;

    public static TowerableManager GetInstance() {
        return instance;
    }

    public GameObject[] towerablePrefabs;

    private List<GameObject> unlockedTowerables;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }



    void Start() {
        unlockedTowerables = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {

    }
}
