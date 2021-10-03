using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerable : MonoBehaviour
{
    public float radiusBoundary;
    public GameObject[] towerablePrefabs;

    int currentTowerableIndex;
    Quaternion currentTowerableQuaternion;
    GameObject ghostObject;

    void Start() {
        ghostObject = null;
        currentTowerableQuaternion = Quaternion.identity;
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
                        Instantiate<GameObject>(towerablePrefabs[currentTowerableIndex], ghostObject.transform.position, ghostObject.transform.rotation);
                        Destroy(ghostObject);
                        currentTowerableIndex = UnityEngine.Random.Range(0, towerablePrefabs.Length);
                        currentTowerableQuaternion = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                        GameObject prefab = towerablePrefabs[currentTowerableIndex];
                        GameObject ghostPrefab = prefab.GetComponent<TowerableData>().ghostPrefab;
                        InstantiateNewGhost(worldPosition, ghostPrefab, currentTowerableQuaternion);
                    }
                } else {
                    if (ghostObject == null) {
                        GameObject prefab = towerablePrefabs[currentTowerableIndex];
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
}