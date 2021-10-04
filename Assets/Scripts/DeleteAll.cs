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

        // Find Towerable Objects
        if (other.gameObject.tag == "Towerable")
        {
            Destroy(other.transform.parent.gameObject);
        }
        // Find ground
        DropTowerable script1 = other.gameObject.GetComponentInParent(typeof(DropTowerable)) as DropTowerable;
        if (script1 != null)
        {
            Destroy(script1.gameObject);
        }
    }
}
