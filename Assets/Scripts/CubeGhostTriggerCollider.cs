using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGhostTriggerCollider : MonoBehaviour
{
    private bool collideWithTowerable;

    private List<GameObject> collidingTowerables;

    private void Start() {
        collideWithTowerable = false;
        collidingTowerables = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Towerable") {
            collideWithTowerable = true;
            if (!collidingTowerables.Contains(other.gameObject)) {
                collidingTowerables.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Towerable") {
            collidingTowerables.Remove(other.gameObject);
            if (collidingTowerables.Count == 0)
                collideWithTowerable = false;
        }
    }

    public bool CollideWithTowerable() {

        return collideWithTowerable;
    }

    public List<GameObject> GetCollidingTowerables() {
        return new List<GameObject>(collidingTowerables);
    }

    public float GetHighestCollidingTowerableVal() {

        float highest = float.MinValue;
        foreach (GameObject tow in collidingTowerables) {
            if (tow == null) {
                continue;
            }
            Bounds b = tow.GetComponent<Collider>().bounds;
            float val = b.center.y + b.extents.y;
            if (val > highest)
                highest = val;
        }
        return highest;
    }
}
