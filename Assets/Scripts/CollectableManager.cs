using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private static CollectableManager instance;

    public static CollectableManager GetInstance() {

        return instance;
    }

    public GameObject collectablePrefab;
    public float popupMaxRadius;
    public float heightBetweenItems;
    
    private int nbItemCollected;
    private float initHeight;
    private float heightOffset;
    private int lastDeathCount;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        nbItemCollected = 0;
        heightOffset = 1;
        lastDeathCount = 0;
        Init();
    }

    private void Init() {
        nbItemCollected = 0;
        initHeight = heightOffset - (levelManager.GetInstance().nextLevelOffset * levelManager.GetInstance().GetDeathCount());
        InstantiateNewCollectable();
    }

    private void Update() {
        int deatCount = levelManager.GetInstance().GetDeathCount();
        if (lastDeathCount != deatCount) {
            Init();
            lastDeathCount = deatCount;
        }
    }

    public void CollectItem(GameObject collectable) {
        nbItemCollected++;
        Destroy(collectable);
        InstantiateNewCollectable();
    }

    private void InstantiateNewCollectable() {

        GameObject item = Instantiate<GameObject>(collectablePrefab, transform);
        float radius = Random.Range(0f, popupMaxRadius);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        item.transform.position = new Vector3(radius * Mathf.Cos(angle), initHeight + (heightBetweenItems * nbItemCollected), radius * Mathf.Sin(angle));
    }
}
