using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Towerable") {
            CollectableManager.GetInstance().CollectItem(gameObject);
        }
    }
}
