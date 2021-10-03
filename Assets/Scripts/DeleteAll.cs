using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("DELETE " + other.gameObject.name + " TAG " + other.gameObject.tag);
        if (other.gameObject.tag == "canFall") {

            DropTowerable script = other.gameObject.GetComponentInParent(typeof(DropTowerable)) as DropTowerable;
            if (script != null && script.gameObject.tag == "canFall")
            {
                Destroy(script.gameObject);
                return;
            }

            Destroy(other.gameObject);
        }
        
    }
}
