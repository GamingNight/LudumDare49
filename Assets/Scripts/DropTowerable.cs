using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerable : MonoBehaviour
{
    public float radiusBoundary;
    public GameObject[] towerablePrefabs;
    public Material ghostMaterial;

    int currentTowerableIndex;
    GameObject ghostObject;
    Material trueMaterial;
    float init_origin_y = 0;

    void Start() {
        ghostObject = null;
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
                    ghostObject.GetComponent<BoxCollider>().enabled = true;
                    ghostObject.GetComponent<Rigidbody>().isKinematic = false;
                    ghostObject.GetComponent<MeshRenderer>().sharedMaterial = trueMaterial;
                    foreach (Transform child in ghostObject.transform) {
                        if (child.gameObject.tag == "Towerable") {
                            child.gameObject.SetActive(true);
                        }
                    }
                    ghostObject.transform.parent = null;
                    InstantiateNewGhost(worldPosition);
                } else {
                    if (ghostObject == null) {
                        InstantiateNewGhost(worldPosition);
                    }
                    ghostObject.transform.localPosition = new Vector3(worldPosition.x, 0.75f, worldPosition.z);
                }
            }
        } else {
            Destroy(ghostObject);
        }
    }

    private void InstantiateNewGhost(Vector3 worldPosition) {

        currentTowerableIndex = Random.Range(0, towerablePrefabs.Length);
        GameObject prefab = towerablePrefabs[currentTowerableIndex];
        trueMaterial = prefab.GetComponent<MeshRenderer>().sharedMaterial;
        Vector3 position = new Vector3(worldPosition.x, 0.75f, worldPosition.z);
        ghostObject = Instantiate<GameObject>(prefab, position, Quaternion.identity);
        ghostObject.GetComponent<MeshRenderer>().sharedMaterial = ghostMaterial;
        ghostObject.GetComponent<BoxCollider>().enabled = false;
        ghostObject.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Transform child in ghostObject.transform) {
            if (child.gameObject.tag == "Towerable") {
                child.gameObject.SetActive(false);
            }
        }
        ghostObject.transform.parent = transform;
    }
}
