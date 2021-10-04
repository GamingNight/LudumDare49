using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerable : MonoBehaviour
{
    public float radiusBoundary;
    public AudioClip[] clickSounds;
    public GameObject[] titleLetters;

    Quaternion currentTowerableQuaternion;
    GameObject ghostObject;
    AudioSource audioSource;
    bool firstClick;
    bool noTitle;

    void Start() {
        ghostObject = null;
        currentTowerableQuaternion = Quaternion.identity;
        audioSource = GetComponent<AudioSource>();
        firstClick = true;
        noTitle = levelManager.GetInstance().GetDeathCount() > 0;
        if (noTitle) {
            foreach (GameObject letter in titleLetters) {
                Destroy(letter);
            }
        }
    }

    void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;
        Vector3 worldPosition = Vector3.zero;
        if (plane.Raycast(ray, out distance)) {
            worldPosition = ray.GetPoint(distance);
        }
        if (Mathf.Pow(worldPosition.x, 2) + Mathf.Pow(worldPosition.z, 2) < Mathf.Pow(radiusBoundary, 2)) {
            if (plane.Raycast(ray, out distance)) {
                bool leftClick = Input.GetMouseButtonDown(0);
                if (leftClick) {
                    if (ghostObject != null) {
                        Instantiate<GameObject>(TowerableManager.GetInstance().GetCurrentPrefab(), ghostObject.transform.position, ghostObject.transform.rotation);
                        Destroy(ghostObject);
                        TowerableManager.GetInstance().GenerateNewPrefab();
                        currentTowerableQuaternion = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                        GameObject prefab = TowerableManager.GetInstance().GetCurrentPrefab();
                        GameObject ghostPrefab = prefab.GetComponent<TowerableData>().ghostPrefab;
                        InstantiateNewGhost(worldPosition, ghostPrefab, currentTowerableQuaternion);
                        audioSource.clip = clickSounds[UnityEngine.Random.Range(0, clickSounds.Length)];
                        audioSource.Play();
                        if (firstClick) {
                            DestroyLetters();
                            firstClick = false;
                        }
                    }
                } else {
                    if (ghostObject == null) {
                        GameObject prefab = TowerableManager.GetInstance().GetCurrentPrefab();
                        GameObject ghostPrefab = prefab.GetComponent<TowerableData>().ghostPrefab;
                        InstantiateNewGhost(worldPosition, ghostPrefab, currentTowerableQuaternion);
                    }
                    CubeGhostTriggerCollider ghostTriggerCollider = ghostObject.GetComponentInChildren<CubeGhostTriggerCollider>();
                    float yPos = 0.1f;
                    if (ghostObject.GetComponentInChildren<CubeGhostTriggerCollider>().CollideWithTowerable()) {
                        yPos += ghostObject.transform.parent.InverseTransformPoint(0, ghostTriggerCollider.GetHighestCollidingTowerableVal(), 0).y;
                    }
                    ghostObject.transform.localPosition = new Vector3(worldPosition.x, yPos, worldPosition.z);
                }
            }
        } else {
            Destroy(ghostObject);
        }
    }

    private void InstantiateNewGhost(Vector3 worldPosition, GameObject prefab, Quaternion quaternion) {

        Vector3 position = new Vector3(worldPosition.x, 0.1f, worldPosition.z);
        ghostObject = Instantiate<GameObject>(prefab, position, quaternion);
        ghostObject.transform.parent = transform;
    }

    private void DestroyLetters() {
        if (noTitle)
            return;
        foreach (GameObject letter in titleLetters) {
            letter.GetComponent<Animator>().SetBool("Explosion", true);
            Destroy(letter, 3);
        }
    }
}