using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerable : MonoBehaviour
{
    public Vector2 topLeftBoundaries;
    public Vector2 bottomRightBoundaries;
    public GameObject[] towerablePrefabs;
    public GameObject[] highlightPrefabs;

    int currentTowerableIndex;
    GameObject highlightObject;

    void Start() {
        currentTowerableIndex = Random.Range(0, towerablePrefabs.Length);
        highlightObject = null;
    }

    void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Vector3 worldPosition = Vector3.zero;
        if (plane.Raycast(ray, out distance)) {
            worldPosition = ray.GetPoint(distance);
        }
        if (worldPosition.x > topLeftBoundaries.x && worldPosition.x < bottomRightBoundaries.x && worldPosition.z > bottomRightBoundaries.y && worldPosition.z < topLeftBoundaries.y) {
            if (plane.Raycast(ray, out distance)) {
                bool leftClick = Input.GetMouseButtonDown(0);
                if (leftClick) {
                    Destroy(highlightObject);
                    GameObject prefab = towerablePrefabs[currentTowerableIndex];
                    Vector3 tPosition = new Vector3(worldPosition.x, 0.75f, worldPosition.z);
                    Instantiate<GameObject>(prefab, tPosition, Quaternion.identity);
                    currentTowerableIndex = Random.Range(0, towerablePrefabs.Length);
                } else {
                    if (highlightObject == null) {
                        GameObject prefab = highlightPrefabs[currentTowerableIndex];
                        Vector3 position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        highlightObject = Instantiate<GameObject>(prefab, position, Quaternion.identity);
                    }
                    highlightObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                }
            }
        } else {
            Destroy(highlightObject);
        }

    }
}
