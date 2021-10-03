using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerable : MonoBehaviour
{
    public float radiusBoundary;
    public GameObject[] towerablePrefabs;

    int currentTowerableIndex;
    GameObject ghostObject;

    public Material ghostMaterial;
    private Material plainMaterial;

    void Start() {
        ghostObject = null;
        plainMaterial = null;
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
                        TowerableData data = ghostObject.GetComponent<TowerableData>();
                        Type colliderType = data.GetColliderType();
                        ((Collider)data.mainObject.GetComponent(colliderType)).enabled = true;
                        data.mainObject.GetComponent<Rigidbody>().isKinematic = false;
                        data.mainObject.GetComponent<Animator>().SetBool("Spawn", true);
                        foreach (Transform child in data.mainObject.transform) {
                            if (child.gameObject.tag == "Towerable") {
                                child.gameObject.SetActive(true);
                            }
                        }
                        ghostObject.transform.parent = null;
                        data.mainObject.GetComponent<MeshRenderer>().sharedMaterial = plainMaterial;
                        InstantiateNewGhost(worldPosition);
                    }
                } else {
                    if (ghostObject == null) {
                        InstantiateNewGhost(worldPosition);
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

    private void InstantiateNewGhost(Vector3 worldPosition) {

        currentTowerableIndex = UnityEngine.Random.Range(0, towerablePrefabs.Length);
        GameObject prefab = towerablePrefabs[currentTowerableIndex];
        Vector3 position = new Vector3(worldPosition.x, 0.1f, worldPosition.z);
        ghostObject = Instantiate<GameObject>(prefab, position, Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
        TowerableData data = ghostObject.GetComponent<TowerableData>();
        Type colliderType = data.GetColliderType();
        ((Collider)data.mainObject.GetComponent(colliderType)).enabled = false;
        data.mainObject.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Transform child in data.mainObject.transform) {
            if (child.gameObject.tag == "Towerable") {
                child.gameObject.SetActive(false);
            }
        }
        ghostObject.transform.parent = transform;
        plainMaterial = data.mainObject.GetComponent<MeshRenderer>().sharedMaterial;
        data.mainObject.GetComponent<MeshRenderer>().sharedMaterial = ghostMaterial;
    }
}