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

    private int currentPrefabIndex;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        Init();
    }

    public void Init() {

        GenerateNewPrefab();
    }
    public List<GameObject> GetUnlockedPrefabs() {

        List<GameObject> unlockedList = new List<GameObject>();

        foreach (GameObject obj in towerablePrefabs) {
            if (obj.GetComponent<TowerableData>().howManyItemsToUnlock <= CollectableManager.GetInstance().GetCollectedItemCount()) {
                unlockedList.Add(obj);
            }
        }
        return unlockedList;
    }

    public void GenerateNewPrefab() {
        currentPrefabIndex = Random.Range(0, GetUnlockedPrefabs().Count);
    }

    public GameObject GetCurrentPrefab() {

        return GetUnlockedPrefabs()[currentPrefabIndex];
    }
}
