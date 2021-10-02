using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerable : MonoBehaviour
{
    public Vector2 topLeftBoundaries;
    public Vector2 bottomRightBoundaries;
    public float groundHeight;
    public GameObject[] towerablePrefabs;

    void Start() {

    }

    void Update() {
        bool leftClick = Input.GetMouseButtonDown(0);
        if (leftClick) {
            Plane plane = new Plane(Vector3.up, 0);
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPosition;
            if (plane.Raycast(ray, out distance)) {
                worldPosition = ray.GetPoint(distance);
                Debug.Log(worldPosition);
                int index = Random.Range(0, towerablePrefabs.Length);
                GameObject prefab = towerablePrefabs[index];
                Vector3 tPosition = new Vector3(Mathf.Max(topLeftBoundaries.x, Mathf.Min(bottomRightBoundaries.x, worldPosition.x)), groundHeight + 0.5f, Mathf.Max(bottomRightBoundaries.y, Mathf.Min(topLeftBoundaries.y, worldPosition.z)));
                Instantiate<GameObject>(prefab, tPosition, Quaternion.identity);
            }
        }
    }
}
