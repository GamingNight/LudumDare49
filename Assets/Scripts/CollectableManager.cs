using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private static CollectableManager instance;

    private AudioSource biteSFX;

    public static CollectableManager GetInstance() {

        return instance;
    }

    public GameObject collectablePrefab;
    public float popupMaxRadius;
    public float heightBetweenItems;
    
    private int nbItemCollected;
    private float initHeight;
    private float heightOffset;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        nbItemCollected = 0;
        heightOffset = 1;
        biteSFX = GetComponent<AudioSource>();
        Init();
    }

    public void Init() {
        nbItemCollected = 0;
        initHeight = heightOffset - (levelManager.GetInstance().nextLevelOffset * levelManager.GetInstance().GetDeathCount());
        InstantiateNewCollectable();
    }

    public void CollectItem(GameObject collectable) {
        nbItemCollected++;
        Destroy(collectable);
        InstantiateNewCollectable();
        biteSFX.Play();
    }
    private void InstantiateNewCollectable() {

        GameObject item = Instantiate<GameObject>(collectablePrefab, transform);
        float radius = Random.Range(0f, popupMaxRadius);
        if(nbItemCollected == 0) {
            radius = 0;
        }
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float radiusMark = Mathf.Floor(Mathf.Pow(radius / popupMaxRadius * 3,1f));
        float height = Mathf.Max(initHeight + (heightBetweenItems * nbItemCollected/(1f+radiusMark)), initHeight);
        item.transform.position = new Vector3(radius * Mathf.Cos(angle),height, radius * Mathf.Sin(angle));
    }

    public int GetCollectedItemCount() {

        return nbItemCollected;
    }
}
