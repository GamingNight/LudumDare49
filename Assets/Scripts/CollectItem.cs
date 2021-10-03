using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{

    private Animator AnimatorController;


    void Start() {
        AnimatorController = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Towerable") {
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