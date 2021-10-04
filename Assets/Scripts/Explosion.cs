using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;
    public float power;

    private AudioSource audioData;

    private float timer;
    private bool exploded;

    void Start()
    {
        timer = 0;
        audioData = GetComponent<AudioSource>();
        exploded = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 && !exploded)
        {
            Boom();
            exploded = true;
        }
    }

    void Boom()
    {

        audioData.Play();

        //Physic
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

    }
}