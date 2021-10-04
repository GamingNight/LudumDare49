using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{

    private Animator AnimatorController;

    public bool collected;

    void Start() {
        AnimatorController = gameObject.GetComponent<Animator>();
        collected = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Towerable" && !collected) {
            collected = true;
            CollectableManager.GetInstance().CollectItem(gameObject);
        }else if (other.gameObject.tag == "Ghost") {
            AnimatorController.SetBool("Inside", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ghost") {
            AnimatorController.SetBool("Inside", false);
        }
    }
}